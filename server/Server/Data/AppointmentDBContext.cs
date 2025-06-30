using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class AppointmentDBContext: DbContext
    {
public AppointmentDBContext(DbContextOptions<AppointmentDBContext> options) : base(options)
    {
    }

        public DbSet<Appointment> Appointments { get; set; }

    }
}
