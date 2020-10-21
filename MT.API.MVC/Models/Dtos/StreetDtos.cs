using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MT.API.MVC.Models.Dtos
{
    public class StreetRequestDto
    {
        public int StreetID { get; set; }
        public string Pin { get; set; }
        public string IpAddress { get; set; }
        public string IsDeleted { get; set; }
        public Nullable<int> Diriction { get; set; }
        public Nullable<bool> IsIn { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Count { get; set; }
    }
    public class AllCurrentStreetStatusResponseDto
    {
        public List<AllCurrentStreetStatusDto> Data { get; set; }
    }
    public class AllCurrentStreetStatusDto
    {
        public int StreetID { get; set; }
        public string StreetName { get; set; }
        public string CityName { get; set; }
        public int StreetCapacity { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int CarrentCars { get; set; }
        public double TrafficJam { get; set; }

    }
    public class CrowedPerDayResponseDto
    {
        public List<CrowedPerDayDto> Data { get; set; }
    }
    public class CrowedPerDayDto
    {
        public int streetId { get; set; }
        public string hour { get; set; }
        public int carsCount { get; set; }
        public double TrafficJam { get; set; }
    }
}