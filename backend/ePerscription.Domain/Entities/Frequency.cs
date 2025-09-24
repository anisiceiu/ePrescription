using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Domain.Entities
{
    public class Frequency
    {
        public int FrequencyId { get; set; }
        public string Label { get; set; } = null!;
    }
}
