using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ClienteDirRepository : GenericRepository<ClienteDireccion>, IClienteDirRepository
    {
        private readonly AnimalsContext _context;

        public ClienteDirRepository(AnimalsContext context) : base(context)
        {
            _context = context;
        }
    }
}