using Microsoft.AspNetCore.Mvc;
using TransnationalLanka.Rms.Mobile.Core.Exceptions;
using TransnationalLanka.Rms.Mobile.Services.Image;
using TransnationalLanka.Rms.Mobile.Services.Request;
using TransnationalLanka.Rms.Mobile.Services.Request.Core;
using TransnationalLanka.Rms.Mobile.WebApi.Models;

namespace TransnationalLanka.Rms.Mobile.WebApi.Signature
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class SignatureController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly IImageService _imageService;

        public SignatureController(IRequestService requestService, IImageService imageService)
        {
            _requestService = requestService;
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] SignatureRequestBindingModel model)
        {
            try
            {
                var file = model.File;
                var fileExtension = Path.GetExtension(file.FileName);

                await using var fileMemoryStream = new MemoryStream();
                await file.CopyToAsync(fileMemoryStream);
                var fileContent = fileMemoryStream.ToArray();

                var uploadedPath = await _imageService.UploadFile($"req-{model.RequestNo}-{model.DocketSerialNo}-signature{fileExtension}",
                    file.ContentType, fileContent);

                await _requestService.UploadSignature(new RequestSignatureModel()
                {
                    ContentType = file.ContentType,
                    FileName = Path.GetFileName(uploadedPath),
                    RequestNo = model.RequestNo,
                    UserName = model.UserName,
                    DocketSerialNo = model.DocketSerialNo,
                    CustomerDepartment = model.CustomerDepartment,
                    CustomerDesignation = model.CustomerDesignation,
                    CustomerNIC = model.CustomerNIC,
                    CustomerName = model.CustomerName
                });
            }
            catch (ServiceException e)
            {
                return BadRequest(e);
            }

            return Ok();
        }
    }
}
