using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class RestaurantCollectionBadRequest:BadRequestException
    {
        public RestaurantCollectionBadRequest() :
            base("Restaurant collection is null")
        {
        }
    }
}
