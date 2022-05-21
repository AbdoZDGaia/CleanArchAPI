using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record RestaurantForCreationDto : RestaurantForManipulationDto
    {
        public IEnumerable<CustomerForCreationDto>? Customers { get; set; }
    }
}
