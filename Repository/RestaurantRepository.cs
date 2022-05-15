using Contracts;
using Entities;

namespace Repository
{
    public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
