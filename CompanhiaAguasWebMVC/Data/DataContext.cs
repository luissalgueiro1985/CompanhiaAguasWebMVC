using CompanhiaAguasWebMVC.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Consumption> Consumptions { get; set; }

        public DbSet<Invoice> Invoices { get; set; }



        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
