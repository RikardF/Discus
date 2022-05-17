using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace ExArbete.Interfaces
{
    public interface ICloudStorageService
    {
        Task<string> UploadFileAsync(IBrowserFile imageFile, string fileNameForStorage);
        Task DeleteFileAsync(string fileNameForStorage);
        Task<bool> CheckIfImageExist(string userId);
    }
}