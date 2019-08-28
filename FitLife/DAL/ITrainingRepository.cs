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
        Task AddTrainingExercise(TrainingExercise te);
        Task AddExercise(Exercise e);
        Task Save();
        Task<IEnumerable<Exercise>> GetExercises();
        Task<IEnumerable<Exercise>> GetBackExercises();
        Task<IEnumerable<Exercise>> GetTricepsExercises();
        Task<IEnumerable<Exercise>> GetBicepsExercises();
        Task<IEnumerable<Exercise>> GetChestExercises();
        Task<IEnumerable<Exercise>> GetLegsExercises();
        Task<IEnumerable<Exercise>> GetShouldersExercises();
        Task<IEnumerable<Exercise>> GetButtocksExercises();
        Task<IEnumerable<Exercise>> GetStomachExercises();

        Task AddData(List<Exercise> l1);
        PartOfBody getPartOfBodyId(string name);

    }
}
