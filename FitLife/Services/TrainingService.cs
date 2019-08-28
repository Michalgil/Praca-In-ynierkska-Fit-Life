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

        public TrainingService(ITrainingRepository trainingRepository, IAccountRepository accountRepository)
        {
            this.trainingRepository = trainingRepository;
            this.accountRepository = accountRepository;
        }
        
        public async Task<List<Training>> CreateTrainingPlan (TrainingDataJson traningData)
        {
            var user = await accountRepository.GetCurrentUser();
            List<Training> trainingPlanList = new List<Training>();
            int breakTime = 0;
            
            if (traningData.Experience == 1)
            {
                breakTime = 120;
                var trainingPlan = await GetTrainingPlan(3, breakTime, user);
                trainingPlanList = trainingPlan.ToList();

            }
            else if (traningData.Experience == 2)
            {
                breakTime = 90;
                var trainingPlan = await GetTrainingPlan(4, breakTime, user);
                trainingPlanList = trainingPlan.ToList();
            }
            else
            {
                breakTime = 60;
                breakTime = 90;
                var trainingPlan = await GetTrainingPlan(5, breakTime, user);
                trainingPlanList = trainingPlan.ToList();
            }
            return trainingPlanList;

        }

        public async Task<List<Training>> GetTrainingPlan(int trainingsPerWeek,int breakTime, ApplicationUser user)
        {
            List<Exercise> exercisesForTraining;
            List<Training> TrainingPlanList = new List<Training>();


            for (int i = 1; i <= trainingsPerWeek; i++)
            {
                Training training = new Training
                {
                    Break = breakTime,
                    ApplicationUser = user,
                };

                if(trainingsPerWeek == 3)
                {
                    training.isBegginer = true;
                    exercisesForTraining = await GetExercisesForBegginer(trainingsPerWeek, user.isMale, i);
                }
                else if(trainingsPerWeek == 4)
                {
                    training.isIntermediate = true;
                    exercisesForTraining = await GetExercisesForIntermediate(trainingsPerWeek, user.isMale, i);
                }
                else
                {
                    training.isAdvanced = true;
                    exercisesForTraining = await GetExercisesForAdvanced(trainingsPerWeek, user.isMale, i);
                }

                await trainingRepository.AddTraining(training);
                foreach(Exercise e in exercisesForTraining)
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
                var backExercises = await trainingRepository.GetBackExercises();
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

        

public async Task addData()
        {
            List<Exercise> list2 = new List<Exercise>();
            Exercise e1 = new Exercise
            {
                Name = "Wypuchanie nógi w góre na maszynie smith",
                PartOfBody = trainingRepository.getPartOfBodyId("Pośladki")

            };
            //Exercise e2 = new Exercise
            //{
            //    Name = "Spięcia brzucha leżąc",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Brzuch")
            //};
            //Exercise e3 = new Exercise
            //{
            //    Name = "Unoszenie nóg w zwisie",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Brzuch")
            //};
            //Exercise e4 = new Exercise
            //{
            //    Name = "Scyzoryk leżąc",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Brzuch")
            //};
            //Exercise e5 = new Exercise
            //{
            //    Name = "Świeca leżać na ławce skośnej w dół",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Brzuch")
            //};

            list2.Add(e1);



            //Exercise e6 = new Exercise
            //{
            //    Name = "Uginanie rąk z supinacją",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Biceps")
            //};
            //Exercise e7 = new Exercise
            //{
            //    Name = "Uginanie rąk z uchwytem młotkowym",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Biceps")
            //};
            //Exercise e8 = new Exercise
            //{
            //    Name = "Uginanie rąk ze sztangą stojąc",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Biceps")
            //};
            //Exercise e9 = new Exercise
            //{
            //    Name = "Uginanie rąk ze sztangą na modlitewniku",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Biceps")
            //};
            //Exercise e10 = new Exercise
            //{
            //    Name = "Podciąganie na drążku wąskim uchwytem",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Biceps")
            //};



            //Exercise e11 = new Exercise
            //{
            //    Name = "Wyciskanie sztangi wąskim uchwytem",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Triceps")
            //};
            //Exercise e12 = new Exercise
            //{
            //    Name = "Ściąganie linki z góry wąskim uchwytem",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Triceps")

            //};
            //Exercise e13 = new Exercise
            //{
            //    Name = "Ściąganie linki z góry przy użyciu sznura",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Triceps")
            //};
            //Exercise e14 = new Exercise
            //{
            //    Name = "Odwrotne popmki na ławce",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Triceps")
            //};
            //Exercise e15 = new Exercise
            //{
            //    Name = "Prostowanie rąk na maszynie hammer",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Triceps")
            //};


            //Exercise e16 = new Exercise
            //{
            //    Name = "Wyciskanie sztangi leżac na ławce płaskiej",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Klatka piersiowa")
            //};
            //Exercise e17 = new Exercise
            //{
            //    Name = "Wyciskanie hantli leżąc na ławce skośnej w góre",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Klatka piersiowa")
            //};
            //Exercise e18 = new Exercise
            //{
            //    Name = "Rozpiętki na bramie",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Klatka piersiowa")
            //};
            //Exercise e19 = new Exercise
            //{
            //    Name = "Wyciskanie hantli leżąc na ławce skośnej w dół",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Klatka piersiowa")
            //};
            //Exercise e20 = new Exercise
            //{
            //    Name = "Wyciskanie szerokim uchwytem na maszynie hammer",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Klatka piersiowa")
            //};




            //Exercise e21 = new Exercise
            //{
            //    Name = "Wyciskanie hantli siedząc",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Ramiona")
            //};
            //Exercise e22 = new Exercise
            //{
            //    Name = "Face pull",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Ramiona")
            //};
            //Exercise e23 = new Exercise
            //{
            //    Name = "Wznoszenie hantli bokiem",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Ramiona")
            //};
            //Exercise e24 = new Exercise
            //{
            //    Name = "Wznoszenie hantli przodem",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Ramiona")
            //};
            //Exercise e25 = new Exercise
            //{
            //    Name = "Wznoszenie sztangi przodem",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Ramiona")
            //};



            //Exercise e26 = new Exercise
            //{
            //    Name = "Przysiady ze sztangą",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Nogi")
            //};
            //Exercise e27 = new Exercise
            //{
            //    Name = "Prostawnie nóg na maszynie",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Nogi")
            //};
            //Exercise e28 = new Exercise
            //{
            //    Name = "Uginanie nóg na maszynie",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Nogi")
            //};
            //Exercise e29 = new Exercise
            //{
            //    Name = "Martwy ciąg na prostych nogach",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Nogi")
            //};
            //Exercise e30 = new Exercise
            //{
            //    Name = "Wykroki chodzone z kettlami",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Nogi")
            //};


            //Exercise e31 = new Exercise
            //{
            //    Name = "Przysiady sumo",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Pośladki")
            //};
            //Exercise e32 = new Exercise
            //{
            //    Name = "Zakroki na stepie",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Pośladki")
            //};
            //Exercise e33 = new Exercise
            //{
            //    Name = "Bułgarski przysiad",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Pośladki")
            //};
            //Exercise e34 = new Exercise
            //{
            //    Name = "Hip thrust",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Pośladki")
            //};
            //Exercise e35 = new Exercise
            //{
            //    Name = "Obwodziciele na maszynie stojąc",
            //    PartOfBody = trainingRepository.getPartOfBodyId("Pośladki")
            //};


            //list2.Add(e1);
            //list2.Add(e2);
            //list2.Add(e3);
            //list2.Add(e4);
            //list2.Add(e5);
            //list2.Add(e6);
            //list2.Add(e7);
            //list2.Add(e8);
            //list2.Add(e9);
            //list2.Add(e10);
            //list2.Add(e11);
            //list2.Add(e12);
            //list2.Add(e13);
            //list2.Add(e14);
            //list2.Add(e15);
            //list2.Add(e16);
            //list2.Add(e17);
            //list2.Add(e18);
            //list2.Add(e19);
            //list2.Add(e20);
            //list2.Add(e21);
            //list2.Add(e22);
            //list2.Add(e23);
            //list2.Add(e24);
            //list2.Add(e25);
            //list2.Add(e26);
            //list2.Add(e27);
            //list2.Add(e28);
            //list2.Add(e29);
            //list2.Add(e30);
            //list2.Add(e31);
            //list2.Add(e32);
            //list2.Add(e32);
            //list2.Add(e34);
            //list2.Add(e35);

            await trainingRepository.AddData(list2);
            await trainingRepository.Save();
        }

    }
}
