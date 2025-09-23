using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Domain.Entities
{
    public class Drug
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ATCCode { get; set; }     // WHO ATC classification code
        public string Strength { get; set; }    // e.g. "500mg"
        public string Unit { get; set; }        // e.g. "mg"
        public string Forms { get; set; }       // JSON/Text: e.g. ["Tablet","Capsule"]
        public string Interactions { get; set; } // JSON/Text
        public bool IsOTC { get; set; }         // Over-the-counter
        public decimal Price { get; set; }

        // Navigation
        public ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
    }
}
