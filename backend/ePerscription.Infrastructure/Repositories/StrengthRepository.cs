using ePerscription.Domain.Entities;
using ePerscription.Domain.Interfaces;
using ePerscription.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Infrastructure.Repositories
{
    public class StrengthRepository : GenericRepository<Strength>, IStrengthRepository
    {
        private readonly EPrescriptionContext _context;

        public StrengthRepository(EPrescriptionContext context) : base(context)
        {
            _context = context;
        }
    }
}
