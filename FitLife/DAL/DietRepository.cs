using FitLife.Data;
using FitLife.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitLife.DAL
{
    public class DietRepository : IDietRepository, IDisposable
    {
        private AplicationDbContext db;
        public DietRepository(AplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await Task.Run(() => { return db.Products.ToList(); });
        }
        public async Task AddMeal(Meal meal)
        {
            await db.Meals.AddAsync(meal);
        }
        public async Task<IEnumerable<Meal>> GetDietMeals(int dietId)
        {
            return await Task.Run(() => { return db.Meals.Where(m => m.DietId == dietId).ToList(); });
        }
        public async Task AddDiet(Diet diet)
        {
            await db.Diets.AddAsync(diet);
        }
        public async Task AddProduct(Product product)
        {
            await db.Products.AddAsync(product);
        }
        public async Task<Diet> GetDiet(string userId)
        {
            return await Task.Run(() => { return db.Diets.Where(d => d.ApplicationUserId == userId).FirstOrDefault(); });
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
