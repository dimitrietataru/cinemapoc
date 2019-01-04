using AutoMapper;
using CMS.Models.Cinema;
using Core.Models;
using Core.Models.NoSql;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CMS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cinema, CinemaIndexViewModel>();
            CreateMap<Cinema, CinemaDetailsViewModel>();
            CreateMap<Cinema, CinemaDetailsViewModel>()
                .ForMember(dest => dest.Schedules, opt => opt
                    .MapFrom(src => JsonConvert.DeserializeObject<List<Schedule>>(src.Schedule)));
            CreateMap<Cinema, CinemaCreateViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Schedule, opt => opt
                    .MapFrom(src => JsonConvert.SerializeObject(src.Schedules)))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Cinema, CinemaEditViewModel>()
                .ForMember(dest => dest.Schedules, opt => opt
                    .MapFrom(src => JsonConvert.DeserializeObject<List<Schedule>>(src.Schedule)))
                .ReverseMap()
                .ForMember(dest => dest.Schedule, opt => opt
                    .MapFrom(src => JsonConvert.SerializeObject(src.Schedules)))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Cinema, CinemaDeleteViewModel>();
        }
    }
}
