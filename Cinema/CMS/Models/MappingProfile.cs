using AutoMapper;
using CMS.Models.Cinema;
using CMS.Models.CinemaMovie;
using CMS.Models.Movie;
using Core.Models;
using Core.Models.NoSql;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CMS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapAuditoriumModels();
            MapBookingModels();
            MapCinemaModels();
            MapCinemaMovieModels();
            MapEventModels();
            MapMovieModels();
        }

        private void MapAuditoriumModels()
        {
        }

        private void MapBookingModels()
        {
        }

        private void MapCinemaModels()
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

        private void MapCinemaMovieModels()
        {
            CreateMap<Movie, MovieAssignViewModel>();
            CreateMap<Cinema, CinemaAssignViewModel>()
                .AfterMap((s, d) => d.IsAssigned = false);
            CreateMap<CinemaAssignViewModel, KeyValuePair<Guid, bool>>()
                .ConstructUsing(cinema => new KeyValuePair<Guid, bool>(cinema.Id, cinema.IsAssigned));
        }

        private void MapEventModels()
        {
        }

        private void MapMovieModels()
        {
            CreateMap<Movie, MovieIndexViewModel>();
            CreateMap<Movie, MovieDetailsViewModel>();
            CreateMap<Movie, MovieCreateViewModel>().ReverseMap();
            CreateMap<Movie, MovieEditViewModel>().ReverseMap();
            CreateMap<Movie, MovieDeleteViewModel>();
        }
    }
}
