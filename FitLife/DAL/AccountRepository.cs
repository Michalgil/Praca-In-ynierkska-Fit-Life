using FitLife.Data;
using FitLife.Models;
using FitLife.ViewModels;
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

        public AccountRepository(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration, AplicationDbContext db)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
            this.db = db;
        }
        public async Task<String> Login(LoginJson loginModel)
        {
            var result = await signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = userManager.Users.FirstOrDefault(u => u.Email == loginModel.Email);
                return GenerateJwtToken(loginModel.Email, appUser);
            }

            return null;
        }

        public async Task<string> Register(RegisterJson registerModel)
        {
            var IfExists = userManager.FindByEmailAsync(registerModel.Email);
            if (IfExists != null)
            {
                return null;
            }
            ApplicationUser user = new ApplicationUser
            {

                UserName = registerModel.Name,
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

