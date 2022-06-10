using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Customer
    {
        [Column("CustomerId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(60, ErrorMessage = "Max length for the name is 60.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Max length for the email is 100.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [MaxLength(20, ErrorMessage = "Max length for the phone is 20.")]
        public string? Phone { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public Guid RestaurantId { get; set; }

        [Required(ErrorMessage = "Age is a required field.")]
        [Range(6, 100, ErrorMessage = "Age must be between 6 and 100.")]
        public int Age { get; set; }

        public Restaurant? Restaurant { get; set; }
    }
}