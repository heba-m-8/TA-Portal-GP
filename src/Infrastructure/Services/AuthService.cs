using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TAManagment.Application.Common.Interfaces;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;
using TAManagment.Domain.Entities.Identity;

namespace TAManagment.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApplicationDbContext _context;

        public AuthService(IApplicationDbContext context)
        {
            _context = context;
        }


        //generate JWT upon successful login
        public async Task<string?> Authentication(LoginDto loginDto)
        {
            //user must be registered (active) with correct credentials
            var user = await _context.User
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.IsActive && u.UniversityId == loginDto.UniversityId);

            if (user == null)
                return null;

            var hashedPassword = HashPassword(loginDto.UserPassword, user.Salt);

            if (!VerifyPassword(user.HashedPassword, hashedPassword))
                return null;

            return GenerateJwtToken(user);
        }


        //register users if their ID and password are in the DB
        public async Task<int> Register(RegisterDto registerDto)
        {
            var salt = GenerateSalt();
            var hashedPassword = HashPassword(registerDto.UserPassword, salt);

            var user = await _context.User.FirstOrDefaultAsync(u => u.UniversityId == registerDto.UniversityId 
                                                                && u.UserPassword == registerDto.UserPassword);

            if (user == null)
                return 0;
            if (user.IsActive)
                return -1;

            user.IsActive = true;
            user.Salt = salt;
            user.HashedPassword = hashedPassword;
            await _context.SaveChangesAsync(default);
            return user.Id;
        }


        //create salt using randomNumberGenerator class
        private byte[] GenerateSalt()
        {
            var salt = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }


        private byte[] HashPassword(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 32
                );
        }


        //check password upon login
        private bool VerifyPassword(byte[] savedHash, byte[] inputHash)
        {
            if (savedHash.Length != inputHash.Length)
                return false;

            for (int i = 0; i < savedHash.Length; i++)
            {
                if (savedHash[i] != inputHash[i])
                    return false;
            }

            return true;
        }


        //generate JWT upon user login
        private string GenerateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes("[My secret key, used to  verify the token, it can be any string]");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, user.UniversityId),
                    new Claim(ClaimTypes.Role, user.Role.RoleName),
                    new Claim("ID", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == changePasswordDto.userId);

            if (user == null)
                return false;

            byte[] salt = GenerateSalt();
            byte[] hashedPassword = HashPassword(changePasswordDto.NewPassword, salt);
            user.UserPassword = changePasswordDto.NewPassword;
            user.Salt = salt;
            user.HashedPassword = hashedPassword;
            
            await _context.SaveChangesAsync(default);

            return true;
        }



        public async Task<UserDto> GetUserDetails(int userId)
        {
            var user = await _context.User.Select( u=> new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                UserEmail=u.UserEmail,
                UniversityId = u.UniversityId,
                DepartmentName = u.Department.Name,
                RoleName=u.Role.RoleName,
                GPA=u.GPA
            }).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return new UserDto();


            return user;
            
        }


        public async Task<bool> CheckPassowrd(int userId , string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId && u.UserPassword == password);

            if (user == null)
                return false;


            return true;

        }

    }
}
