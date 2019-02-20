using AutoMapper;
using CMS.Models.Auditorium;
using CMS.Models.Auditorium.Partial;
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
			CreateMap<Auditorium, AuditoriumIndexViewModel>();
			CreateMap<Auditorium, AuditoriumDetailsViewModel>();
			CreateMap<Auditorium, AuditoriumCreateViewModel>()
				.IgnoreAllPropertiesWithAnInaccessibleSetter()
				.ForMember(dest => dest.AuditoriumSeats, opt => opt
					.MapFrom(src => src));
			CreateMap<AuditoriumCreateViewModel, Auditorium>()
				.IgnoreAllPropertiesWithAnInaccessibleSetter()
				.ForMember(dest => dest.Seats, opt => opt
					.MapFrom(src => JsonConvert.SerializeObject(src.AuditoriumSeats.Seats)));
			CreateMap<Auditorium, AuditoriumEditViewModel>()
				.BeforeMap((src, dest) => dest.AuditoriumSeats = new AuditoriumSeatsViewModel())
				.ForMember(dest => dest.AuditoriumSeats, opt => opt
				.MapFrom(src => src));
			CreateMap<AuditoriumEditViewModel, Auditorium>();
			CreateMap<Auditorium, AuditoriumDeleteViewModel>();
			CreateMap<Auditorium, AuditoriumSeatsViewModel>()
				.ForMember(dest => dest.Seats, opt => opt
					.MapFrom(src => JsonConvert.DeserializeObject<List<Seat>>(src.Seats)));

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
		}
	}
}
