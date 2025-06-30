using Server.Models;

namespace Server.Interfaces
{
    public interface IPatientService
    {

        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(int id);
        Task<Patient> AddPatientAsync(Patient patient);
        Task<Patient?> UpdatePatientAsync(int id, Patient patient);
        Task<bool> DeletePatientAsync(int id);

    }
}
