using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAManagment.Application.Dtos;

namespace TAManagment.Application.IServices;
public interface IAuthService
{
    public Task<string> Authentication(LoginDto loginDto);
    public Task<int> Register(RegisterDto registerDto);
    public Task<bool> ChangePassword(ChangePasswordDto changePasswordDto);

    public Task<UserDto> GetUserDetails(int userId);
    public Task<bool> CheckPassowrd(int userId, string password);

}
