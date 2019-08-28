using FitLife.DAL;
using FitLife.Models;
using FitLife.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitLife.Services
{
    public class DietService
    {
        private IDietRepository dietRepository;
        private IAccountRepository accountRepository;
        public DietService(IDietRepository dietRepository, IAccountRepository accountRepository)
        {
            this.dietRepository = dietRepository;
            this.accountRepository = accountRepository;
        }
        public async Task<Diet> CreateDiet(DietDataJson dietData)
        {
            double caloricDemand = CountCaloricDemand(dietData.Weight, dietData.Height, dietData.Age, dietData.IsMale) * dietData.DailyActivity;

            if (dietData.DietTarget == 2)
            {
                caloricDemand += 300;
            }
            else if (dietData.DietTarget == 1)
            {
                caloricDemand -= 300;
            }

            var user = await accountRepository.GetCurrentUser();

            Diet diet = new Diet
            {
                Kcal = Convert.ToInt32(caloricDemand),
                Protein = Convert.ToInt32(caloricDemand * 0.3),
                Fat = Convert.ToInt32(caloricDemand * 0.2),
                Carbohydrates = Convert.ToInt32(caloricDemand * 0.5),
                IsActive = true,
                ApplicationUserId = user.Id,
                ApplicationUser = user
            };

            if (dietData.DietTarget == 0)
            {
                await CreateMeal(4, caloricDemand, diet);
            }
            else
            {
                await CreateMeal(5, caloricDemand, diet);
            }

            await dietRepository.AddDiet(diet);
            await dietRepository.Save();

            return diet;
        }
        public double CountCaloricDemand(double weight, double height, double age, Boolean isMale)
        {
            weight = weight * 9.99;
            height = height * 6.25;
            age = age * 4.92;

            if (isMale == true)
            {
                return (weight + height + age) + 5;
            }

            return (weight + height + age) - 161;
        }

        public async Task CreateMeal(int numberOfMeals, double caloricDemand, Diet diet)
        {
            double kcalPerMeal = caloricDemand / numberOfMeals;
            for (int i = 1; i <= numberOfMeals; i++)
            {
                Meal meal;
                if ( i == 1)
                {
                     meal = new Meal
                    {
                        NumberOfMeal = i,
                        Kcal = Convert.ToInt32(kcalPerMeal),
                        Protein = Convert.ToInt32(kcalPerMeal * 0.3),
                        Fat = Convert.ToInt32(kcalPerMeal * 0.7),
                        Carbohydrates = 0,
                        DietId = diet.Id,
                        Diet = diet
                    };
                }
                else if ( i == 2 && numberOfMeals == 4)
                {
                    meal = new Meal
                    {
                        NumberOfMeal = i,
                        Kcal = Convert.ToInt32(kcalPerMeal),
                        Protein = Convert.ToInt32(kcalPerMeal * 0.3),
                        Fat = 0,
                        Carbohydrates = Convert.ToInt32(kcalPerMeal * 0.7),
                        DietId = diet.Id,
                        Diet = diet
                    };
                }
                else if (i == 3 && numberOfMeals == 5)
                {
                    meal = new Meal
                    {
                        NumberOfMeal = i,
                        Kcal = Convert.ToInt32(kcalPerMeal),
                        Protein = Convert.ToInt32(kcalPerMeal * 0.3),
                        Fat = 0,
                        Carbohydrates = Convert.ToInt32(kcalPerMeal * 0.7),
                        DietId = diet.Id,
                        Diet = diet
                    };
                }
                else if ( i == 4 && numberOfMeals == 5)
                {
                    meal = new Meal
                    {
                        NumberOfMeal = i,
                        Kcal = Convert.ToInt32(kcalPerMeal),
                        Protein = Convert.ToInt32(kcalPerMeal * 0.3),
                        Fat = 0,
                        Carbohydrates = Convert.ToInt32(kcalPerMeal * 0.7),
                        DietId = diet.Id,
                        Diet = diet
                    };
                }
                else if (i == 3 && numberOfMeals == 4)
                {
                    meal = new Meal
                    {
                        NumberOfMeal = i,
                        Kcal = Convert.ToInt32(kcalPerMeal),
                        Protein = Convert.ToInt32(kcalPerMeal * 0.3),
                        Fat = 0,
                        Carbohydrates = Convert.ToInt32(kcalPerMeal * 0.7),
                        DietId = diet.Id,
                        Diet = diet
                    };
                }
                else
                {
                    meal = new Meal
                    {
                        NumberOfMeal = i,
                        Kcal = Convert.ToInt32(kcalPerMeal),
                        Protein = Convert.ToInt32(kcalPerMeal * 0.3),
                        Fat = Convert.ToInt32(kcalPerMeal * 0.2),
                        Carbohydrates = Convert.ToInt32(kcalPerMeal * 0.5),
                        DietId = diet.Id,
                        Diet = diet
                    };
                }
                

                await dietRepository.AddMeal(meal);
            }
        }

        public async Task<Diet> GetDiet()
        {
            var user = await accountRepository.GetCurrentUser();
            var diet = await dietRepository.GetDiet(user.Id);
            var meals = await dietRepository.GetDietMeals(diet.Id);
            diet.Meals = meals.ToList();
            return diet;
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await dietRepository.GetAllProducts();
        }
    }
}

