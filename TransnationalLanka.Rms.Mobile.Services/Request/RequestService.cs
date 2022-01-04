using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TransnationalLanka.Rms.Mobile.Core.Enum;
using TransnationalLanka.Rms.Mobile.Core.Exceptions;
using TransnationalLanka.Rms.Mobile.Core.Extensions;
using TransnationalLanka.Rms.Mobile.Dal;
using TransnationalLanka.Rms.Mobile.Dal.Entities;
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
                         Message =  $"Unable to find request detail  {cartonNo}"
                     }
                });
            }

            return requestDetail;
        }

        private async Task<RequestView> GetRequestHeader(string requestNo)
        {
            var requestHeader = await _context.RequestViews.Where(r => r.RequestNo == requestNo).FirstOrDefaultAsync();

            if (requestHeader == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                         Code = string.Empty,
                         Message =  $"Unable to find request header  {requestNo}"
                     }
                });
            }

            return requestHeader;
        }

        //need to add searching and paging.
        public async Task<List<RequestView>> SearchRequestHeader(string requestNo, string customerName)
        {
            var requestHeaders = await _context.RequestViews.Where(r => r.RequestNo.Contains(requestNo) && r.Name.Contains(customerName)).ToListAsync();
            return requestHeaders;

        }

        //need to refactor sp call.
        public async Task<DocketDto> GetDocketDetails(string requestNo, string userName)
        {
            var requestHeader = await GetRequestHeader(requestNo);

            int docketSerailNo = GetSerialNo(requestNo, requestHeader.RequestType);

            List<SqlParameter> parms = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = "@requestNo", Value = requestNo },
                 new SqlParameter { ParameterName = "@printedBy", Value = userName },
                 new SqlParameter { ParameterName = "@requestType", Value =requestHeader.RequestType}
             };

            var OutSerialNo = new SqlParameter
            {
                ParameterName = "@serialNo",
                SqlDbType = SqlDbType.Int,
                Direction = docketSerailNo == 0 ? ParameterDirection.Output : ParameterDirection.Input,
                Value = docketSerailNo
            };

            parms.Add(OutSerialNo);

            List<DocketEmptyDetail> docketEmptyDetail = new List<DocketEmptyDetail>();
            List<DocketDetail> docketDetails = new List<DocketDetail>();

            string spName = "exec docketRePrint ";
            string parameterNames = " @requestNo, @printedBy, @requestType, @serialNo";

            if (docketSerailNo == 0)
            {
                parameterNames = parameterNames + " OUTPUT ";
                spName = "exec docketInsertUpdateDelete ";
            }

            if (requestHeader.RequestType.ToLower() == RequestType.empty.ToString())
                docketEmptyDetail = _context.Set<DocketEmptyDetail>().FromSqlRaw(spName + parameterNames, parms.ToArray()).ToList();
            else
                docketDetails = _context.Set<DocketDetail>().FromSqlRaw(spName + parameterNames, parms.ToArray()).ToList();

            int serialNo = (int)OutSerialNo.Value;

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
                serialNo = _context.EmptyDocketPrintHeaders.Where(e => e.RequestNo == requestNo).OrderByDescending(e => e.PrintedOn).FirstOrDefault().SerialNo;
            }
            else
            {
                serialNo = _context.DocketPrintSlices.Where(p => p.RequestNo == requestNo).OrderByDescending(p => p.TrackingId).FirstOrDefault().SerialNo;
            }

            return serialNo;
        }
        //refactor sp call
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

            List<CartonValidationModel> cartonValidationModels = new List<CartonValidationModel>()
            {
                new CartonValidationModel()
                {
                    CartonNo=cartonNo,
                    ToCartonNo=cartonNo
                }
            };

            List<SqlParameter> parms = new List<SqlParameter>
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

        //to implement image upload
        public async Task<bool> UploadSignature(RequestSignatureModel model)
        {

            var request = await _context.RequestHeaders.Where(r => r.RequestNo == model.RequestNo).FirstOrDefaultAsync();

            if (request != null)
            {
                request.DigitallySignedDate = System.DateTime.Now;
                request.IsDigitallySigned = true;
                request.DigitallySignedBy = model.UserName;

            }

            var signatureInfo = new RequestSignatureImage
            {
                ImagePath = model.ImagePath,
                RequestNo = model.RequestNo,
                UploadedBy = model.RequestNo,
                UploadedDate = System.DateTime.Now
            };

            _context.Add(signatureInfo);

            _context.SaveChanges();

            //to implement image upload

            return true;


        }
    }
}
