namespace DemoApplication.Areas.Admin.ViewModels.MainSlider
{
    public class AddViewModel
    {
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public IFormFile? Image { get; set; }
        public string ButtonName { get; set; }
        public string ButtonURL { get; set; }
        public int Order { get; set; }
    }
}
