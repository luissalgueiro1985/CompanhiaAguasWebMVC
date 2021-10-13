using CompanhiaAguasWebMVC.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Data
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        public IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboClients();

        Task<Client> GetClientByEmail(string email);

    }
}
