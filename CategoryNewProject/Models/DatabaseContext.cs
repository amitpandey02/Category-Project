using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CategoryNewProject.Models
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Category> Categories { get; set;}
        public DbSet<Product> Products { get;set;}
        public DbSet<Credential> Credentials { get; set;}
        public DbSet<Role> Roles { get; set;}
    }
}