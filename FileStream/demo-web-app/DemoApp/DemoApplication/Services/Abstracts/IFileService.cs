using DemoApplication.Areas.Client.ViewModels.Home.Index;
using DemoApplication.Contracts.Email;
using DemoApplication.Contracts.File;

namespace DemoApplication.Services.Abstracts
{
    public interface IFileService
    {
        Task<List<string>> UploadAsync(List<IFormFile> formFile, UploadDirectory uploadDirectory);
        List<string>? GetFileUrl(List<ImageData>? fileName, UploadDirectory uploadDirectory);
        Task DeleteAsync(string? fileName, UploadDirectory uploadDirectory);
    }
}
