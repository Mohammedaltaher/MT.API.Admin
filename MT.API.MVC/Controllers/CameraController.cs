﻿using AutoMapper;
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
            var data = context.CameraRequest.Where(w => w.Camera.Street.Name.Contains(streetName)).ToList();
            streetInfoResponseDto response = new streetInfoResponseDto()
            {
                Data = mapper.Map<IEnumerable<CameraRequest>, IEnumerable<streetInfoDto>>(data)
            };

            return Json(response);
        }
        [Route("api/GetCurrentStreetStatus")]
        [HttpGet]
        public IHttpActionResult GetStreet(string streetName)
        {
            var data = context.CameraRequest.Where(w => w.Camera.Street.Name.Contains(streetName)).ToList();
            streetInfoResponseDto response = new streetInfoResponseDto()
            {
                Data = mapper.Map<IEnumerable<CameraRequest>, IEnumerable<streetInfoDto>>(data)
            };

            return Json(response);
        }
        [Route("api/GetStreetPosstions")]
        [HttpGet]
        public IHttpActionResult GetStreetPosstions(int StreetId)
        {
            var data = context.StreetPosstion.Where(c => c.StreetID == StreetId).ToList();
          //  var streetInf = GetStreetInf(context.Street.Where(c => c.Id == StreetId).FirstOrDefault());
            StreetPossationsResponseDto response = new StreetPossationsResponseDto()
            {
                Data = mapper.Map<IEnumerable<StreetPosstion>, IEnumerable<StreetPossationsDto>>(data)
            };
            return Json(response);
        }

        [Route("api/GetCurrentAllStreetStatus")]
        [HttpGet]
        public IHttpActionResult GetAllCurrentStreetStatus()
        {
            var CurrentStreetStatus = new List<AllCurrentStreetStatusDto>() { };
            var streetList = context.Street.ToList();
            // get street cameras 
            foreach (var street in streetList)
            {

                CurrentStreetStatus.AddRange(GetStreetInf(street));
            }
            AllCurrentStreetStatusResponseDto response = new AllCurrentStreetStatusResponseDto()
            {
                Data = CurrentStreetStatus
            };

            return Json(response);
        }

        [Route("api/GetCurrentStreet")]
        [HttpGet]
        public IHttpActionResult GetCurrentStreet(int StreetId)
        {
            var CurrentStreetStatus = new List<AllCurrentStreetStatusDto>() { };
         
                CurrentStreetStatus.AddRange(GetStreetInf(context.Street.Where(x=>x.Id == StreetId).FirstOrDefault()));
         
            AllCurrentStreetStatusResponseDto response = new AllCurrentStreetStatusResponseDto()
            {
                Data = CurrentStreetStatus
            };

            return Json(response);
        }
        // POST api/values
        public IHttpActionResult Post([FromBody] CameraRequestDto req)
        {
            try
            {
                CameraRequest data = mapper.Map<CameraRequestDto, CameraRequest>(req);
                data.Date = DateTime.Now;
                context.CameraRequest.Add(data);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //get day crowd by hours / week crowd by day / year crowd by months for traffic man / street crowd
        // Infrastructure Authority get street crowd by day weeek month , determent what city needs a new street 
        // Citizen current crowded roud , show the map full of cars , show the expcted street crowd per day 



        static List<AllCurrentStreetStatusDto> GetStreetInf(Street street)
        {
            var CurrentStreetStatus = new List<AllCurrentStreetStatusDto>() { };

            var CameraCounts = street.Cameras.Count;

            var AtThebegaining = street.Cameras.Where(c => c.IsInStreetBegaining == true).FirstOrDefault();
            var AtTheEnd = street.Cameras.Where(c => c.IsInStreetBegaining == false).FirstOrDefault();

            if (CameraCounts > 0 && AtThebegaining.CameraRequests.Count > 0 && AtTheEnd.CameraRequests.Count > 0)
            {
                var inCountBegaining = AtThebegaining.CameraRequests.Where(w => w.Date.Value.Date == DateTime.Now.Date).Sum(s => s.InCount) ?? 0;
                var outCountEnd = AtTheEnd.CameraRequests.Where(w => w.Date.Value.Date == DateTime.Now.Date).Sum(s => s.OutCount) ?? 0;
                var goResuilt = inCountBegaining - outCountEnd;

                var inCountEnd = AtTheEnd.CameraRequests.Where(w => w.Date.Value.Date == DateTime.Now.Date).Sum(s => s.InCount) ?? 0;
                var outCountBegaining = AtThebegaining.CameraRequests.Where(w => w.Date.Value.Date == DateTime.Now.Date).Sum(s => s.OutCount) ?? 0;
                var backResuilt = inCountEnd - outCountBegaining;


                AllCurrentStreetStatusDto DataGo = new AllCurrentStreetStatusDto
                {
                    StreetID = street.Id,
                    CityName = street.City.Name,
                    StreetName = street.Name,
                    StreetCapacity = street.Capacity,
                    From = street.From,
                    To = street.To,
                    CarrentCars = goResuilt,
                    TrafficJam = ((double)goResuilt / (double)street.Capacity) * 100
                };

                AllCurrentStreetStatusDto DataBack = new AllCurrentStreetStatusDto
                {
                    StreetID = street.Id,
                    CityName = street.City.Name,
                    StreetName = street.Name,
                    StreetCapacity = street.Capacity,
                    From = street.To,
                    To = street.From,
                    CarrentCars = backResuilt,
                    TrafficJam = ((double)backResuilt / (double)street.Capacity) * 100
                };
                CurrentStreetStatus.Add(DataGo);
                CurrentStreetStatus.Add(DataBack);
            }
            return CurrentStreetStatus;
        }
    }
}
