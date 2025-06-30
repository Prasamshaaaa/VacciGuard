using Server.Common;
using Server.DTOs;
using Server.Models;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<ApplicationUser>> RegisterAsync(RegisterDto dto);
        Task<ApiResponse<ApplicationUser>> LoginAsync(LoginDto dto);
        Task<ApiResponse<bool>> ChangePasswordAsync(ChangePasswordDto dto, string currentUsername);
    }
}
