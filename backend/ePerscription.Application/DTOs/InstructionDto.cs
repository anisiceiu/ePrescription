using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Application.DTOs
{
    public class InstructionDto
    {
        public int InstructionId { get; set; }
        public string Label { get; set; } = null!;
    }
}
