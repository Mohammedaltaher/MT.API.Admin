using AutoMapper;
using MT.API.MVC.Models;
using MT.API.MVC.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace MT.API.MVC.Controllers
{
    public class CameraController : ApiController
    {
        Context context = new Context();

        public IHttpActionResult Get()
        {
             return Ok(context.Camera.ToList());
        }
        // POST api/values
        public IHttpActionResult Post([FromBody] CameraRequestDto req)
        {
            try
            {
                // var data =  Mapper.Map<CameraRequestDto, Camera>(req);
                Camera camrea = new Camera()
                {
                    IpAddress = req.IpAddress,
                    Diriction = req.Diriction,
                    IsDeleted = "N",
                    IsIn = req.IsIn,
                    Pin = req.Pin,
                    StreetID = req.StreetID,
                };
                context.Camera.Add(camrea);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
      
        }
    }
}
