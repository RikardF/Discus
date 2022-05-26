using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using ExArbete.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace ExArbete.Services
{
    public class CloudStorageService : ICloudStorageService
    {
        private readonly GoogleCredential googleCredential;
        private readonly StorageClient storageClient;
        private readonly string bucketName;

        public CloudStorageService(IConfiguration configuration)
        {
            googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GoogleCredentialFile"));
            storageClient = StorageClient.Create(googleCredential);
            bucketName = configuration.GetValue<string>("GoogleCloudStorageBucket");
        }

        public async Task<string> UploadFileAsync(IBrowserFile imageFile, string fileNameForStorage)
        {
            using (var memoryStream = new MemoryStream())
            {
                string imageType = imageFile.ContentType;
                var resizedImage = await imageFile.RequestImageFileAsync(imageType, 200, 200);
                await resizedImage.OpenReadStream(resizedImage.Size).CopyToAsync(memoryStream);
                var dataObject = await storageClient.UploadObjectAsync(bucketName, fileNameForStorage, null, memoryStream);
                return dataObject.MediaLink;
            }
        }

        public async Task DeleteFileAsync(string fileNameForStorage)
        {
            await storageClient.DeleteObjectAsync(bucketName, fileNameForStorage);
        }

        public async Task<bool> CheckIfImageExist(string userId)
        {
            var images = storageClient.ListObjectsAsync(bucketName);
            if(await images.Where(i => i.Name.Contains(userId)).CountAsync() > 0) return true;
            else return false;
        }
    }
}