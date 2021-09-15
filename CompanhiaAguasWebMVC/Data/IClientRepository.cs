using CompanhiaAguasWebMVC.Data.Entities;
using System.Linq;

namespace CompanhiaAguasWebMVC.Data
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        public IQueryable GetAllWithUsers();

    }
}
