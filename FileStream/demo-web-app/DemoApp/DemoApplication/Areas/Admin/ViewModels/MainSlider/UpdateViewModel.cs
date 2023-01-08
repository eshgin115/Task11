namespace DemoApplication.Areas.Admin.ViewModels.MainSlider
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; } //Guid.<extension>
        public string ButtonName { get; set; }
        public string ButtonURL { get; set; }
        public int Order { get; set; }
    }
}
