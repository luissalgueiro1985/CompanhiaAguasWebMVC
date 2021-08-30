using CompanhiaAguasWebMVC.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }



        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
