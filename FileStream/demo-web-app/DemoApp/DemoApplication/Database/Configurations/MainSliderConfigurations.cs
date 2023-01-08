using DemoApplication.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApplication.Database.Configurations
{
    public class MainSliderConfigurations : IEntityTypeConfiguration<MainSlider>
    {
        public void Configure(EntityTypeBuilder<MainSlider> builder)
        {
            builder
               .ToTable("MainSliders");
        }
    
    }
}
