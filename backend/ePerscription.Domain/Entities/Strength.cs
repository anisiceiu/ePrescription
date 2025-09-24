using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Domain.Entities
{
    public class Strength
    {
        public int StrengthId { get; set; }
        public string Value { get; set; } = null!;
    }

}
