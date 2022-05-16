using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class RestaurantNotFoundException : NotFoundException
    {
        public RestaurantNotFoundException(Guid restaurantId) :
            base($"Restaurant with id: {restaurantId} does not exist.")
        {

        }
    }
}
