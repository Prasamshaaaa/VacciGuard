using Microsoft.EntityFrameworkCore;
using Server.Models;
namespace Server.Data
{
    public class VaccinationDBContext : DbContext
    {

        public VaccinationDBContext(DbContextOptions<VaccinationDBContext> options) : base(options)
        {

        }

        public DbSet<Vaccine> Vaccines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()    // Configuring the Appointment entity
                .HasOne(a => a.Patient)            // Appointment has one Patient
                .WithMany()                        // Patient can have many Appointments (default)
                .HasForeignKey(a => a.PatientId); // The foreign key in Appointment is PatientId

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Vaccine)            // Appointment has one Vaccine
                .WithMany()                        // Vaccine can have many Appointments (default)
                .HasForeignKey(a => a.VaccineId); // The foreign key in Appointment is VaccineId
        }


    }
}
