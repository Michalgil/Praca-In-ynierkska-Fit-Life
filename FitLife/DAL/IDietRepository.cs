﻿using FitLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitLife.DAL
{
    public interface IDietRepository : IDisposable
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task AddMeal(Meal meal);
        Task<IEnumerable<Meal>> GetDietMeals(int dietId);
        Task AddDiet(Diet diet);
        Task ChangeDietsAndDimensionsStatus(string userId);
        Task AddProduct(Product product);
        Task<Diet> GetDiet(string userId);
        Task Save();
        Task AddDimensions(Dimensions d);
        Task<Dimensions> GetDimensions(string userId);
        Task RemoveProduct(int id);
    }
}
