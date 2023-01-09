using DemoApplication.Areas.Client.ViewModels.Home.Index;
using DemoApplication.Contracts.File;
using DemoApplication.Services.Abstracts;

namespace DemoApplication.Services.Concretes
{
    public class MultipleFileService : IMultipleFileService
    {
        private readonly ILogger<FileService>? _logger;

        public MultipleFileService(ILogger<FileService>? logger)
        {
            _logger = logger;
        }
        public async Task<List<string>> UploadAsync(List<IFormFile> formFiles, UploadDirectory uploadDirectory)
        {
            List<string> imageNamesInSystem = new List<string>();
            string directoryPath = GetUploadDirectory(uploadDirectory);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            foreach (var formFile in formFiles)
            {

                var imageNameInSystem = GenerateUniqueFileName(formFile.FileName);
                imageNamesInSystem.Add(imageNameInSystem);
                var filePath = Path.Combine(directoryPath, imageNameInSystem);
                try
                {
                    using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    await formFile.CopyToAsync(fileStream);
                }
                catch (Exception e)
                {
                    _logger!.LogError(e, "Error occured in file service");

                    throw;
                }
            }
            return imageNamesInSystem;
        }
     

        public List<string>? GetFileUrl(List<ImageData>? fileNames, UploadDirectory uploadDirectory)
        {

            List<string> result = new List<string>();
            if (fileNames == null) return result;
            string initialSegment = "client/custom-files/";
            foreach (var fileName in fileNames)
            {
                if (uploadDirectory == UploadDirectory.Book)
                {
                    result.Add($"{initialSegment}/books/{fileName}");
                }
            }
            return result;
        }

        private string GetUploadDirectory(UploadDirectory uploadDirectory)
        {
            string startPath = Path.Combine("wwwroot", "client", "custom-files");

            switch (uploadDirectory)
            {
                case UploadDirectory.Book:
                    return Path.Combine(startPath, "books");
                default:
                    throw new Exception("Something went wrong");
            }
        }

        private string GenerateUniqueFileName(string fileName)
        {
            return $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        }
        public async Task DeleteAsync(List<ImageData>? fileNames, UploadDirectory uploadDirectory)
        {
            foreach (var fileName in fileNames)
            {
                var deletePath = Path.Combine(GetUploadDirectory(uploadDirectory), fileName.ImageUrl);

                await Task.Run(() => File.Delete(deletePath));

            }
        }


    }
}
