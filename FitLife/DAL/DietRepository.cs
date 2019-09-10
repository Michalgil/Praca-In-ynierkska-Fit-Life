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
        private Diet removeDiet;
        public DietRepository(AplicationDbContext db)
        {
            this.db = db;
        }
        public async Task RemoveProduct(int id)
        {
            var productToRemove = db.Products.Where(p => p.Id == id).FirstOrDefault();
            db.Products.Remove(productToRemove);
            await db.SaveChangesAsync();
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
            removeDiet = diet;
        }
        public async Task AddProduct(Product product)
        {
            await db.Products.AddAsync(product);
        }
        public async Task<Diet> GetDiet(string userId)
        {
            return await Task.Run(() => { return db.Diets.Where(d => d.ApplicationUserId == userId && d.IsActive == true).FirstOrDefault(); });
        }
        public async Task AddDimensions(Dimensions d)
        {
            await db.Dimensions.AddAsync(d);
        }
        public async Task<Dimensions> GetDimensions(string userId)
        {
            return await Task.Run(() => { return db.Dimensions.Where(d => d.ApplicationUserId == userId && d.isActive == true).FirstOrDefault(); });
        }
        public async Task ChangeDietsAndDimensionsStatus(string userId)
        {
            var diets = await Task.Run(() => { return db.Diets.Where(d => d.ApplicationUserId == userId && d.IsActive == true).ToList(); });
            foreach (Diet d in diets)
            {
                d.IsActive = false;
                db.Diets.Update(d);
            }
            var dimensions = await Task.Run(() => { return db.Dimensions.Where(d => d.ApplicationUserId == userId && d.isActive == true).ToList(); });
            foreach (Dimensions d in dimensions)
            {
                d.isActive = false;
                db.Dimensions.Update(d);
            }
            
        }
        public async Task Save()
        {
            await db.SaveChangesAsync();
            //if (removeDiet != null)
            //{
            //    db.Diets.Remove(removeDiet);
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
