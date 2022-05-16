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
                .ForMember(r => r.Id, opt => opt.MapFrom(r => r.Id));
        }
    }
}
