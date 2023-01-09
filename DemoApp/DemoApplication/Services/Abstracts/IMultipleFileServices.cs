using DemoApplication.Areas.Client.ViewModels.Home.Index;
using DemoApplication.Contracts.File;

namespace DemoApplication.Services.Abstracts
{
    public interface IMultipleFileService
    {
        Task<List<string>> UploadAsync(List<IFormFile> formFiles, UploadDirectory uploadDirectory);
        List<string>? GetFileUrl(List<ImageData>? fileNames, UploadDirectory uploadDirectory);
        Task DeleteAsync(List<ImageData>? fileNames, UploadDirectory uploadDirectory);
    }
}
