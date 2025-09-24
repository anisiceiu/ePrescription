using ePerscription.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Domain.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
    }
}
