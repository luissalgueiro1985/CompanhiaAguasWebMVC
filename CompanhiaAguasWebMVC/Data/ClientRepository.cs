using CompanhiaAguasWebMVC.Data.Entities;

namespace CompanhiaAguasWebMVC.Data
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(DataContext context) : base(context)
        {

        }
    }
}
