using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAPIProject.Models
{
    public class AppDbContextDepartment
    {
        public AppDbContextDepartment() : base()
        { }
        public DbSet<Department> Department { get; set; }
    }
}