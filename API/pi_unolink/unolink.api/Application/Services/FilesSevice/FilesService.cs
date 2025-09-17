
using static System.Net.Mime.MediaTypeNames;

namespace unolink.api.Application.Services.ImagesSevice
{
    public class FilesService : IFilesService
    {
        private readonly IWebHostEnvironment _env;
        public FilesService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> AddImage(IFormFile image, string baseUrl)
        {
            if(image is null)
            {
                return null;
            }
            var resources = Path.Combine(_env.WebRootPath, "Resources");
            var name = Guid.NewGuid() + System.IO.Path.GetExtension(image.FileName);
            var path = Path.Combine(resources, name);

            using (var stream = new FileStream(path,FileMode.Create)) {
                await image.CopyToAsync(stream);
            }
            var url = $"{baseUrl}/resources/{name}";
            return url;
        }

        public async Task<bool> DeleteFile(string FileUrl)
        {
            
            string fileName = Path.GetFileName(FileUrl);

            var path = Path.Combine(_env.WebRootPath, "Resources",fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }

    }
}
