using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MyLeasing.Web.Helpers
{
    public class BlobHelper : IBlobHelper
    {
        private readonly IConfiguration _configuration;


        public BlobHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            // var keys = _configuration["AzureStorage:BlobLicinio"];
        }

        public async Task<Guid> UploadBlobAsync(
            IFormFile file, string containerName)
        {
            var stream = file.OpenReadStream();
            return await UploadStreamAsync(stream, containerName);
        }

        public async Task<Guid> UploadBlobAsync(
            byte[] file, string containerName)
        {
            var stream = new MemoryStream();
            return await UploadStreamAsync(stream, containerName);
        }

        public async Task<Guid> UploadBlobAsync(
            string image, string containerName)
        {
            Stream stream = File.OpenRead(image);
            return await UploadStreamAsync(stream, containerName);
        }

        private async Task<Guid> UploadStreamAsync(
            Stream stream, string containerName)
        {
            var name = Guid.NewGuid();


            // Get a reference to a container named "sample-container"
            // and then create it
            var blobContainerClient =
                new BlobContainerClient(
                    _configuration["AzureStorage:BlobLicinio"],
                    containerName);


            // Get a reference to a blob named "sample-file"
            // in a container named "sample-container"
            var blobClient =
                blobContainerClient.GetBlobClient(name.ToString());


            // Check if the container already exists
            bool containerExists = await blobContainerClient.ExistsAsync();


            // Create the container if it doesn't exist
            if (!containerExists) await blobContainerClient.CreateAsync();

            // Perform any additional setup or
            // configuration for the container if needed
            // Upload local file
            await blobClient.UploadAsync(stream);

            return name; // "Uploaded file to blob storage.";
        }
    }
}