using CompanhiaAguasWebMVC.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace CompanhiaAguasWebMVC.Data
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        public IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboClients();

    }
}
