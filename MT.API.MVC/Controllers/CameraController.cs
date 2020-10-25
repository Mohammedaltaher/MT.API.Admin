using AutoMapper;
using MT.API.MVC.App_Start;
using MT.API.MVC.Models;
using MT.API.MVC.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
            var data = context.StreetPosstion.Where(c => c.StreetID == StreetId).OrderBy(x => x.Name).ToList();
            //  var streetInf = GetStreetInf(context.Street.Where(c => c.Id == StreetId).FirstOrDefault());
            StreetPossationsResponseDto response = new StreetPossationsResponseDto()
            {
                Data = mapper.Map<IEnumerable<StreetPosstion>, IEnumerable<StreetPossationsDto>>(data)
            };
            return Json(response);
        }
        [Route("api/GetAllStreetPosstions")]
        [HttpGet]
        public IHttpActionResult GetAllStreetPosstions()
        {
            var data = context.StreetPosstion.ToList();
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
                Data = CurrentStreetStatus.OrderByDescending(x=>x.TrafficJam)
            };

            return Json(response);
        }
        [Route("api/GetAllStreetDetails")]
        [HttpGet]
        public IHttpActionResult GetAllStreetDetails()
        {
            var data = context.Street.ToList();
            // get street cameras 
            AllStreetDetailsResponse response = new AllStreetDetailsResponse()
            {
                Data = mapper.Map<IEnumerable<Street>, IEnumerable<GetAllStreetDetailsDto>>(data)
            };

            return Json(response);
        }

        [Route("api/GetCurrentStreet")]
        [HttpGet]
        public IHttpActionResult GetCurrentStreet(int StreetId)
        {
            var CurrentStreetStatus = new List<AllCurrentStreetStatusDto>() { };

            CurrentStreetStatus.AddRange(GetStreetInf(context.Street.Where(x => x.Id == StreetId).FirstOrDefault()));

            AllCurrentStreetStatusResponseDto response = new AllCurrentStreetStatusResponseDto()
            {
                Data = CurrentStreetStatus.OrderByDescending(x => x.TrafficJam)
            };

            return Json(response);
        }

        [Route("api/GetTodayPerHour")]
        [HttpGet]
        public IHttpActionResult GetTodayPerHour(int StreetId)
        {
            var crowedPerDayDto = new List<CrowedPerDayDto>() { };

            crowedPerDayDto.AddRange(GetStreetDataPerDay(context.Street.Where(x => x.Id == StreetId).FirstOrDefault()));

            CrowedPerDayResponseDto response = new CrowedPerDayResponseDto()
            {
                Data = crowedPerDayDto.OrderByDescending(x => x.TrafficJam)
            };

            return Json(response);
        }
        [Route("api/GetTodayPerWeek")]
        [HttpGet]
        public IHttpActionResult GetTodayPerWeek(int StreetId)
        {
            var crowedPerDayDto = new List<CrowedPerDayDto>() { };

            crowedPerDayDto.AddRange(GetStreetDataPerWeek(context.Street.Where(x => x.Id == StreetId).FirstOrDefault()));

            CrowedPerDayResponseDto response = new CrowedPerDayResponseDto()
            {
                Data = crowedPerDayDto.OrderByDescending(x => x.TrafficJam)
            };

            return Json(response);
        }
        [Route("api/GetTodayPerYear")]
        [HttpGet]
        public IHttpActionResult GetTodayPerYear(int StreetId)
        {
            var crowedPerDayDto = new List<CrowedPerDayDto>() { };

            crowedPerDayDto.AddRange(GetStreetDataPerYear(context.Street.Where(x => x.Id == StreetId).FirstOrDefault()));

            CrowedPerDayResponseDto response = new CrowedPerDayResponseDto()
            {
                Data = crowedPerDayDto.OrderByDescending(x => x.TrafficJam)
            };

            return Json(response);
        }

        [Route("api/GetTodayPerCity")]
        [HttpGet]
        public IHttpActionResult GetTodayPerCity()
        {
            var crowedPerDayDto = new List<CrowedPerDayDto>() { };
            foreach (var item in context.City)
            {
                crowedPerDayDto.AddRange(GetStreetDataPerCity(item));
            }

            CrowedPerDayResponseDto response = new CrowedPerDayResponseDto()
            {
                Data = crowedPerDayDto.OrderByDescending(x => x.TrafficJam)
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
        /// <summary>
        /// 7 steen carscount in 7 am an dcroud percent 7 80%
        /// get cars count 7in - 7out
        /// </summary>
        /// <param name="street"></param>
        /// <returns></returns>
        static List<CrowedPerDayDto> GetStreetDataPerDay(Street street)
        {
            var CurrentStreetStatus = new List<CrowedPerDayDto>() { };

            var CameraCounts = street.Cameras.Count;

            var AtThebegaining = street.Cameras.Where(c => c.IsInStreetBegaining == true).FirstOrDefault();
            var AtTheEnd = street.Cameras.Where(c => c.IsInStreetBegaining == false).FirstOrDefault();
            for (int i = 0; i < 24; i++)
            {
                if (CameraCounts > 0 && AtThebegaining.CameraRequests.Count > 0 && AtTheEnd.CameraRequests.Count > 0)
                {
                    // DayOfWeek week = DayOfWeek.
                    var hourin = AtThebegaining.CameraRequests.FirstOrDefault().Date.Value.DayOfWeek;
                    var datyein = AtThebegaining.CameraRequests.FirstOrDefault().Date.Value.Date;
                    var dateTime = DateTime.Now.Date;
                    var inCountBegaining = AtThebegaining.CameraRequests.Where(w => w.Date.Value.Date == DateTime.Now.Date && w.Date.Value.Hour == i).Sum(s => s.InCount) ?? 0;
                    var outCountEnd = AtTheEnd.CameraRequests.Where(w => w.Date.Value.Date == DateTime.Now.Date && w.Date.Value.Hour == i).Sum(s => s.OutCount) ?? 0;
                    var goResuilt = inCountBegaining - outCountEnd;

                    var inCountEnd = AtTheEnd.CameraRequests.Where(w => w.Date.Value.Date == DateTime.Now.Date && w.Date.Value.Hour == i).Sum(s => s.InCount) ?? 0;
                    var outCountBegaining = AtThebegaining.CameraRequests.Where(w => w.Date.Value.Date == DateTime.Now.Date && w.Date.Value.Hour == i).Sum(s => s.OutCount) ?? 0;
                    var backResuilt = inCountEnd - outCountBegaining;

                    var carsCount = goResuilt + backResuilt;

                    CrowedPerDayDto todayHour = new CrowedPerDayDto
                    {
                        Value = i.ToString(),
                        carsCount = carsCount,
                        TrafficJam = ((double)carsCount / (double)street.Capacity) * 100,
                        streetId = street.Id
                    };
                    CurrentStreetStatus.Add(todayHour);
                }

            }
            return CurrentStreetStatus;
        }
        static List<CrowedPerDayDto> GetStreetDataPerWeek(Street street)
        {
            var CurrentStreetStatus = new List<CrowedPerDayDto>() { };

            var CameraCounts = street.Cameras.Count;

            var AtThebegaining = street.Cameras.Where(c => c.IsInStreetBegaining == true).FirstOrDefault();
            var AtTheEnd = street.Cameras.Where(c => c.IsInStreetBegaining == false).FirstOrDefault();

            for (int i = 0; i < 7; i++)
            {
                if (CameraCounts > 0 && AtThebegaining.CameraRequests.Count > 0 && AtTheEnd.CameraRequests.Count > 0)
                {
                    int hourin = AtThebegaining.CameraRequests.FirstOrDefault().Date.Value.Date.Hour;
                    var datyein = AtThebegaining.CameraRequests.FirstOrDefault().Date.Value.Date;
                    var dateTime = DateTime.Now.Date;
                    var inCountBegaining = AtThebegaining.CameraRequests.Where(w => w.Date.Value.Month == DateTime.Now.Month && (int)w.Date.Value.DayOfWeek == i).Sum(s => s.InCount) ?? 0;
                    var outCountEnd = AtTheEnd.CameraRequests.Where(w => w.Date.Value.Month == DateTime.Now.Month && (int)w.Date.Value.DayOfWeek == i).Sum(s => s.OutCount) ?? 0;
                    var goResuilt = inCountBegaining - outCountEnd;

                    var inCountEnd = AtTheEnd.CameraRequests.Where(w => w.Date.Value.Month == DateTime.Now.Month && (int)w.Date.Value.DayOfWeek == i).Sum(s => s.InCount) ?? 0;
                    var outCountBegaining = AtThebegaining.CameraRequests.Where(w => w.Date.Value.Month == DateTime.Now.Month && (int)w.Date.Value.DayOfWeek == i).Sum(s => s.OutCount) ?? 0;
                    var backResuilt = inCountEnd - outCountBegaining;

                    var carsCount = goResuilt + backResuilt;

                    CrowedPerDayDto todayHour = new CrowedPerDayDto
                    {
                        Value = ((DayOfWeek)i).ToString(),
                        carsCount = carsCount,
                        TrafficJam = ((double)carsCount / (double)street.Capacity) * 100,
                        streetId = street.Id
                    };
                    CurrentStreetStatus.Add(todayHour);
                }

            }
            return CurrentStreetStatus;
        }
        static List<CrowedPerDayDto> GetStreetDataPerYear(Street street)
        {
            var CurrentStreetStatus = new List<CrowedPerDayDto>() { };

            var CameraCounts = street.Cameras.Count;

            var AtThebegaining = street.Cameras.Where(c => c.IsInStreetBegaining == true).FirstOrDefault();
            var AtTheEnd = street.Cameras.Where(c => c.IsInStreetBegaining == false).FirstOrDefault();

            for (int i = 1; i < 13; i++)
            {
                if (CameraCounts > 0 && AtThebegaining.CameraRequests.Count > 0 && AtTheEnd.CameraRequests.Count > 0)
                {
                    int hourin = AtThebegaining.CameraRequests.FirstOrDefault().Date.Value.Date.Month;
                    var datyein = AtThebegaining.CameraRequests.FirstOrDefault().Date.Value.Date;
                    var dateTime = DateTime.Now.Date;
                    var inCountBegaining = AtThebegaining.CameraRequests.Where(w => w.Date.Value.Year == DateTime.Now.Year && w.Date.Value.Month == i).Sum(s => s.InCount) ?? 0;
                    var outCountEnd = AtTheEnd.CameraRequests.Where(w => w.Date.Value.Year == DateTime.Now.Year && w.Date.Value.Month == i).Sum(s => s.OutCount) ?? 0;
                    var goResuilt = inCountBegaining - outCountEnd;

                    var inCountEnd = AtTheEnd.CameraRequests.Where(w => w.Date.Value.Year == DateTime.Now.Year && w.Date.Value.Month == i).Sum(s => s.InCount) ?? 0;
                    var outCountBegaining = AtThebegaining.CameraRequests.Where(w => w.Date.Value.Year == DateTime.Now.Year && w.Date.Value.Month == i).Sum(s => s.OutCount) ?? 0;
                    var backResuilt = inCountEnd - outCountBegaining;

                    var carsCount = goResuilt + backResuilt;

                    CrowedPerDayDto todayHour = new CrowedPerDayDto
                    {
                        Value = (i).ToString(),
                        carsCount = carsCount,
                        TrafficJam = ((double)carsCount / (double)street.Capacity) * 100,
                        streetId = street.Id
                    };
                    CurrentStreetStatus.Add(todayHour);
                }

            }
            return CurrentStreetStatus;
        }
        // Infrastructure Authority get street crowd by day weeek month , determent what city needs a new street 
        static List<CrowedPerDayDto> GetStreetDataPerCity(City city)
        {
            var CurrentStreetStatus = new List<CrowedPerDayDto>() { };
            var carsCount = 0;
            double cityCapcity = 0;
            foreach (var street in city.Streets)
            {
                var CameraCounts = street.Cameras.Count;

                var AtThebegaining = street.Cameras.Where(c => c.IsInStreetBegaining == true).FirstOrDefault();
                var AtTheEnd = street.Cameras.Where(c => c.IsInStreetBegaining == false).FirstOrDefault();

                if (CameraCounts > 0 && AtThebegaining.CameraRequests.Count > 0 && AtTheEnd.CameraRequests.Count > 0)
                {
                    int hourin = AtThebegaining.CameraRequests.FirstOrDefault().Date.Value.Date.Month;
                    var datyein = AtThebegaining.CameraRequests.FirstOrDefault().Date.Value.Date;
                    var dateTime = DateTime.Now.Date;
                    var inCountBegaining = AtThebegaining.CameraRequests.Sum(s => s.InCount) ?? 0;
                    var outCountEnd = AtTheEnd.CameraRequests.Sum(s => s.OutCount) ?? 0;
                    var goResuilt = inCountBegaining - outCountEnd;

                    var inCountEnd = AtTheEnd.CameraRequests.Sum(s => s.InCount) ?? 0;
                    var outCountBegaining = AtThebegaining.CameraRequests.Sum(s => s.OutCount) ?? 0;
                    var backResuilt = inCountEnd - outCountBegaining;

                    carsCount += goResuilt + backResuilt;
                    cityCapcity += street.Capacity;

                }

            }
            double trafficJam = 0;
            if(carsCount != 0)
            {
                 trafficJam = ((double)carsCount / cityCapcity) * 100;
            }
            CrowedPerDayDto todayHour = new CrowedPerDayDto
            {
                Value = city.Name,
                carsCount = carsCount,
                TrafficJam = trafficJam,
                streetId = city.Id
            };
            CurrentStreetStatus.Add(todayHour);
            return CurrentStreetStatus;
        }
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
