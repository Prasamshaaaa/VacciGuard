using Microsoft.AspNetCore.Mvc;
using Server.Common;
using Server.DTOs;
using Server.Interfaces;
using Server.Models;
using System;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var response = new ApiResponse<ApplicationUser>();

            try
            {
                var result = await _authService.RegisterAsync(registerDto);

                if (result.Status == ResponseStatusText.Failed)
                {
                    response.Status = ResponseStatusText.Failed;
                    response.ErrorMessage = result.ErrorMessage ?? "Username already exists.";
                    return BadRequest(response);
                }

                response.Status = ResponseStatusText.OK;
                response.Results = result.Results;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var response = new ApiResponse<ApplicationUser>();
            try
            {
                var result = await _authService.LoginAsync(dto);
                if (result.Status == ResponseStatusText.Failed)
                {
                    response.Status = ResponseStatusText.Failed;
                    response.ErrorMessage = result.ErrorMessage ?? "Invalid username or password.";
                    return Unauthorized(response);
                }
                response.Status = ResponseStatusText.OK;
                response.Results = result.Results;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var result = await _authService.ChangePasswordAsync(dto, dto.Username);
                if (result.Status == ResponseStatusText.Failed)
                {
                    response.Status = ResponseStatusText.Failed;
                    response.ErrorMessage = result.ErrorMessage ?? "Password change failed.";
                    response.Results = false;
                    return BadRequest(response);
                }
                response.Status = ResponseStatusText.OK;
                response.Results = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = ex.Message;
                response.Results = false;
                return BadRequest(response);
            }
        }
    }
}
