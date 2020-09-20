using AutoMapper;
using MT.API.MVC.App_Start;
using MT.API.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MT.API.MVC.Controllers
{
    public class StreetsController : ApiController
    {
        Context context = new Context();
        private readonly IMapper mapper;
        public StreetsController()
        {
            var config = MappingProfile.config();
            mapper = config.CreateMapper();
        }
        public IHttpActionResult Get()
        {

            return Ok(context.Street.ToList());
        }

    }
}
