﻿using AutoMapper;
using CMS.Models.Cinema;
using CMS.Models.Movie;
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
            MapAuditoriumModels();
            MapBookingModels();
            MapCinemaModels();
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

        private void MapEventModels()
        {
        }

        private void MapMovieModels()
        {
            CreateMap<Movie, MovieIndexViewModel>();
            CreateMap<Movie, MovieCreateViewModel>().ReverseMap();
            CreateMap<Movie, MovieEditViewModel>()
                .ForMember(dest => dest.CinemaId, opt => opt.Ignore());
            CreateMap<MovieEditViewModel, Movie>();
            CreateMap<Movie, MovieDetailsViewModel>();
        }
    }
}
