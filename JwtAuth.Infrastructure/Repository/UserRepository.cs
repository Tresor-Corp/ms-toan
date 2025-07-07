using JwtAuth.Core.Entity;
using JwtAuth.Core.IRepository;
using JwtAuth.Core.Response;
using JwtAuth.Core.Setting;
using JwtAuth.Infrastructure.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private AuthSetting _setting;

        public UserRepository(ApplicationDbContext context, IOptions<AuthSetting> option)
        {
            _context = context;
            _setting = option.Value;
        }

        public async Task<BaseResponse<User>> GetUserProfile(Guid userId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == userId);
                if (user == null)
                {
                    throw new Exception("User Not Found");
                }
                return new BaseResponse<User>
                {
                    Status = 200,
                    Message = "Success",
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>
                {
                    Status = ex.HResult,
                    Message = ex.Message,
                };
            }
        }

        public async Task<BaseResponse<TokenResponse>> Login(string email, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(p => p.Email == email && p.Password == password);
                if (user == null) {
                    throw new Exception("User Not Found");
                }
                return new BaseResponse<TokenResponse>
                {
                    Status = 200,
                    Message = "Success",
                    Data = new TokenResponse
                    {
                        AccessToken = GenerateJwtToken(user)
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TokenResponse>
                {
                    Status = ex.HResult,
                    Message = ex.Message,
                };
            }
        }

        public async Task<BaseResponse<User>> Regiter(User user)
        {
            try
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return new BaseResponse<User>
                {
                    Status = 200,
                    Message = "Success",
                    Data = user
                };
            }
            catch (Exception ex) {
                return new BaseResponse<User>
                {
                    Status = ex.HResult,
                    Message = ex.Message,
                };
            }
        }
        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_setting.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                issuer: _setting.Issuer,
                audience: _setting.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_setting.ExpirationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
