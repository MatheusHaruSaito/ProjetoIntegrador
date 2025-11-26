namespace unolink.api.Application.Services.ImagesSevice
{
    public interface IFilesService
    {
        Task<string> AddImage(IFormFile image, string baseUrl);
        Task<bool> DeleteFile(string FileUrl);

    }
}
