using AutoMapper;
using MT.API.MVC.App_Start;
using MT.API.MVC.Models;
using MT.API.MVC.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MT.API.MVC.Controllers
{
    public class CameraController : ApiController
    {
        Context context = new Context();
        private readonly IMapper mapper;
        public CameraController()
        {
            var config = MappingProfile.config();
            mapper = config.CreateMapper();
        }
        public IHttpActionResult Get()
        {
             return Ok(context.Camera.ToList());
        }
        [Route("api/Search")]
        [HttpGet]
        public IHttpActionResult Search(string streetName)
        {
            var data = context.Camera.Where(w => w.Street.Name.Contains(streetName)).ToList();
            streetInfoResponseDto response = new streetInfoResponseDto()
            {
                Data = mapper.Map< IEnumerable< Camera>, IEnumerable< streetInfoDto>>(data)
            };
        
            return Ok(response);
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
                    Count = req.Count,
                    Date=req.Date
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
