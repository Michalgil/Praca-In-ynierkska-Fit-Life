using FitLife.Data;
using FitLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.DAL
{
    public class TrainingRepository : ITrainingRepository, IDisposable
    {
        private AplicationDbContext db;

        public TrainingRepository(AplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddTraining(Training tr)
        {
            await db.Trainings.AddAsync(tr);
        }
        public async Task AddTrainingExercise(TrainingExercise te)
        {
            await db.TrainingExercises.AddAsync(te);
        }
        public async Task AddExercise(Exercise e)
        {
            await db.Exercises.AddAsync(e);
        }
        public async Task AddPartOfBody(PartOfBody p)
        {
            await db.PartsOfBody.AddAsync(p);
        }
        public async Task<IEnumerable<Exercise>> GetExercises()
        {
            return await Task.Run(() => { return db.Exercises.ToList(); });
        }
        public async Task<IEnumerable<Exercise>> GetBackExercises()
        {
            var exercises = await Task.Run(() => { return db.Exercises.Where(p => p.PartOfBody.Name == "Plecy").ToList(); });
            foreach (Exercise e in exercises)
            {
                e.PartOfBody = db.PartsOfBody.Where(p => p.Name == "Plecy").FirstOrDefault();
            }

            return exercises;
        }
        public async Task<IEnumerable<Exercise>> GetBicepsExercises()
        {
            var exercises = await Task.Run(() => { return db.Exercises.Where(p => p.PartOfBody.Name == "Biceps").ToList(); });
            foreach (Exercise e in exercises)
            {
                e.PartOfBody = db.PartsOfBody.Where(p => p.Name == "Biceps").FirstOrDefault();
            }

            return exercises;
        }
        public async Task<IEnumerable<Exercise>> GetTricepsExercises()
        {
            var exercises = await Task.Run(() => { return db.Exercises.Where(p => p.PartOfBody.Name == "Triceps").ToList(); });
            foreach (Exercise e in exercises)
            {
                e.PartOfBody = db.PartsOfBody.Where(p => p.Name == "Triceps").FirstOrDefault();
            }

            return exercises;
        }
        public async Task<IEnumerable<Exercise>> GetChestExercises()
        {
            var exercises = await Task.Run(() => { return db.Exercises.Where(p => p.PartOfBody.Name == "Klatka piersiowa").ToList(); });
            foreach (Exercise e in exercises)
            {
                e.PartOfBody = db.PartsOfBody.Where(p => p.Name == "Klatka piersiowa").FirstOrDefault();
            }

            return exercises;
        }
        public async Task<IEnumerable<Exercise>> GetLegsExercises()
        {
            var exercises = await Task.Run(() => { return db.Exercises.Where(p => p.PartOfBody.Name == "Nogi").ToList(); });
            foreach (Exercise e in exercises)
            {
                e.PartOfBody = db.PartsOfBody.Where(p => p.Name == "Nogi").FirstOrDefault();
            }

            return exercises;
        }
        public async Task<IEnumerable<Exercise>> GetShouldersExercises()
        {
            var exercises = await Task.Run(() => { return db.Exercises.Where(p => p.PartOfBody.Name == "Ramiona").ToList(); });
            foreach (Exercise e in exercises)
            {
                e.PartOfBody = db.PartsOfBody.Where(p => p.Name == "Ramiona").FirstOrDefault();
            }

            return exercises;
        }
        public async Task<IEnumerable<Exercise>> GetButtocksExercises()
        {
            var exercises = await Task.Run(() => { return db.Exercises.Where(p => p.PartOfBody.Name == "Pośladki").ToList(); });
            foreach (Exercise e in exercises)
            {
                e.PartOfBody = db.PartsOfBody.Where(p => p.Name == "Pośladki").FirstOrDefault();
            }

            return exercises;
        }
        public async Task<IEnumerable<Exercise>> GetStomachExercises()
        {
            var exercises = await Task.Run(() => { return db.Exercises.Where(p => p.PartOfBody.Name == "Brzuch").ToList(); });
            foreach (Exercise e in exercises)
            {
                e.PartOfBody = db.PartsOfBody.Where(p => p.Name == "Brzuch").FirstOrDefault();
            }

            return exercises;
        }

        public async Task AddData(List<Exercise> l1)
        {
            foreach(Exercise e in l1)
            {
                await db.Exercises.AddAsync(e);
            }
        }
        public PartOfBody getPartOfBodyId(string name)
        {
            var partOfbody = db.PartsOfBody.Where(p => p.Name == name).FirstOrDefault();
            return partOfbody;
        }
        
        public async Task Save()
        {
            await db.SaveChangesAsync();
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
