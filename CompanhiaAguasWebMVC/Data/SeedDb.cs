using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        private Random _random;

        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            
        }
    }
}
