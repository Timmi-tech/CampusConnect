using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;


namespace ChatSystem_1.Application.Services.Contracts
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
        Task<List<ImageUploadResult>> UploadImagesAsync(List<IFormFile> files);

        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}