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
            CreateMap<Restaurant, RestaurantDto>();

            CreateMap<Customer, CustomerDto>();
            //.ForMember(c => c.Id
            //, opt => opt.MapFrom(r => r.Id));

            CreateMap<RestaurantForCreationDto, Restaurant>();
            CreateMap<RestaurantForUpdateDto, Restaurant>();
            CreateMap<CustomerForCreationDto, Customer>();
            CreateMap<CustomerForUpdateDto, Customer>();
            CreateMap<CustomerForUpdateDto, Customer>().ReverseMap();
        }
    }
}
