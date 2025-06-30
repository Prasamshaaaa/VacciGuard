using Server.Models;

namespace Server.Interfaces
{
    public interface IAppointmentService
    {

        Task<List<Appointment>> GetAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(int id);
        Task<Appointment> AddAppointmentAsync(Appointment appointment);
        Task<Appointment?> UpdateAppointmentAsync(int id, Appointment appointment);
        Task<bool> DeleteAppointmentAsync(int id);

    }
}
