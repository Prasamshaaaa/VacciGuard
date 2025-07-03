using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTOs;
using Server.Interfaces;
using Server.Models;
using Server.Common;
using System;
using System.Threading.Tasks;
using Server.Configurations;
using Microsoft.Extensions.Options;

namespace Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthDBContext _context;
        private readonly InvitationCodesConfig _invitationCodes;
        public AuthService(AuthDBContext context, IOptions<InvitationCodesConfig> invitationCodes)
        {
            _context = context;
            _invitationCodes = invitationCodes.Value;
        }

        public async Task<ApiResponse<ApplicationUser>> RegisterAsync(RegisterDto RegisterDto)
        {
            var response = new ApiResponse<ApplicationUser>();

            if (RegisterDto == null)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = "Invalid registration data.";
                return response;
            }

            bool userExists = await _context.ApplicationUsers.AnyAsync(u => u.Username == RegisterDto.Username);
            if (userExists)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = "Username already exists.";
                return response;
            }

            var newUser = new ApplicationUser
            {
                Username = RegisterDto.Username,
            };

            CreatePasswordHash(RegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            if (!string.IsNullOrEmpty(RegisterDto.InvitationCode))
            {
                if (RegisterDto.InvitationCode == _invitationCodes.AdminCode)
                    newUser.Role = _invitationCodes.AdminRole;
                else if (RegisterDto.InvitationCode == _invitationCodes.StaffCode)
                    newUser.Role = _invitationCodes.StaffRole;
                else
                    newUser.Role = _invitationCodes.UserRole;  
            }
            else
            {
                newUser.Role = _invitationCodes.UserRole;  // default role from config
            }


            _context.ApplicationUsers.Add(newUser);
            await _context.SaveChangesAsync();

            response.Status = ResponseStatusText.OK;
            response.Results = newUser;
            return response;
        }

        public async Task<ApiResponse<ApplicationUser>> LoginAsync(LoginDto LoginDto)
        {
            var response = new ApiResponse<ApplicationUser>();

            if (LoginDto == null || string.IsNullOrWhiteSpace(LoginDto.Username) || string.IsNullOrWhiteSpace(LoginDto.Password))
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = "Invalid login data.";
                return response;
            }

            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Username == LoginDto.Username);
            if (user == null)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = "User not found.";
                return response;
            }

            bool passwordValid = VerifyPasswordHash(LoginDto.Password, user.PasswordHash, user.PasswordSalt);
            if (!passwordValid)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = "Incorrect password.";
                return response;
            }

            response.Status = ResponseStatusText.OK;
            response.Results = user;
            return response;
        }

        public async Task<ApiResponse<bool>> ChangePasswordAsync(ChangePasswordDto ChangePasswordDto, string currentUsername)
        {
            var response = new ApiResponse<bool>();

            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Username == currentUsername);
            if (user == null)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = "User not found.";
                response.Results = false;
                return response;
            }

            bool oldPasswordCorrect = VerifyPasswordHash(ChangePasswordDto.OldPassword, user.PasswordHash, user.PasswordSalt);
            if (!oldPasswordCorrect)
            {
                response.Status = ResponseStatusText.Failed;
                response.ErrorMessage = "Old password is incorrect.";
                response.Results = false;
                return response;
            }

            CreatePasswordHash(ChangePasswordDto.NewPassword, out byte[] newHash, out byte[] newSalt);
            user.PasswordHash = newHash;
            user.PasswordSalt = newSalt;

            _context.ApplicationUsers.Update(user);
            await _context.SaveChangesAsync();

            response.Status = ResponseStatusText.OK;
            response.Results = true;
            return response;
        }

        // Helpers
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
    }
}
