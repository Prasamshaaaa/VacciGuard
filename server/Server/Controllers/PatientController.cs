using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }


        [Authorize]
        [HttpGet("patients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var response = new ApiResponse<List<Patient>>();
            try
            {
                var result = await _service.GetAllPatientsAsync();
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

        [HttpGet("patients/{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var response = new ApiResponse<Patient>();
            try
            {
                var patient = await _service.GetPatientByIdAsync(id);
                if (patient == null)
                {
                    response.Status = ResponseStatusText.Failed;
                    response.ErrorMessage = "Patient not found.";
                    return NotFound(response);
                }
                response.Status = ResponseStatusText.OK;
                response.Results = patient;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("patients")]
        public async Task<IActionResult> AddPatient([FromBody] Patient patient)
        {
            var response = new ApiResponse<Patient>();
            try
            {
                var pat = await _service.AddPatientAsync(patient);
                response.Status = ResponseStatusText.OK;
                response.Results = pat;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = "Error while adding patient: " + ex.Message;
                return BadRequest(response);
            }
        }


        [HttpPut("patients/{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            var response = new ApiResponse<Patient>();
            try
            {
                var updated = await _service.UpdatePatientAsync(id, patient);
                if (updated == null)
                {
                    response.Status = ResponseStatusText.Failed;
                    response.ErrorMessage = "Patient not found.";
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

        [HttpDelete("patients/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var response = new ApiResponse<object>();
            try
            {
                var deleted = await _service.DeletePatientAsync(id);
                if (!deleted)
                {
                    response.Status = ResponseStatusText.Failed;
                    response.ErrorMessage = "Patient not found.";
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
