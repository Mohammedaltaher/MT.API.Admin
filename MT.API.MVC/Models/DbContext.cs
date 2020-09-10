using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MT.API.MVC.Models
{
    public class Context:DbContext
    {
        public Context() : base("MonitoringTrafficDB") { }
        public DbSet<City> City { get; set; }
        public DbSet<Camera> Camera { get; set; }
        public DbSet<Street> Street { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserType> UserType { get; set; }

    }
}