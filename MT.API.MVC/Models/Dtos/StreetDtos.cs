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
        public IEnumerable<AllCurrentStreetStatusDto> Data { get; set; }
    }
    public class AllStreetDetailsResponse
    {
        public IEnumerable<GetAllStreetDetailsDto> Data { get; set; }
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
    public class GetAllStreetDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public int Capacity { get; set; }
    }
    public class CrowedPerDayResponseDto
    {
        public IEnumerable<CrowedPerDayDto> Data { get; set; }
    }
    public class CrowedPerDayDto
    {

        public int streetId { get; set; }
        public string Value { get; set; }
        public int carsCount { get; set; }
        public double TrafficJam { get; set; }
    }
}