﻿using CompanhiaAguasWebMVC.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
    }
}
