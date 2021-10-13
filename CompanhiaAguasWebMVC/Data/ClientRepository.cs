using CompanhiaAguasWebMVC.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Data
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly DataContext _context;

        public ClientRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Clients.Include(p => p.User);
        }

        public async Task<Client> GetClientByEmail(string email)
        {
            return await _context.Clients.Where(c => c.Email == email).FirstOrDefaultAsync();
        }

        public IEnumerable<SelectListItem> GetComboClients()
        {
            var list = _context.Clients.Select(c => new SelectListItem
            {
                Text = c.FirstName,
                Value = c.Id.ToString()

            }).OrderBy(l => l.Text).ToList();

            

            return list;
        }
    }
}
