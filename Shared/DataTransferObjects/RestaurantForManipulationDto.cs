using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record RestaurantForManipulationDto
    {
        [Required(ErrorMessage = "Restaurant name is required")]
        [MaxLength(60, ErrorMessage = "Restaurant name cannot be longer than 60 characters")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Restaurant location is required")]
        [MaxLength(60, ErrorMessage = "Restaurant location cannot be longer than 60 characters")]
        public string? Location { get; init; }
    }
}
