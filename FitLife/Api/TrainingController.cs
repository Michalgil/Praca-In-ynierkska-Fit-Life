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
        [HttpGet("getTraining")]
        public async Task<IActionResult> GetTraining()
        {
            var trainingPlan = await trainingService.GetTrainingPlan();
            if (trainingPlan == null)
            {
                return new BadRequestResult();
            }
            return Ok(trainingPlan);
        }
        [HttpGet("quantitytOfTrainings")]
        public async Task<IActionResult> GetQunatityOfTrainings()
        {
            try
            {
                var quantity = await trainingService.GetNumberOfTrainings();
                return Ok(quantity);
            }
            catch
            {
                return new BadRequestResult();
            }
        }
        [HttpGet("updateTraining")]
        public async Task<IActionResult> UpdateTraining()
        {
            var trainingPlan = await trainingService.updateTraining();
            if (trainingPlan.Count() == 0)
            {
                return new BadRequestResult();
            }
            return Ok(trainingPlan);
        }
        [HttpGet("getExercises")]
        public IActionResult GetExercises()
        {
            var exercisesList = trainingService.GetExercises();
            if (exercisesList.Count() == 0)
            {
                return new BadRequestResult();
            }
            return Ok(exercisesList);
        }

    }
}
