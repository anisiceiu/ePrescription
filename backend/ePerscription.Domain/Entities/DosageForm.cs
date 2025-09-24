using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Domain.Entities
{
    public class DosageForm
    {
        public int DosageFormId { get; set; }
        public string Name { get; set; } = null!;
    }
}
