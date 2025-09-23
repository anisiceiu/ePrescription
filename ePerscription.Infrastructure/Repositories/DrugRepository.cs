using ePerscription.Domain.Interfaces;
using ePerscription.Domain.Entities;
using ePerscription.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Infrastructure.Repositories
{
    public class DrugRepository: GenericRepository<Drug>, IDrugRepository
    {
        public DrugRepository(EPrescriptionContext context) : base(context)
        {
        }
    }
}
