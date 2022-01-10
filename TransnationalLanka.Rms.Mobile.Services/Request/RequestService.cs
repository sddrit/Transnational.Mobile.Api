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
            var requestHeader = await _context.RequestViews.Where(r => r.RequestNo == requestNo && r.Status != "Invoice Confirmed" && r.Deleted == false).FirstOrDefaultAsync();

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
                IsDigitallySigned = r.IsDigitallySigned.Value
            }).ToList();
          

            var paginationResponse = new PagedResponse<SearchRequestResult>(result, pageIndex, pageSize, count);

            return paginationResponse;
        }

        public async Task<DocketDto> GetDocketDetails(string requestNo, string userName)
        {
            var requestHeader = await GetRequestHeader(requestNo);

            if (requestHeader == null)
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
                 new() { ParameterName = "@requestType", Value = requestHeader.RequestType},
                  new() { ParameterName = "@isMobile", Value = true}
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
            var docketDetails = new List<string>();

            string spName = "exec docketRePrint ";
            string parameterNames = " @requestNo, @printedBy, @requestType,@isMobile, @serialNo ";

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
                var requestDetails = _context.Set<DocketDetail>().FromSqlRaw(spName + parameterNames, parms.ToArray()).ToList();
                docketDetails = requestDetails.Select(i => i.CartonNo).ToList();

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
                    .OrderByDescending(e => e.PrintedOn);

                if (emptyDocket.Count() == 0)
                {
                    return 0;
                }

                serialNo = Convert.ToInt32(emptyDocket.FirstOrDefault().SerialNo);
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

            var cartonValidationModels = new List<DocketDetail>()
            {
                new DocketDetail()
                {
                   CartonNo= cartonNo.ToString()
                }
            };

            var result = await ValidateRequest(requestNo, cartonValidationModels);

            return result;
        }

        private async Task<List<ValidateCartonResult>> ValidateRequest(string requestNo, List<DocketDetail> docketDetails)
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

            var cartonValidationModels = docketDetails.Select(dt => new CartonValidationModel()
            {
                CartonNo = Convert.ToInt32(dt.CartonNo),
                ToCartonNo = Convert.ToInt32(dt.CartonNo)

            }).ToList();


            var parms = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = "@customerCode", Value = request.CustomerCode },
                  new SqlParameter { ParameterName = "@requestType", Value = request.RequestType.ToString()},
                 new SqlParameter { ParameterName = "@requestNo", Value = requestNo },
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
                " @customerCode, @requestType,@requestNo,@statementType,@requestDetail ", parms.ToArray()).ToListAsync();

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
            var validateRequest = await GetRequestHeader(model.RequestNo);

            if (validateRequest == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Message = "Unable to find request"
                    }
                });
            }

            var request = await _context.RequestHeaders.Where(r => r.RequestNo == model.RequestNo)
              .FirstOrDefaultAsync();

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
                CustomerName = model.CustomerName,
                CustomerNIC = model.CustomerNIC,
                CustomerDepartment = model.CustomerDepartment,
                CustomerDesignation = model.CustomerDesignation,
                DocketSerialNo = model.DocketSerialNo
            };

            _context.Add(signatureInfo);

            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> BindCartonsToColletion(CollectionCartonBindModel model)
        {

            var request = await GetRequestHeader(model.RequestNo);

            if (request == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Message = $"Unable to find request {model.RequestNo}"
                    }
                });

            }
            if (request.RequestType.ToLower() != RequestType.collection.ToString())
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Message = $"Unable to bind cartons for {request.RequestType}"
                    }
                });
            }

            var validatedResult = await ValidateRequest(model.RequestNo, model.DocketDetails);

            if (validatedResult.Where(x => x.CanProcess == false).Any())
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Message = "Request cannot be signed - invalid cartons collected"
                    }
                });
            }

            foreach (DocketDetail detail in model.DocketDetails)
            {
                var existResult = _context.RequestDetails.Where(rd => rd.RequestNo == model.RequestNo && rd.CartonNo.ToString() == detail.CartonNo);


                if (existResult.Count() == 0)
                {

                    _context.RequestDetails.Add(new RequestDetail()
                    {
                        CartonNo = Convert.ToInt32(detail.CartonNo),
                        Deleted = false,
                        FromMobile = true,
                        Picked = false,
                        PickListNo = string.Empty,
                        RequestNo = model.RequestNo,
                        CollectedBy = model.UserName,
                        CollectedDate = System.DateTime.Now
                    });
                }
            }
            _context.SaveChanges();

            return true;



        }
    }
}
