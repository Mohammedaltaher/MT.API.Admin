using AutoMapper;
using MT.API.MVC.Models;
using MT.API.MVC.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MT.API.MVC.App_Start
{
    public class MappingProfile : Profile
    {
        public static MapperConfiguration config()
        {
            return new MapperConfiguration(cfg =>
             {
                 cfg.CreateMap<User, UserDto>();
                 cfg.CreateMap<CameraRequest, streetInfoDto>()
                .ForMember(p => p.Capacity, opt => opt.MapFrom(x => x.Camera.Street.Capacity))
                .ForMember(p => p.CurrentCars, opt => opt.MapFrom(x => x.InCount))
                .ForMember(p => p.StreetName, opt => opt.MapFrom(x => x.Camera.Street.Name))
                ;

                 cfg.CreateMap<StreetPosstion, StreetPossationsDto>()
              .ForMember(p => p.StreetName, opt => opt.MapFrom(x => x.Street.Capacity))
              .ForMember(p => p.CityName, opt => opt.MapFrom(x => x.Street.City.Name))
              .ForMember(p => p.Capcity, opt => opt.MapFrom(x => x.Street.Capacity))
              ;

                 cfg.CreateMap<CameraRequestDto, CameraRequest>();
                 cfg.CreateMap<User, UserLoginDto>()
              .ForMember(p => p.UserTypeName, opt => opt.MapFrom(x => x.UserType.Name))
                 ;

             });

        }
    }
}