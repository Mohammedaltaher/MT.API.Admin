using MT.API.MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MT.API.MVC.Controllers
{
    public class CitiesController : ApiController
    {
        Context context = new Context();

        public IHttpActionResult Get()
        {
            return Ok(context.City.ToList());
        }

    }
}
