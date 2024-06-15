using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMediaAppAPI.Data;
using SocialMediaAppAPI.Dto;
using SocialMediaAppAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMediaAppAPI.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<object>> Register(RegisterDTO userDto);
        Task<ServiceResponse<object>> Login(LoginDTO userDto);
    }

    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public UserService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<object>> Login(LoginDTO userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == userDto.Name);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
            {
                return new ServiceResponse<object>(null, "Invalid username or password.", false);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""); // Using UTF8 encoding

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new ServiceResponse<object>(new { Token = tokenHandler.WriteToken(token) }, "Login successful.");
        }

        public async Task<ServiceResponse<object>> Register(RegisterDTO userDto)
        {
            if (await _context.Users.AnyAsync(u => u.Name == userDto.Name))
            {
                return new ServiceResponse<object>(null, "Username already exists.", false);
            }

            var user = new User
            {
                Name = userDto.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password) // Hash password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<object>(null, "User registered successfully.");
        }
    }
}
