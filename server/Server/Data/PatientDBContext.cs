using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class PatientDBContext : DbContext
    {
        public PatientDBContext(DbContextOptions<PatientDBContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

    }
}
