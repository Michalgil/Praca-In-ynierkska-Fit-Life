using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FitLife.Models;
using FitLife.Services;
using FitLife.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FitLife.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietController : ControllerBase
    {
        private DietService dietService;

        public DietController(DietService dietService)
        {
            this.dietService = dietService;
        }
        [HttpPost("createDiet")]
        public async Task<IActionResult> CreateDiet([FromBody] DietDataJson dietData)
        {
            dietData.IsMale = true;
            var diet = await dietService.CreateDiet(dietData);
            if (diet == null)
            {
                return BadRequest();
            }

            return Ok(diet);
        }

        [HttpGet("getCurrentDiet")]
        [ProducesResponseType(typeof(Diet), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>  GetCurrentDiet()
        {
            var diet = await dietService.GetDiet();
            if(diet == null)
            {
                return NotFound();
            }
            return Ok(diet);
        }
        [HttpGet("getAllProducts")]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await dietService.GetAllProduct();
            if(products == null)
            {
                return BadRequest();
            }
            return Ok(new { product = products });
        }
    }
}
