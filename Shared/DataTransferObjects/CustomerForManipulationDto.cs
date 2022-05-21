using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record CustomerForManipulationDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(60, ErrorMessage = "Max length for the name is 60.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Max length for the email is 100.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; init; }

        [Required(ErrorMessage = "Phone is required")]
        [MaxLength(20, ErrorMessage = "Max length for the phone is 20.")]
        public string? Phone { get; init; }
    }
}