using FitLife.DAL;
using FitLife.Helpers;
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
            Diet diet = new Diet();
            var user = await accountRepository.GetCurrentUser();
            await AddDimendions(dietData.Arms, dietData.Chest, dietData.Waist, dietData.Thig, dietData.Buttocks, user);
            double caloricDemand = CountCaloricDemand(dietData.Weight, dietData.Height, dietData.Age, user.isMale) * dietData.DailyActivity;

            if (dietData.DietTarget == 2)
            {
                caloricDemand += 300;
                diet.Kcal = Convert.ToInt32(caloricDemand);
                diet.Protein = Convert.ToInt32(caloricDemand * 0.3);
                diet.Fat = Convert.ToInt32(caloricDemand * 0.2);
                diet.Carbohydrates = Convert.ToInt32(caloricDemand * 0.5);
                diet.IsActive = true;
                diet.ApplicationUserId = user.Id;
                diet.ApplicationUser = user;
                diet.Date = DateTime.Now;
                diet.Mass = true;
                diet.WeightReduction = false;
                diet.WeightMaintenance = false;
                diet.Weight = (int)dietData.Weight;
            }
            else if (dietData.DietTarget == 1)
            {
                caloricDemand -= 300;
                diet.Kcal = Convert.ToInt32(caloricDemand);
                diet.Protein = Convert.ToInt32(caloricDemand * 0.3);
                diet.Fat = Convert.ToInt32(caloricDemand * 0.2);
                diet.Carbohydrates = Convert.ToInt32(caloricDemand * 0.5);
                diet.IsActive = true;
                diet.ApplicationUserId = user.Id;
                diet.ApplicationUser = user;
                diet.Date = DateTime.Now;
                diet.Mass = false;
                diet.WeightReduction = true;
                diet.WeightMaintenance = false;
                diet.Weight = (int)dietData.Weight;
            }



            if (dietData.DietTarget == 0)
            {
                diet.Kcal = Convert.ToInt32(caloricDemand);
                diet.Protein = Convert.ToInt32(caloricDemand * 0.3);
                diet.Fat = Convert.ToInt32(caloricDemand * 0.2);
                diet.Carbohydrates = Convert.ToInt32(caloricDemand * 0.5);
                diet.IsActive = true;
                diet.ApplicationUserId = user.Id;
                diet.ApplicationUser = user;
                diet.Date = DateTime.Now;
                diet.Mass = false;
                diet.WeightReduction = false;
                diet.WeightMaintenance = true;
                diet.Weight = (int)dietData.Weight;
                await CreateMeal(4, caloricDemand, diet);
            }
            else
            {
                await CreateMeal(5, caloricDemand, diet);
            }
            await dietRepository.AddDiet(diet);
            user.Diets.Add(diet);
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
                if (i == 1)
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
                else if (i == 2 && numberOfMeals == 4)
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
                else if (i == 4 && numberOfMeals == 5)
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

        public async Task AddDimendions(float arms, float chest, float waist, float thig, float buttocks, ApplicationUser user)
        {
            Dimensions d = new Dimensions
            {
                Arms = arms,
                Chest = chest,
                Waist = waist,
                Thig = thig,
                Buttocks = buttocks,
                isActive = true,
                ApplicationUser = user,
                ApplicationUserId = user.Id
            };
            await dietRepository.AddDimensions(d);
        }
        public async Task<Dimensions> GetDimensions()
        {
            var user = await accountRepository.GetCurrentUser();
            return await dietRepository.GetDimensions(user.Id);
        }

        public async Task<Diet> UpdateDiet(DietDataJson dietData)
        {
            var user = await accountRepository.GetCurrentUser();
            Diet updateDiet = await dietRepository.GetDiet(user.Id);
            await dietRepository.ChangeDietsAndDimensionsStatus(user.Id);
            await AddDimendions(dietData.Arms, dietData.Chest, dietData.Waist, dietData.Thig, dietData.Buttocks, user);
            Diet newDiet = new Diet();
            if (dietData.DietTarget == 2)
            {
                newDiet.Kcal = updateDiet.Kcal + 200;
                newDiet.Mass = true;
                newDiet.WeightReduction = false;
                newDiet.WeightMaintenance = false;
            }
            else if (dietData.DietTarget == 1)
            {
                if (user.isMale == true)
                {
                    if (updateDiet.Kcal > 1300)
                    {
                        newDiet.Kcal = updateDiet.Kcal - 100;
                    }
                    else
                    {
                        newDiet.Kcal = updateDiet.Kcal;
                    }
                    newDiet.Mass = false;
                    newDiet.WeightReduction = true;
                    newDiet.WeightMaintenance = false;

                }
            }
            else if (dietData.DietTarget == 0)
            {
                newDiet.Kcal = updateDiet.Kcal;
                newDiet.Mass = false;
                newDiet.WeightReduction = false;
                newDiet.WeightMaintenance = true;
            }

            newDiet.Protein = Convert.ToInt32(newDiet.Kcal * 0.3);
            newDiet.Fat = Convert.ToInt32(newDiet.Kcal * 0.2);
            newDiet.Carbohydrates = Convert.ToInt32(newDiet.Kcal * 0.5);
            newDiet.IsActive = true;
            newDiet.ApplicationUserId = user.Id;
            newDiet.ApplicationUser = user;
            newDiet.Date = DateTime.Now;

            if (newDiet.WeightMaintenance == true)
            {
                await CreateMeal(4, newDiet.Kcal, newDiet);
            }
            else
            {
                await CreateMeal(5, newDiet.Kcal, newDiet);
            }

            await dietRepository.AddDiet(newDiet);
            user.Diets.Add(newDiet);
            await dietRepository.Save();
            return newDiet;
        }

        public async Task<bool> RemoveProduct(int id)
        {
           await dietRepository.RemoveProduct(id);
            return true;
        }
        public async Task<bool> AddProduct(ProductJson product)
        {
            Product newProduct = new Product
            {
                Name = product.Name,
                NutritionalValue = product.NutritionalValue,
                Category = product.Category
            };

            await dietRepository.AddProduct(newProduct);
            return true;
        }

    }
}

