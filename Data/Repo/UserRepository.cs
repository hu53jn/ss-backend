using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ss_backend.Dtos;
using ss_backend.Helpers;
using ss_backend.Interfaces;
using ss_backend.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ss_backend.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly AppSettings _appSettings;
        public UserRepository(DataContext context, AppSettings appSettings)
        {
            _context = context;
            _appSettings = appSettings;
        }

        public LoginResDto Authenticate(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == email && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var loginResDto = new LoginResDto();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            if(user.Role != null)
            {
                loginResDto.Role = user.Role;
            }
            loginResDto.Token = tokenHandler.WriteToken(token);
            loginResDto.Email = user.Email;

            return loginResDto;
        }

        public void RegisterUser(User user)
        {
            _context.Users.Add(user);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public User GetUserInfo(string email)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == email);

            return user;
        }
        public bool UniqueEmail(string email)
        {
            var user = _context.Users.Where(x => x.Email == email);
            if (user.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
