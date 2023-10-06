using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ClienteTelRepository : GenericRepository<ClienteTelefono>, IClienteTelRepository
    {
        private readonly AnimalsContext _context;

        public ClienteTelRepository(AnimalsContext context) : base(context)
        {
            _context = context;
        }
    }
}