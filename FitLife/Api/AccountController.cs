using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitLife.DAL;
using FitLife.DTO;
using FitLife.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountRepository accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginJson loginJson)
        {
           var tokenValue = await accountRepository.Login(loginJson);
            if (tokenValue.GetType() == typeof(String) )
            {
                return Ok(new { token = tokenValue });
            }

            return new BadRequestResult();
        }
        [HttpPost("register")]
        public async Task<object> Register([FromBody] RegisterJson registerJson)
        {
            var tokenValue = await accountRepository.Register(registerJson);
            if (tokenValue.GetType() == typeof(String))
            {
                return Ok(new { token = tokenValue });
            }

            return new BadRequestResult();
        }
        [HttpGet("signout")]
        public async Task<IActionResult> SignOut()
        {
            try
            {
                await accountRepository.SignOut();
                return Ok();
            }
            catch
            {
                return new BadRequestResult();
            }
        }

    }
}