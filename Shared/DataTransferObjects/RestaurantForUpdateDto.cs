using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record RestaurantForUpdateDto
    {
        public string? Name { get; init; }
        public string? Location { get; init; }
        public IEnumerable<CustomerForCreationDto>? Customers { get; set; }
    }
}
