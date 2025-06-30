using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common;
using Server.Interfaces;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaccineController : Controller
    {
        private readonly IVaccineService _service;

        public VaccineController(IVaccineService service)
        {
            _service = service;
        }

        // ===== Vaccines =====

        [HttpGet("vaccines")]
        public async Task<IActionResult> GetAllVaccines()
        {
            var response = new ApiResponse<List<Vaccine>>();
            try
            {
                var result = await _service.GetAllVaccinesAsync();
                response.Status = ResponseStatusText.OK;
                response.Results = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("vaccines/{id}")]
        public async Task<IActionResult> GetVaccine(int id)
        {
            var response = new ApiResponse<Vaccine>();
            try
            {
                var vaccine = await _service.GetVaccineByIdAsync(id);
                if (vaccine == null)
                {
                    response.Status = ResponseStatusText.Failed;
                    response.ErrorMessage = "Vaccine not found.";
                    return NotFound(response);
                }

                response.Status = ResponseStatusText.OK;
                response.Results = vaccine;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("vaccines")]
        public async Task<IActionResult> AddVaccine(Vaccine vaccine)
        {
            var response = new ApiResponse<Vaccine>();
            try
            {
                var result = await _service.AddVaccineAsync(vaccine);
                response.Status = ResponseStatusText.OK;
                response.Results = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut("vaccines/{id}")]
        public async Task<IActionResult> UpdateVaccine(int id, Vaccine vaccine)
        {
            var response = new ApiResponse<Vaccine>();
            try
            {
                var updated = await _service.UpdateVaccineAsync(id, vaccine);
                if (updated == null)
                {
                    response.Status = ResponseStatusText.Failed;
                    response.ErrorMessage = "Vaccine not found.";
                    return NotFound(response);
                }

                response.Status = ResponseStatusText.OK;
                response.Results = updated;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete("vaccines/{id}")]
        public async Task<IActionResult> DeleteVaccine(int id)
        {
            var response = new ApiResponse<object>();
            try
            {
                var deleted = await _service.DeleteVaccineAsync(id);
                if (!deleted)
                {
                    response.Status = ResponseStatusText.Failed;
                    response.ErrorMessage = "Vaccine not found.";
                    return NotFound(response);
                }

                response.Status = ResponseStatusText.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }

    }
}
