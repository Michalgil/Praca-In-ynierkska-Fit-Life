using FitLife.Data;
using FitLife.Models;
using FitLife.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FitLife.DAL
{
    public class AccountRepository : IAccountRepository, IDisposable
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private AplicationDbContext db;
        IHttpContextAccessor httpContextAccessor;

        public AccountRepository(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration, AplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<String> Login(LoginJson loginModel)
        {
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            var result = await signInManager.PasswordSignInAsync(user.UserName, loginModel.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = userManager.Users.FirstOrDefault(u => u.Email == user.Email);
                return GenerateJwtToken(user.Email, appUser);
            }

            return null;
        }

        public async Task<string> Register(RegisterJson registerModel)
        {
            var IfExists = await userManager.FindByEmailAsync(registerModel.Email);
            if (IfExists != null)
            {
                return null;
            }
            ApplicationUser user = new ApplicationUser
            {

                UserName = registerModel.UserName,
                isMale = registerModel.IsMale,
                Email = registerModel.Email
            };
            var result = await userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return GenerateJwtToken(user.Email, user);
            }

            return null;
        }
        public async Task<ApplicationUser> GetCurrentUser()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await userManager.FindByIdAsync(userId);
        }
        private string GenerateJwtToken(string email, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
             configuration["Jwt:Issuer"],
             claims,
             expires: DateTime.Now.AddMinutes(30),
             signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task SignOut()
        {
            
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

