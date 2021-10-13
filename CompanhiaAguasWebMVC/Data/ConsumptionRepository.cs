using CompanhiaAguasWebMVC.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CompanhiaAguasWebMVC.Data
{
    public class ConsumptionRepository : GenericRepository<Consumption>, IConsumptionRepository
    {
        private readonly DataContext _context;

        public ConsumptionRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public IQueryable GetAllWithUsers()
        {
            return _context.Consumptions.Include(p => p.User);
        }
    }
}
