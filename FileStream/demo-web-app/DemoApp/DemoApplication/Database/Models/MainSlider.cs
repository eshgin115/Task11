﻿using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class MainSlider : BaseEntity<int>, IAuditable
    {
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public string? ImageName { get; set; } //<original_name>.<extension>
        public string? ImageNameInFileSystem { get; set; } //Guid.<extension>
        public string ButtonName { get; set; }
        public string ButtonURL { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get ; set; }
        public DateTime UpdatedAt { get ; set ; }
    }
}
