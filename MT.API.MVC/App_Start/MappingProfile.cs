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
                 cfg.CreateMap<Street, GetAllStreetDetailsDto>()
               .ForMember(p => p.CityName, opt => opt.MapFrom(x => x.City.Name))
               ;
                 cfg.CreateMap<StreetPosstion, StreetPossationsDto>()
              .ForMember(p => p.StreetName, opt => opt.MapFrom(x => x.Street.Capacity))
              .ForMember(p => p.CityName, opt => opt.MapFrom(x => x.Street.City.Name))
              .ForMember(p => p.Capcity, opt => opt.MapFrom(x => x.Street.Capacity))
              .ForMember(p => p.CarsCount, opt => opt.MapFrom(x => x.Street.Cameras.Where(z=>z.IsInStreetBegaining == true).FirstOrDefault().CameraRequests.Sum(c=>c.InCount) + x.Street.Cameras.Where(z => z.IsInStreetBegaining == false).FirstOrDefault().CameraRequests.Sum(c => c.OutCount)))
              ;

                 cfg.CreateMap<CameraRequestDto, CameraRequest>();
                 cfg.CreateMap<User, UserLoginDto>()
              .ForMember(p => p.UserTypeName, opt => opt.MapFrom(x => x.UserType.Name))
                 ;

             });

        }
    }
}