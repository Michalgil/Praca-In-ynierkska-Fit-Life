using FitLife.Data;
using FitLife.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.DAL
{
    public class TrainingRepository : ITrainingRepository, IDisposable
    {
        private AplicationDbContext db;
        List<Training> removeTrainingList = new List<Training>();

        public TrainingRepository(AplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddTraining(Training tr)
        {
             await Task.Run(() => { db.Trainings.AddAsync(tr); });
            removeTrainingList.Add(tr);
        }
        public int GetQunatityOfTrainings(string userID)
        {
            return db.Trainings.AsNoTracking().Where(t => t.ApplicationUserId == userID && t.IsActive == true).Count();
        }
        public IEnumerable<Training> GetTrainingList(string userID)
        {
            var trainingPlan = db.Trainings.AsNoTracking().Where(t => t.ApplicationUser.Id == userID && t.IsActive == true)
                                    .Select(t => new Training
                                    {
                                        Id = t.Id,
                                        ApplicationUser = t.ApplicationUser,
                                        Break = t.Break,
                                        IsAdvanced = t.IsAdvanced,
                                        IsBegginer = t.IsBegginer,
                                        IsIntermediate = t.IsIntermediate,
                                        TrainingExercises = db.TrainingExercises.AsNoTracking().Where(tr => tr.TrainingId == t.Id)
                                              .Select(tr => new TrainingExercise
                                              {
                                                  Training = t,
                                                  TrainingId = t.Id,
                                                  Exercise = db.Exercises.AsNoTracking().Where(e => e.Id == tr.ExerciseId).Select(ex => new Exercise
                                                  {
                                                      Id = ex.Id,
                                                      TrainingExercises = ex.TrainingExercises,
                                                      Name = ex.Name,
                                                      PartOfBody = db.PartsOfBody.AsNoTracking().Where(p => p.Id == ex.PartOfBody.Id).Select(pb => new PartOfBody
                                                      {
                                                          Id = pb.Id,
                                                          Name = pb.Name
                                                      }).FirstOrDefault()
                                                  }).FirstOrDefault()
                                              }).ToList()
                                    }).ToList();
            return trainingPlan;

        }
        public async Task<Training> GetTraining(string userId)
        {
            return await Task.Run(() => { return db.Trainings.AsNoTracking().Where( t => t.ApplicationUserId == userId && t.IsActive == true).FirstOrDefault(); });
        }
        public void SetTrainingStatus(string userID)
        {
            var trainingPlan = db.Trainings.Where(t => t.ApplicationUser.Id == userID && t.IsActive == true)
                                    .Select(t => new Training
                                    {
                                        Id = t.Id,
                                        ApplicationUser = t.ApplicationUser,
                                        Break = t.Break,
                                        IsAdvanced = t.IsAdvanced,
                                        IsBegginer = t.IsBegginer,
                                        IsIntermediate = t.IsIntermediate
                                    }).ToList();
            foreach (Training t in trainingPlan)
            {
                t.IsActive = false;
                db.Trainings.Update(t);
            }
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
        public IEnumerable<Exercise> GetExercises()
        {
            var exerciseList = db.Exercises
                                   .Select(t => new Exercise
                                   {
                                       Id = t.Id,
                                       Name = t.Name,
                                       PartOfBody = db.PartsOfBody.Where(p => p.Id == t.PartOfBody.Id).FirstOrDefault()
                                   }).ToList();
            
            return exerciseList;
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

        public async Task<bool> RemoveExercise(int id)
        {
            var ex = db.Exercises.Where(e => e.Id == id).FirstOrDefault();
            if (ex == null)
            {
                return false;
            }

            await db.Exercises.AddAsync(ex);
            return true;
        }


        public async Task AddData(List<Exercise> l1) // wywalic to do dodawnia cwiczen itp
        {
            foreach(Exercise e in l1)
            {
                await db.Exercises.AddAsync(e);
            }
        }
        public PartOfBody getPartOfBodyId(string name) // zmienic nazwe bez id bo zwraca obiekt a nie id
        {
            var partOfbody = db.PartsOfBody.Where(p => p.Name == name).FirstOrDefault();
            return partOfbody;
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();

            //if (removeTrainingList.Count > 0)
            //{
            //    foreach (Training t in removeTrainingList)
            //    {
            //        db.Trainings.Remove(t);
            //    }
            //    await db.SaveChangesAsync();
            //}
            
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
