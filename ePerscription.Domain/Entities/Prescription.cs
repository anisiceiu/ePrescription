using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Domain.Entities
{
    public class Prescription
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }   // Could map to User/Doctor entity later
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string PDFPath { get; set; }
        public string Status { get; set; }

        // Navigation
        public Patient Patient { get; set; }
        public ICollection<PrescriptionItem> Items { get; set; } = new List<PrescriptionItem>();
    }
}
