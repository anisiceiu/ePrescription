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
    public class DosageFormRepository : GenericRepository<DosageForm>, IDosageFormRepository
    {
        private readonly EPrescriptionContext _context;

        public DosageFormRepository(EPrescriptionContext context) : base(context)
        {
            _context = context;
        }
    }
}
