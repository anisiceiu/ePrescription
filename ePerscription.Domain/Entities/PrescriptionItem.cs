using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Domain.Entities
{
    public class PrescriptionItem
    {
        public int Id { get; set; }
        public int PrescriptionId { get; set; }
        public int DrugId { get; set; }
        public string Dose { get; set; }        // e.g. "500 mg"
        public string Form { get; set; }        // e.g. "Tablet"
        public string Frequency { get; set; }   // e.g. "BID"
        public string Duration { get; set; }    // e.g. "7 days"
        public string Instructions { get; set; }

        // Navigation
        public Prescription Prescription { get; set; }
        public Drug Drug { get; set; }
    }
}
