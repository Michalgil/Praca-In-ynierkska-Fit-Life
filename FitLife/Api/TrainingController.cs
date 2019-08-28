using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Services;
using FitLife.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FitLife.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private TrainingService trainingService;
        public TrainingController(TrainingService trainingService)
        {
            this.trainingService = trainingService;
        }
        // GET: api/<controller>
        [HttpPost("createTraining")]
        public async Task<IActionResult> CreateTrening([FromBody] TrainingDataJson trainingData)
        {
            var trainingPlan = await trainingService.CreateTrainingPlan(trainingData);

            if(trainingPlan == null)
            {
                return new BadRequestResult();
            }
            return Ok(trainingPlan);
        }
        
    }
}
