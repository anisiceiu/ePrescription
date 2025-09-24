using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Application.DTOs
{
    public class DosageFormDto
    {
        public int DosageFormId { get; set; }
        public string Name { get; set; } = null!;
    }
}
