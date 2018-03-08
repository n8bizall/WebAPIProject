using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAPIProject.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext() :base()
        {}
        public DbSet<Employee> Employees { get; set; }
    }
}