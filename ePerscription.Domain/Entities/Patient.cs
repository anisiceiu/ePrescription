namespace ePerscription.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string MRN { get; set; }   // Medical Record Number
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Allergies { get; set; }  // JSON/Text
        public string Notes { get; set; }

        // Navigation
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
