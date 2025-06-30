using Microsoft.AspNetCore.Mvc;
using Server.Common;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpGet("appointments")]
        public async Task<IActionResult> GetAppointments()
        {
            var response = new ApiResponse<List<Appointment>>();
            try
            {
                var result = await _service.GetAppointmentsAsync();
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

        [HttpGet("appointments/{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var response = new ApiResponse<Appointment>();
            try
            {
                var appointment = await _service.GetAppointmentByIdAsync(id);
                if (appointment == null)
                {
                    response.Status = ResponseStatusText.Failed;
                    response.ErrorMessage = "Appointment not found.";
                    return NotFound(response);
                }

                response.Status = ResponseStatusText.OK;
                response.Results = appointment;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("appointments")]
        public async Task<IActionResult> AddAppointment(Appointment appointment)
        {
            var response = new ApiResponse<Appointment>();
            try
            {
                var result = await _service.AddAppointmentAsync(appointment);
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

        [HttpPut("appointments/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointment appointment)
        {
            var response = new ApiResponse<Appointment>();
            try
            {
                var updated = await _service.UpdateAppointmentAsync(id, appointment);
                if (updated == null)
                {
                    response.Status = ResponseStatusText.Failed;
                    response.ErrorMessage = "Appointment not found.";
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

        [HttpDelete("appointments/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var response = new ApiResponse<object>();
            try
            {
                var deleted = await _service.DeleteAppointmentAsync(id);
                if (!deleted)
                {
                    response.Status = ResponseStatusText.Failed;
                    response.ErrorMessage = "Appointment not found.";
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
