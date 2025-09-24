using Microsoft.EntityFrameworkCore;
using ePerscription.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Infrastructure.Data
{
    public class EPrescriptionContext : DbContext
    {
        public EPrescriptionContext(DbContextOptions<EPrescriptionContext> options)
            : base(options)
        {
        }

        // DbSets for all EPrescription tables
        public DbSet<Drug>  Drugs { get; set; }
        public DbSet<Patient>  Patients { get; set; }
        public DbSet<Prescription>  Prescriptions { get; set; }
        public DbSet<PrescriptionItem>  PrescriptionItems { get; set; }
        public DbSet<User>  Users { get; set; }
        public DbSet<DosageForm> DosageForms { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Strength> Strengths { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
