using CompanhiaAguasWebMVC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Data
{
    public interface IConsumptionRepository : IGenericRepository<Consumption>
    {

        public IQueryable GetAllWithUsers();

        public IQueryable GetAllByClient(int id);

    }
}
