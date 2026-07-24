using Fitness.Interface.IService;

namespace Fitness.Service
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _env;

        public FileUploadService(IWebHostEnvironment env) => _env = env;

        public async Task<string> UploadFileAsync(IFormFile file, string folder)
        {
            var uploadsDir = Path.Combine(_env.WebRootPath ?? _env.ContentRootPath, "uploads", folder);
            Directory.CreateDirectory(uploadsDir);

            var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsDir, uniqueName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/{folder}/{uniqueName}";
        }
    }
}
