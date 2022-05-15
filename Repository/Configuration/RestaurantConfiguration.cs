using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasData(
                new Restaurant { Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), Name = "Restaurant 1", Location = "Location 1" },
                new Restaurant {Id= new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), Name = "Restaurant 2", Location = "Location 2" },
                new Restaurant { Id= new Guid("3d490b70-94be-4d35-9424-5248412c2ca4"), Name = "Restaurant 3", Location = "Location 3" }
                );
        }
    }
}
