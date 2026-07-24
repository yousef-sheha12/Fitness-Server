namespace Fitness.Interface.IService
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, string folder);
    }
}
