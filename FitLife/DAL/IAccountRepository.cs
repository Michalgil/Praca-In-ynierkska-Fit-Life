using FitLife.Models;
using FitLife.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.DAL
{
    public interface IAccountRepository : IDisposable
    {
        Task<String> Login(LoginJson loginModel);
        Task<String> Register(RegisterJson registerJson);
        Task SignOut();
        Task<ApplicationUser> GetCurrentUser();
    }
}
