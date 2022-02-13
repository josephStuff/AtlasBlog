namespace AtlasBlog.Services.Interfaces
{
    public class BasicImageService : IImageService
    {
        public string ConvertByteArrayToFile(byte[] fileData, string extension)
        {
            try
            {
                var imageBase64Data = Convert.ToBase64String(fileData);
                return $"data:{extension};base64,{imageBase64Data}";
                                
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file)
        {
            using MemoryStream memoryStream = new();
            await file.CopyToAsync(memoryStream);
            byte[] byteFile = memoryStream.ToArray();
            return byteFile;

        }
        
    }
}
