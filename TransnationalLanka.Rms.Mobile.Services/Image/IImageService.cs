namespace TransnationalLanka.Rms.Mobile.Services.Image;

public interface IImageService
{
    Task<string> UploadFile(string fileName, string contentType, Byte[] fileContent);
    Task<Byte[]> GetFile(string filePath);
}