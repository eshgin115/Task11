using DemoApplication.Areas.Admin.ViewModels.MainSlider;
using DemoApplication.Contracts.File;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/mainslider")]
    [Authorize(Roles = "admin")]
    public class MainSliderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public MainSliderController(
            DataContext dataContext,
            IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        [HttpGet("list", Name = "admin-mainslider-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.MainSliders.Select(ms =>
                          new ListViewModel(ms.Id, ms.MainTitle, ms.Content,
                          _fileService.GetFileUrl(ms.ImageNameInFileSystem, UploadDirectory.Book), ms.ButtonName, ms.ButtonURL, ms.Order))
                          .ToListAsync();

            return View(model);
        }


        [HttpGet("add", Name = "admin-mainslider-add")]
        public IActionResult Add()
        {
            return View(new AddViewModel());
        }
        [HttpPost("add", Name = "admin-mainslider-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.Book);

            await AddSlider(model.Image!.FileName, imageNameInSystem);


            return RedirectToRoute("admin-mainslider-list");


            async Task AddSlider(string imageName, string imageNameInSystem)
            {
                var mainslider = new MainSlider
                {
                    MainTitle = model.MainTitle,
                    ButtonName = model.ButtonName,
                    Content = model.Content,
                    ButtonURL = model.ButtonURL,
                    Order = model.Order,
                    ImageName = imageName,
                    ImageNameInFileSystem = imageNameInSystem
                };

                await _dataContext.MainSliders.AddAsync(mainslider);

                await _dataContext.SaveChangesAsync();
            }
        }


        [HttpGet("update/{id}", Name = "admin-mainslider-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var mainSlider = await _dataContext.MainSliders.FirstOrDefaultAsync(b => b.Id == id);


            if (mainSlider is null) return NotFound();


            var model = new UpdateViewModel
            {
                Id = mainSlider.Id,
                ButtonName = mainSlider.ButtonName,
                ButtonURL = mainSlider.ButtonURL,
                Content = mainSlider.Content,
                MainTitle = mainSlider.MainTitle,
                Order = mainSlider.Order,
                ImageUrl = _fileService.GetFileUrl(mainSlider.ImageNameInFileSystem, UploadDirectory.Book)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-mainslider-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var mainSlider = await _dataContext.MainSliders.FirstOrDefaultAsync(b => b.Id == model.Id);


            if (mainSlider is null) return NotFound();



            if (!ModelState.IsValid) return View(model);



            await _fileService.DeleteAsync(mainSlider.ImageNameInFileSystem, UploadDirectory.Book);

            var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Book);

            await UpdateMainSliderAsync(model.Image.FileName, imageFileNameInSystem);

            return RedirectToRoute("admin-mainslider-list");



            async Task UpdateMainSliderAsync(string imageName, string imageNameInFileSystem)
            {
                mainSlider.MainTitle = model.MainTitle;
                mainSlider.ButtonURL = model.ButtonURL;
                mainSlider.ButtonName = model.ButtonName;
                mainSlider.Order = model.Order;
                mainSlider.Content = model.Content;
                mainSlider.ImageName = imageName;
                mainSlider.ImageNameInFileSystem = imageNameInFileSystem;


                await _dataContext.SaveChangesAsync();
            }
        }
        [HttpPost("delete/{id}", Name = "admin-mainslider-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var mainSlider = await _dataContext.MainSliders.FirstOrDefaultAsync(b => b.Id == id);


            if (mainSlider is null) return NotFound();

            await _fileService.DeleteAsync(mainSlider.ImageNameInFileSystem, UploadDirectory.Book);

            _dataContext.MainSliders.Remove(mainSlider);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-mainslider-list");
        }

    }
}