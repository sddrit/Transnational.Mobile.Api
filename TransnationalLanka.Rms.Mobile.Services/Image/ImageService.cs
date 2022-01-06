namespace TransnationalLanka.Rms.Mobile.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly string _uploadPath;

        public ImageService(string uploadPath)
        {
            _uploadPath = uploadPath;
        }

        public async Task<string> UploadFile(string fileName, string contentType, Byte[] fileContent)
        {
            var filePath = Path.Join(_uploadPath, fileName);
            await File.WriteAllBytesAsync(filePath, fileContent);
            return filePath;
        }

        public async Task<Byte[]> GetFile(string filePath)
        {
            return await File.ReadAllBytesAsync(filePath);
        }
    }
}
