using FitLife.DAL;
using FitLife.Models;
using FitLife.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.Services
{
    public class TrainingService
    {
        private ITrainingRepository trainingRepository;
        private IAccountRepository accountRepository;
        static Random rnd = new Random();
        private List<Task> taskList = new List<Task>();

        public TrainingService(ITrainingRepository trainingRepository, IAccountRepository accountRepository)
        {
            this.trainingRepository = trainingRepository;
            this.accountRepository = accountRepository;
        }
        
        public async Task<List<Training>> CreateTrainingPlan (TrainingDataJson trainingData)
        {
            
            var user = await accountRepository.GetCurrentUser();
            List<Training> trainingPlanList = new List<Training>();
            int breakTime = 0;
            
            if (trainingData.Experience == 1)
            {
                breakTime = 120;
                var trainingPlan = await GetReadyTrainingPlan(3, breakTime, user, trainingData.PriorityPart);
                trainingPlanList = trainingPlan.ToList();

            }
            else if (trainingData.Experience == 2)
            {
                breakTime = 90;
                var trainingPlan = await GetReadyTrainingPlan(4, breakTime, user, trainingData.PriorityPart);
                trainingPlanList = trainingPlan.ToList();
            }
            else
            {
                breakTime = 60;
                var trainingPlan = await GetReadyTrainingPlan(5, breakTime, user, trainingData.PriorityPart);
                trainingPlanList = trainingPlan.ToList();
            }
            //if (trainingData.PriorityPart != null)
            //{
            //    var result = await AddPriorityExercise(trainingPlanList, trainingData.PriorityPart);
            //    await trainingRepository.Save();
            //    return result;
            //}
            await trainingRepository.Save();
            return trainingPlanList;
        }

        public async Task<List<Training>> GetReadyTrainingPlan(int trainingsPerWeek,int breakTime, ApplicationUser user, string priorityPart)
        {
            List<Exercise> exercisesForTraining; //usunac newlist
            List<Training> TrainingPlanList = new List<Training>();


            for (int i = 1; i <= trainingsPerWeek; i++)
            {
                Training training = new Training
                {
                    Break = breakTime,
                    ApplicationUser = user,
                    ApplicationUserId = user.Id,
                    Date = DateTime.Now,
                    PriorityPart = priorityPart,
                    IsActive = true
            };

                if(trainingsPerWeek == 3)
                {
                    training.IsBegginer = true;
                    exercisesForTraining = await GetExercisesForBegginer(trainingsPerWeek, user.isMale, i);
                }
                else if(trainingsPerWeek == 4)
                {
                    training.IsIntermediate = true;
                    exercisesForTraining =  await GetExercisesForIntermediate(trainingsPerWeek, user.isMale, i);
                }
                else
                {
                    training.IsAdvanced = true;
                    exercisesForTraining =  await GetExercisesForAdvanced(trainingsPerWeek, user.isMale, i);
                }

                await trainingRepository.AddTraining(training);
                foreach (Exercise e in exercisesForTraining)
                {
                    TrainingExercise te = new TrainingExercise
                    {
                        TrainingId = training.Id,
                        Training = training,
                        Exercise = e,
                        ExerciseId = e.Id
                    };
                    training.TrainingExercises.Add(te);
                    await trainingRepository.AddTrainingExercise(te);

                }
                TrainingPlanList.Add(training);
            }
            return TrainingPlanList;

        }

        public async Task<List<Exercise>> GetExercisesForBegginer(int trainingsPerWeek, bool isMale, int numberOfTraining)
        {
            List<Exercise> trainingExerciseList = new List<Exercise>();
            List<int> randomNumbers = new List<int>();

            if (numberOfTraining == 1)
            {
                var backExercises =  await trainingRepository.GetBackExercises();
                var bicepsExercises = await trainingRepository.GetBicepsExercises();
                var randomNumberForBack = GetRandomNumbers(backExercises.Count(), 3);
                var randomNumberForBiceps = GetRandomNumbers(bicepsExercises.Count(), 3);

                for (int i = 0; i <= 2; i++)
                {
                    int randomBackExercise = (randomNumberForBack.ElementAt(i));
                    int randomBicepsExercise = (randomNumberForBiceps.ElementAt(i));
                    trainingExerciseList.Add(backExercises.ElementAt(randomBackExercise));
                    trainingExerciseList.Add(bicepsExercises.ElementAt(randomBicepsExercise));
                }
                var stomachExercises = await trainingRepository.GetStomachExercises();
                int randomstomachExercise = rnd.Next(stomachExercises.Count());
                trainingExerciseList.Add(stomachExercises.ElementAt(randomstomachExercise));
            }
            if (numberOfTraining == 2)
            {
                var chestExercises = await trainingRepository.GetChestExercises();
                var shouldersExercises = await trainingRepository.GetShouldersExercises();
                var tricepsExercises = await trainingRepository.GetTricepsExercises();

                var randomNumberForChest = GetRandomNumbers(chestExercises.Count(), 3);
                var randomNumberForShoulders = GetRandomNumbers(shouldersExercises.Count(), 3);
                var randomNumberForTriceps = GetRandomNumbers(tricepsExercises.Count(), 3);

                for (int i = 0; i <= 2; i++)
                {
                    int randomChestExercise = (randomNumberForChest.ElementAt(i));
                    int randomShouldersExercise = (randomNumberForShoulders.ElementAt(i));
                    int randomTricepsExercise = (randomNumberForTriceps.ElementAt(i));
                    trainingExerciseList.Add(chestExercises.ElementAt(randomChestExercise));
                    trainingExerciseList.Add(shouldersExercises.ElementAt(randomShouldersExercise));
                    trainingExerciseList.Add(tricepsExercises.ElementAt(randomTricepsExercise));

                }

            }
            if (numberOfTraining == 3)
            {
                var legsExercises = await trainingRepository.GetLegsExercises();
                var buttocksExercises = await trainingRepository.GetButtocksExercises();

                var randomNumbersForLegs = GetRandomNumbers(legsExercises.Count(), 3);
                var randomNumbersForButtocks = GetRandomNumbers(buttocksExercises.Count(), 3);

                for (int i = 0; i <= 2; i++)
                {
                    int randomButtocksExercise;
                    if (!isMale)
                    {
                        randomButtocksExercise = (randomNumbersForButtocks.ElementAt(i));
                        trainingExerciseList.Add(buttocksExercises.ElementAt(randomButtocksExercise));
                    }

                    int randomLegsExercise = (randomNumbersForLegs.ElementAt(i));
                    trainingExerciseList.Add(legsExercises.ElementAt(randomLegsExercise));

                }
            }

            return trainingExerciseList;


        }
        public async Task<List<Exercise>> GetExercisesForIntermediate(int trainingsPerWeek, bool isMale, int numberOfTraining)
        {
            List<Exercise> trainingExerciseList = new List<Exercise>();

            if (numberOfTraining == 1)
            {
                var backExercises = await trainingRepository.GetBackExercises();
                var randomNumberForBack = GetRandomNumbers(backExercises.Count(), 4);

                for (int i = 0; i <= 3; i++)
                {
                    int randomBackExercise = (randomNumberForBack.ElementAt(i));
                    trainingExerciseList.Add(backExercises.ElementAt(randomBackExercise));

                }
                var stomachExercises = await trainingRepository.GetStomachExercises();
                int randomstomachExercise = rnd.Next(stomachExercises.Count());
                trainingExerciseList.Add(stomachExercises.ElementAt(randomstomachExercise));
            }
            if (numberOfTraining == 2)
            {
                var bicepsExercises = await trainingRepository.GetBicepsExercises();
                var tricepsExercises = await trainingRepository.GetTricepsExercises();

                var randomNumberForBiceps = GetRandomNumbers(bicepsExercises.Count(), 4);
                var randomNumberForTriceps = GetRandomNumbers(tricepsExercises.Count(), 4);

                for (int i = 0; i <= 3; i++)
                {
                    int randomBicepsExercise = (randomNumberForBiceps.ElementAt(i));
                    int randomTricepsExercise = (randomNumberForTriceps.ElementAt(i));
                    trainingExerciseList.Add(bicepsExercises.ElementAt(randomBicepsExercise));
                    trainingExerciseList.Add(tricepsExercises.ElementAt(randomTricepsExercise));
                }

            }
            if (numberOfTraining == 3)
            {
                var chestExercises = await trainingRepository.GetChestExercises();
                var shouldersExercises = await trainingRepository.GetShouldersExercises();

                var randomNumberForChest = GetRandomNumbers(chestExercises.Count(), 4);
                var randomNumberForShoulders = GetRandomNumbers(shouldersExercises.Count(), 4);

                for (int i = 0; i <= 3; i++)
                {
                    int randomChestExercise = (randomNumberForChest.ElementAt(i));
                    int randomShouldersExercise = (randomNumberForShoulders.ElementAt(i));
                    trainingExerciseList.Add(chestExercises.ElementAt(randomChestExercise));
                    trainingExerciseList.Add(shouldersExercises.ElementAt(randomShouldersExercise));
                }
            }
            if (numberOfTraining == 4)
            {
                var legsExercises = await trainingRepository.GetLegsExercises();
                var buttocksExercises = await trainingRepository.GetButtocksExercises();

                var randomNumberForLegs = GetRandomNumbers(legsExercises.Count(), 4);
                var randomNumberForButtocks = GetRandomNumbers(buttocksExercises.Count(), 4);

                for (int i = 0; i <= 3; i++)
                {
                    int randomButtocksExercise;
                    if (!isMale)
                    {
                        randomButtocksExercise = (randomNumberForButtocks.ElementAt(i));
                        trainingExerciseList.Add(buttocksExercises.ElementAt(randomButtocksExercise));
                    }

                    int randomLegsExercise = (randomNumberForLegs.ElementAt(i));
                    trainingExerciseList.Add(legsExercises.ElementAt(randomLegsExercise));

                }
            }

            return trainingExerciseList;
        }
        public async Task<List<Exercise>> GetExercisesForAdvanced(int trainingsPerWeek, bool isMale, int numberOfTraining)
        {

            List<Exercise> trainingExerciseList = new List<Exercise>();

            if (numberOfTraining == 1)
            {
                var backExercises = await trainingRepository.GetBackExercises();

                var randomNumberForBack = GetRandomNumbers(backExercises.Count(), 5);

                for (int i = 0; i <= 4; i++)
                {
                    int randomBackExercise = (randomNumberForBack.ElementAt(i));

                    trainingExerciseList.Add(backExercises.ElementAt(randomBackExercise));
                }
                var stomachExercises = await trainingRepository.GetStomachExercises();
                int randomstomachExercise = rnd.Next(stomachExercises.Count());
                trainingExerciseList.Add(stomachExercises.ElementAt(randomstomachExercise));
            }
            if (numberOfTraining == 2)
            {
                var bicepsExercises = await trainingRepository.GetBicepsExercises();
                var tricepsExercises = await trainingRepository.GetTricepsExercises();

                var randomNumberForBiceps = GetRandomNumbers(bicepsExercises.Count(), 5);
                var randomNumberForTriceps = GetRandomNumbers(tricepsExercises.Count(), 5);

                for (int i = 0; i <= 4; i++)
                {
                    int randomBicepsExercise = (randomNumberForBiceps.ElementAt(i));
                    int randomTricepsExercise = (randomNumberForTriceps.ElementAt(i));
                    trainingExerciseList.Add(bicepsExercises.ElementAt(randomBicepsExercise));
                    trainingExerciseList.Add(tricepsExercises.ElementAt(randomTricepsExercise));
                }

            }
            if (numberOfTraining == 3)
            {
                var chestExercises = await trainingRepository.GetChestExercises();

                var randomNumberForChest = GetRandomNumbers(chestExercises.Count(), 5);

                for (int i = 0; i <= 4; i++)
                {
                    int randomChestExercise = (randomNumberForChest.ElementAt(i));
                    trainingExerciseList.Add(chestExercises.ElementAt(randomChestExercise));
                }
            }
            if (numberOfTraining == 4)
            {
                var shouldersExercises = await trainingRepository.GetShouldersExercises();

                var randomNumberForShoulders = GetRandomNumbers(shouldersExercises.Count(), 5);

                for (int i = 0; i <= 4; i++)
                {
                    int randomShouldersExercise = (randomNumberForShoulders.ElementAt(i));
                    trainingExerciseList.Add(shouldersExercises.ElementAt(randomShouldersExercise));
                }

                var stomachExercises = await trainingRepository.GetStomachExercises();
                int randomstomachExercise = rnd.Next(stomachExercises.Count());
                trainingExerciseList.Add(stomachExercises.ElementAt(randomstomachExercise));
            }
            if (numberOfTraining == 5)
            {
                var legsExercises = await trainingRepository.GetLegsExercises();
                var buttocksExercises = await trainingRepository.GetButtocksExercises();

                var randomNumberForLegs = GetRandomNumbers(legsExercises.Count(), 5);
                var randomNumberForButtocks = GetRandomNumbers(buttocksExercises.Count(), 5);

                for (int i = 0; i <= 4; i++)
                {
                    int randomButtocksExercise;
                    if (!isMale)
                    {
                        randomButtocksExercise = (randomNumberForButtocks.ElementAt(i));
                        trainingExerciseList.Add(buttocksExercises.ElementAt(randomButtocksExercise));
                    }

                    int randomLegsExercise = (randomNumberForLegs.ElementAt(i));
                    trainingExerciseList.Add(legsExercises.ElementAt(randomLegsExercise));

                }
            }

            return trainingExerciseList;
        }
        public List<int> GetRandomNumbers(int lengthExerciseList,int numberOfExercises)
        {
            List<int> listNumbers = new List<int>();
             int number;
            for (int i = 0; i < numberOfExercises; i++)
            {
                do
                {
                    number = rnd.Next(0,lengthExerciseList);
                } 
                while (listNumbers.Contains(number));
                listNumbers.Add(number);
            }

            return listNumbers;
        }
        //public async Task<List<Training>> AddPriorityExercise(List<Training> trainingPlan, string priorityPart)
        //{
        //    foreach (Training t in trainingPlan)
        //    {
        //        if (t.TrainingExercises.Any(x => x.Exercise.PartOfBody.Name == priorityPart))
        //        {
        //            foreach (Exercise e in await trainingRepository.GetExercises())
        //            {
        //                if (!(t.TrainingExercises.Any(x => x.Exercise.Id == e.Id)) && e.PartOfBody.Name == priorityPart)
        //                {
        //                    TrainingExercise te = new TrainingExercise
        //                    {
        //                        TrainingId = t.Id,
        //                        Training = t,
        //                        Exercise = e,
        //                        ExerciseId = e.Id
        //                    };
        //                    t.TrainingExercises.Add(te);
        //                    await trainingRepository.AddTrainingExercise(te);
        //                    goto BreakLoops;
                            
        //                }
        //            }
        //        }
        //    }
        //    BreakLoops:
        //    return trainingPlan;
        //}

        public async Task<List<Training>> GetTrainingPlan()
        {
            var user = await accountRepository.GetCurrentUser();
            return (trainingRepository.GetTrainingList(user.Id)).ToList();
        }
        public async Task<List<Training>> updateTraining()
        {
            int breakTime = 0;
            var user = await accountRepository.GetCurrentUser();
            var training = await trainingRepository.GetTraining(user.Id);
            trainingRepository.SetTrainingStatus(user.Id);
            List<Training> trainingPlanList = new List<Training>();

            if (training.IsBegginer == true)
            {
                breakTime = 90;
                var trainingPlan = await GetReadyTrainingPlan(4, breakTime, user, training.PriorityPart);
                trainingPlanList = trainingPlan.ToList();
            }
            else if (training.IsIntermediate == true || training.IsAdvanced == true)
            {
                breakTime = 60;
                var trainingPlan = await GetReadyTrainingPlan(5, breakTime, user, training.PriorityPart);
                trainingPlanList = trainingPlan.ToList();
            }

            //if (training.PriorityPart != null)
            //{
            //    var result = await AddPriorityExercise(trainingPlanList, training.PriorityPart);
            //    await trainingRepository.Save();
            //    return result;
            //}
            await trainingRepository.Save();
            return trainingPlanList;
        }
        public async Task<int> GetNumberOfTrainings()
        {
            var user = await accountRepository.GetCurrentUser();

            return trainingRepository.GetQunatityOfTrainings(user.Id);
        }

        public IEnumerable<Exercise> GetExercises()
        {
            return trainingRepository.GetExercises();
        }

    }
}
