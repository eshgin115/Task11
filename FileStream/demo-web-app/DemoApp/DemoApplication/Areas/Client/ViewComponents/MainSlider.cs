using DemoApplication.Areas.Admin.ViewModels.MainSlider;
using DemoApplication.Contracts.File;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Client.ViewComponents
{
    public class MainSlider : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public MainSlider(
            DataContext dataContext,
            IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            int SliderCount = 2;
            var model = await _dataContext.MainSliders.OrderBy(ms=>ms.Order).Select(ms =>
                           new ListViewModel(ms.Id, ms.MainTitle, ms.Content,
                           _fileService.GetFileUrl(ms.ImageNameInFileSystem, UploadDirectory.Book), ms.ButtonName, ms.ButtonURL, ms.Order))
                           .Take(SliderCount)
                           .ToListAsync();

            return View(model);
        }
    }
}
