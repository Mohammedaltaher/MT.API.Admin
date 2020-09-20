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
          return  new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Camera, streetInfoDto>()
               .ForMember(p => p.Capacity, opt => opt.MapFrom(x => x.Street.Capacity))
               .ForMember(p => p.CurrentCars, opt => opt.MapFrom(x => x.Count));
            });

        }
    }
}