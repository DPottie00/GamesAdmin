namespace GamesAdmin.Services
{
    public interface IAzureStorageService
    {
        // Service to upload file to Azure Blob Storage
        // return: the URL of the uploaded file as a string
        public Task<string> UploadFileAsync(IFormFile file, string fileName);
    }
}
