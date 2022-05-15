using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(
                new Customer
                {
                    Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                    Name = "Customer 1",
                    Email = "Customer1@test.com",
                    Phone = "123456789",
                    RestaurantId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                },
                new Customer
                {
                    Id = new Guid("b4b3e8a4-7b2b-4b8b-b7c8-024705497d4a"),
                    Name = "Customer 2",
                    Email = "Customer2@test.com",
                    Phone = "123456789",
                    RestaurantId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                },
                new Customer
                {
                    Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                    Name = "Customer 3",
                    Email = "Customer3@test.com",
                    Phone = "123456789",
                    RestaurantId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                }
                );
        }
    }
}
