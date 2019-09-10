using FitLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.DAL
{
    public interface ITrainingRepository : IDisposable
    {
        Task AddTraining(Training tr);
        IEnumerable<Training> GetTrainingList(string userID);
        Task AddTrainingExercise(TrainingExercise te);
        Task AddExercise(Exercise e);
        Task Save();
        void SetTrainingStatus(string userID);
        Task<Training> GetTraining(string userId);
        IEnumerable<Exercise> GetExercises();
        Task<IEnumerable<Exercise>> GetBackExercises();
        Task<IEnumerable<Exercise>> GetTricepsExercises();
        Task<IEnumerable<Exercise>> GetBicepsExercises();
        Task<IEnumerable<Exercise>> GetChestExercises();
        Task<IEnumerable<Exercise>> GetLegsExercises();
        Task<IEnumerable<Exercise>> GetShouldersExercises();
        Task<IEnumerable<Exercise>> GetButtocksExercises();
        Task<IEnumerable<Exercise>> GetStomachExercises();
        int GetQunatityOfTrainings(string userID);
        PartOfBody getPartOfBodyId(string name);

    }
}
