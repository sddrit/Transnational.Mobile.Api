using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TransnationalLanka.Rms.Mobile.Core.Enum;
using TransnationalLanka.Rms.Mobile.Core.Exceptions;
using TransnationalLanka.Rms.Mobile.Core.Extensions;
using TransnationalLanka.Rms.Mobile.Dal;
using TransnationalLanka.Rms.Mobile.Dal.Entities;
using TransnationalLanka.Rms.Mobile.Dal.Helper;
using TransnationalLanka.Rms.Mobile.Services.Request.Core;

namespace TransnationalLanka.Rms.Mobile.Services.Request
{
    public class RequestService : IRequestService
    {
        private readonly RmsDbContext _context;


        public RequestService(RmsDbContext context)
        {
            _context = context;
        }

        public async Task<Dal.Entities.RequestDetail> GetCartonByRequestNo(string requestNo, int cartonNo)
        {
            var requestDetail = await _context.RequestDetails.FirstOrDefaultAsync(x => x.RequestNo == requestNo && x.CartonNo == cartonNo);

            if (requestDetail == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                         Code = string.Empty,
                         Message =  $"Unable to find request detail {cartonNo}"
                     }
                });
            }

            return requestDetail;
        }

        private async Task<RequestView> GetRequestHeader(string requestNo)
        {
            var requestHeader = await _context.RequestViews.Where(r => r.RequestNo == requestNo && r.Status!= "Invoice Confirmed").FirstOrDefaultAsync();

            if (requestHeader == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                         Code = string.Empty,
                         Message =  $"Unable to find request header {requestNo}"
                     }
                });
            }

            return requestHeader;
        }

        public async Task<PagedResponse<SearchRequestResult>> SearchRequestHeader(string searchText = null, 
            int pageIndex = 1, int pageSize = 10)
        {
            IQueryable<RequestView> query = _context.RequestViews;

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(r => r.RequestNo.ToLower().Contains(searchText.ToLower())
                                         || r.Name.ToLower().Contains(searchText.ToLower()));
            }

            var requestHeaders = await query
                    .OrderByDescending(r => r.DeliveryDate)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .ToListAsync();

            var count = await query.CountAsync();

            var result = requestHeaders.Select(r => new SearchRequestResult
            {
                RequestNo = r.RequestNo,
                DeliveryDate = r.DeliveryDate.Value.IntToDate(),
                Name = r.Name,
                IsDigitallySigned = r.IsDigitallySigned == null ? false : r.IsDigitallySigned.Value
            }).ToList();

            var paginationResponse = new PagedResponse<SearchRequestResult>(result, pageIndex, pageSize, count);

            return paginationResponse;
        }

        public async Task<DocketDto> GetDocketDetails(string requestNo, string userName)
        {
            var requestHeader = await GetRequestHeader(requestNo);

            if(requestHeader==null)
            {
                throw new ServiceException(new ErrorMessage[]
               {
                     new ErrorMessage()
                     {
                         Code = string.Empty,
                         Message =  $"Unable to find pending request header {requestNo}"
                     }
               });
            }
            
            int docketSerailNo = GetSerialNo(requestNo, requestHeader.RequestType);

            var parms = new List<SqlParameter>
            {
                 new() { ParameterName = "@requestNo", Value = requestNo },
                 new() { ParameterName = "@printedBy", Value = userName },
                 new() { ParameterName = "@requestType", Value = requestHeader.RequestType}
             };

            var outSerialNo = new SqlParameter
            {
                ParameterName = "@serialNo",
                SqlDbType = SqlDbType.Int,
                Direction = docketSerailNo == 0 ? ParameterDirection.Output : ParameterDirection.Input,
                Value = docketSerailNo
            };

            parms.Add(outSerialNo);

            var docketEmptyDetail = new List<DocketEmptyDetail>();
            var docketDetails = new List<DocketDetail>();

            string spName = "exec docketRePrint ";
            string parameterNames = " @requestNo, @printedBy, @requestType, @serialNo";

            if (docketSerailNo == 0)
            {
                parameterNames = parameterNames + " OUTPUT ";
                spName = "exec docketInsertUpdateDelete ";
            }

            if (requestHeader.RequestType.ToLower() == RequestType.empty.ToString())
            {
                docketEmptyDetail = _context.Set<DocketEmptyDetail>()
                    .FromSqlRaw(spName + parameterNames, parms.ToArray()).ToList();
            }
            else
            {
                docketDetails = _context.Set<DocketDetail>().FromSqlRaw(spName + parameterNames, parms.ToArray())
                    .ToList();
            }

            var serialNo = (int)outSerialNo.Value;

            if (requestHeader.RequestType.ToLower() != RequestType.collection.ToString() && docketEmptyDetail.Count == 0 && docketDetails.Count == 0)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                         Code = string.Empty,
                         Message = $"Unable to find request details {requestNo}"
                     }
                });
            }

            return new DocketDto()
            {
                RequestNo = requestNo,
                SerialNo = serialNo,
                Address = requestHeader.Address,
                DocketType = requestHeader.DocketType,
                CustomerCode = requestHeader.CustomerCode,
                Name = requestHeader.Name,
                ContactNo = requestHeader.ContactNo,
                ContactPerson = requestHeader.ContactPerson,
                Department = requestHeader.Department,
                PoNo = requestHeader.PoNo,
                IsPrintAlternativeNo = requestHeader.IsPrintAlternativeNo,
                Route = requestHeader.Route,
                EmptyDetails = docketEmptyDetail,
                DocketDetails = docketDetails
            };
        }

        private int GetSerialNo(string requestNo, string requestType)
        {
            int serialNo = 0;

            if (requestType.ToLower() == RequestType.empty.ToString())
            {
                var emptyDocket = _context.EmptyDocketPrintHeaders.Where(e => e.RequestNo == requestNo)
                    .OrderByDescending(e => e.PrintedOn).FirstOrDefault();

                if (emptyDocket == null)
                {
                    return 0;
                }

                serialNo = emptyDocket.SerialNo;
            }
            else
            {
                var docket = _context.DocketPrintSlices.Where(p => p.RequestNo == requestNo)
                    .OrderByDescending(p => p.TrackingId).FirstOrDefault();

                if (docket == null)
                {
                    return 0;
                }

                serialNo = docket.SerialNo;
            }

            return serialNo;
        }
      
        public async Task<List<ValidateCartonResult>> ValidateRequest(string requestNo, int cartonNo)
        {

            var request = await GetRequestHeader(requestNo);

            if (request == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                         Code = string.Empty,
                         Message = $"Unable to find request {requestNo}"
                     }
                });
            }

            var cartonValidationModels = new List<CartonValidationModel>()
            {
                new CartonValidationModel()
                {
                    CartonNo=cartonNo,
                    ToCartonNo=cartonNo
                }
            };

            var parms = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = "@customerCode", Value = request.CustomerCode },
                 new SqlParameter { ParameterName = "@requestNo", Value = requestNo },
                 new SqlParameter { ParameterName = "@requestType", Value = request.RequestType.ToString()},
                 new SqlParameter { ParameterName = "@statementType", Value ="Edit"},
                 new SqlParameter
                {
                   ParameterName = "@requestDetail",
                   TypeName = "dbo.udtValidateCarton",
                   SqlDbType = SqlDbType.Structured,
                   Value =cartonValidationModels.ToList().ToDataTable()
                },
            };

            var result = await _context.Set<ValidateCartonResult>().FromSqlRaw("exec requestInsertUpdateDeleteValidate " +
                " @customerCode,@requestNo, @requestType,@statementType,@requestDetail ", parms.ToArray()).ToListAsync();

            if (result == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Code = string.Empty,
                        Message = $"nothing to validate"
                    }
                });
            }

            return result;
        }

        public async Task<bool> UploadSignature(RequestSignatureModel model)
        {
            var request = await _context.RequestHeaders.Where(r => r.RequestNo == model.RequestNo)
                .FirstOrDefaultAsync();

            if (request == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Message = "Unable to find request"
                    }
                });
            }

            request.DigitallySignedDate = DateTime.Now;
            request.IsDigitallySigned = true;
            request.DigitallySignedBy = model.UserName;

            var signatureInfo = new RequestSignatureImage
            {
                ImagePath = model.FileName,
                ContentType = model.ContentType,
                RequestNo = model.RequestNo,
                UploadedBy = model.UserName,
                UploadedDate = System.DateTime.Now,
                CustomerName= model.CustomerName,
                CustomerNIC=model.CustomerNIC,
                CustomerDepartment= model.CustomerDepartment,
                CustomerDesignation= model.CustomerDesignation,
                DocketSerialNo= model.DocketSerialNo                
            };

            _context.Add(signatureInfo);

            await _context.SaveChangesAsync();

            return true;


        }
    }
}
