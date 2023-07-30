
using CloudinaryDotNet.Actions;

namespace RunGroupWebApp1.Interface
{
    public interface IPhotoServices
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(String publicId);
    }
}
