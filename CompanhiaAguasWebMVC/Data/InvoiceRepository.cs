using CompanhiaAguasWebMVC.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Data
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly DataContext _context;

        public InvoiceRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllByClient(int id)
        {
            return _context.Invoices
                .Include(i => i.User)
                .Include(i => i.Client)
                .Include(i => i.Consumption)
                .Where(c => c.Client.Id == id);
                
        }

        public IQueryable GetAllInvoicesAsync()
        {
             
            return _context.Invoices
                .Include(i => i.User)
                .Include(i => i.Client)
                .Include(i => i.Consumption);
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Invoices.Include(p => p.User);
        }

        public async Task<Invoice> GetLastInvoice()
        {
            return await _context.Invoices
                .Include(i => i.Client)
                .Include(i => i.Consumption)
                .Include(i => i.User)
                .Where(i => i.IsPaid == false)
                .OrderByDescending(i => i.Id)
                .FirstOrDefaultAsync();
                
        }

        public async Task<bool> ExistInvoiceConsumptionAsync(int id)
        {
            return await _context.Invoices
                .AnyAsync(i => i.Consumption.Id == id);
                
        }
    }
}
