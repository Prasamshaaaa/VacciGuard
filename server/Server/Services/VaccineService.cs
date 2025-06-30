using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Interfaces;
using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Services
{
    public class VaccineService : IVaccineService
    {
        private readonly VaccinationDBContext _context;

        public VaccineService(VaccinationDBContext context)
        {
            _context = context;
        }

 
       
        // ===== Vaccines =====

        public async Task<List<Vaccine>> GetAllVaccinesAsync()
        {
            return await _context.Vaccines.ToListAsync();
        }

        public async Task<Vaccine?> GetVaccineByIdAsync(int id)
        {
            return await _context.Vaccines.FindAsync(id);
        }

        public async Task<Vaccine> AddVaccineAsync(Vaccine vaccine)
        {
            _context.Vaccines.Add(vaccine);
            await _context.SaveChangesAsync();
            return vaccine;
        }

        public async Task<Vaccine?> UpdateVaccineAsync(int id, Vaccine vaccine)
        {
            var existingVaccine = await _context.Vaccines.FindAsync(id);
            if (existingVaccine == null) return null;

            // Update all fields
            existingVaccine.Name = vaccine.Name;
            existingVaccine.Manufacturer = vaccine.Manufacturer;
            existingVaccine.RequiredDoses = vaccine.RequiredDoses;

            await _context.SaveChangesAsync();
            return existingVaccine;
        }

        public async Task<bool> DeleteVaccineAsync(int id)
        {
            var vaccine = await _context.Vaccines.FindAsync(id);
            if (vaccine == null) return false;

            _context.Vaccines.Remove(vaccine);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
