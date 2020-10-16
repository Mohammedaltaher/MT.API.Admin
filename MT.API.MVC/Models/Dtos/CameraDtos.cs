using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MT.API.MVC.Models.Dtos
{
    public class CameraRequestDto
    {
        public int CameraID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> InCount { get; set; }
        public Nullable<int> OutCount { get; set; }
    }

    public class streetInfoResponseDto { 

      public IEnumerable<streetInfoDto> Data { get; set; }
    }
    public class streetInfoDto
    {
        public string StreetName { get; set; }
        public int CurrentCars { get; set; }
        public int Capacity { get; set; }
    }
    public class StreetPossationsResponseDto
    {

        public IEnumerable<StreetPossationsDto> Data { get; set; }
    }
    public class StreetPossationsDto
    {
        public int Id { get; set; }
        public int StreetID { get; set; }
        public string StreetName { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public int Capcity { get; set; }
    }
}