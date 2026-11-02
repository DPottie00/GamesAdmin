using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace GamesAdmin.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly IConfiguration _configuration;

        private readonly BlobServiceClient _blobServiceClient;
        private string containerName;

        public AzureStorageService(IConfiguration configuration)
        {
            // used to get appsetting, user-secrets, env vars, etc.
            _configuration = configuration;


            Console.WriteLine(_configuration["CoverArtContainerName"] ?? "NOT FOUND");

            // Get Azure container name
            containerName = _configuration["CoverArtContainerName"] ?? throw new InvalidOperationException("Missing confiuration: CoverArtContainerName");

            // Get Azure connection string
            string connectionString = _configuration.GetConnectionString("LootTableAzurite") ?? throw new InvalidOperationException("Missing confiuration: ConnectionStrings:LootTableAzurite");

            // initialize blob client
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string fileName)
        {
            // get client for containerName (e.g. "uploads")
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(fileName);

            var options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                }
            };



            // file I/O
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, options);
            }

            // return the URL for the saved file
            return blobClient.Uri.ToString();
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(fileName);

            var response = await blobClient.DeleteIfExistsAsync();

            return response.Value;
        }
    }
}
