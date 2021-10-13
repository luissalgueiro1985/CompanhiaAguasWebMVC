using CompanhiaAguasWebMVC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Data
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        public IQueryable GetAllWithUsers();

        public IQueryable GetAllInvoicesAsync();

        Task<Invoice> GetLastInvoice();
    }
}
