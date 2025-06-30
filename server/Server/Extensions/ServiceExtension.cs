using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Interfaces;
using Server.Services;

namespace Server.Extensions
{
    public static class ServiceExtension
    {

        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<VaccinationDBContext>(options =>
            options.UseSqlServer(connectionString));

            services.AddDbContext<PatientDBContext>(options =>
            options.UseSqlServer(connectionString));


            services.AddDbContext<AppointmentDBContext>(options =>
            options.UseSqlServer(connectionString));


            services.AddDbContext<AuthDBContext>(options =>
            options.UseSqlServer(connectionString));

            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IVaccineService, VaccineService>();
            services.AddScoped<IAuthService, AuthService>();


        }

    }
}
