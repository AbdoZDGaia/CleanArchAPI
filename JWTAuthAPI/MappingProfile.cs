using AutoMapper;
using Entities;
using Shared.DataTransferObjects;

namespace JWTAuthAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Source --> Destination
            CreateMap<Restaurant, RestaurantDto>()
                .ForCtorParam("Id", opt => opt.MapFrom(r => r.Id));

            CreateMap<Customer, CustomerDto>()
                .ForCtorParam("Id", opt => opt.MapFrom(r => r.Id));
        }
    }
}
