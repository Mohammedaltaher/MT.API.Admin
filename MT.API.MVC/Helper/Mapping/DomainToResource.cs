﻿using AutoMapper;
using MT.API.MVC.Models;
using MT.API.MVC.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MT.API.MVC.Helper.Mapping
{
    public class DomainToResource:Profile
    {
        public DomainToResource()
        {
            CreateMap<User, UserDto>();
        }
    }
}