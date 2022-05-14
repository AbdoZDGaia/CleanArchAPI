using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Restaurant
    {
        [Column("RestaurantId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Restaurant name is required")]
        [MaxLength(60, ErrorMessage = "Restaurant name cannot be longer than 60 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Restaurant location is required")]
        [MaxLength(60, ErrorMessage = "Restaurant location cannot be longer than 60 characters")]
        public string? Location { get; set; }

        public ICollection<Customer>? Customers { get; set; }
    }
}
