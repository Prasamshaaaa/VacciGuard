using Server.Models;

namespace Server.Interfaces
{
    public interface IVaccineService
    {

        Task<List<Vaccine>> GetAllVaccinesAsync();
        Task<Vaccine?> GetVaccineByIdAsync(int id);
        Task<Vaccine> AddVaccineAsync(Vaccine vaccine);
        Task<Vaccine?> UpdateVaccineAsync(int id, Vaccine vaccine);
        Task<bool> DeleteVaccineAsync(int id);

    }
}
