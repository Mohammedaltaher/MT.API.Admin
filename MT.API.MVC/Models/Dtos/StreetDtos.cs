﻿using System;
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
}