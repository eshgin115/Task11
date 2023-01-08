namespace DemoApplication.Areas.Admin.ViewModels.MainSlider
{
    public class ListViewModel
    {
     
        public int Id { get; set; }
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public string ImageURL { get; set; }
        public string ButtonName { get; set; }
        public string ButtonURL { get; set; }
        public int Order { get; set; }
        public ListViewModel(int id, string mainTitle, string content, string imageURL, string buttonName, string buttonURL, int order)
        {
            Id = id;
            MainTitle = mainTitle;
            Content = content;
            ImageURL = imageURL;
            ButtonName = buttonName;
            ButtonURL = buttonURL;
            Order = order;
        }
    }
}
