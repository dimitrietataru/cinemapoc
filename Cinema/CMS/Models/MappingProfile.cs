using AutoMapper;
using CMS.Models.Cinema;
using Core.Models;

namespace CMS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cinema, CinemaIndexViewModel>();
            CreateMap<Cinema, CinemaDetailsViewModel>();
            CreateMap<Cinema, CinemaCreateViewModel>()
                .ReverseMap()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Cinema, CinemaEditViewModel>()
                .ReverseMap()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Cinema, CinemaDeleteViewModel>();
        }
    }
}
