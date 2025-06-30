using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppointmentDBContext _context;

        public AppointmentService(AppointmentDBContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAppointmentsAsync()
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Vaccine)
                .ToListAsync();
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Vaccine)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
        }

        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment?> UpdateAppointmentAsync(int id, Appointment appointment)
        {
            var existingAppointment = await _context.Appointments.FindAsync(id);
            if (existingAppointment == null) return null;

            // Update all fields
            existingAppointment.PatientId = appointment.PatientId;
            existingAppointment.VaccineId = appointment.VaccineId;
            existingAppointment.AppointmentDate = appointment.AppointmentDate;
            existingAppointment.DoseGiven = appointment.DoseGiven;
            existingAppointment.DoseNumber = appointment.DoseNumber;

            await _context.SaveChangesAsync();
            return existingAppointment;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return false;

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
