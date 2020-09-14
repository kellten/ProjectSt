using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Kitchen
{

    class CookingInput
    {
        public CookingInput(Recipe recipe)
        {
            this.recipe = recipe;
        }
        protected internal String[] Vessels = null;
        protected internal String[] Vegetables = null;
        protected internal String[] ingrdients = null;        
        protected internal String[] decorates = null;
        protected internal String dish;
        protected internal readonly Recipe recipe;
    }

    class CookingProcess
    {  
        public static void start(CookingInput input)
        {
            if (input == null)
            {
                return ;
            }
            //start the tasks
            Task<string[]> buyVegiesTask = Task.Run(() => buyVegies(input.recipe));            
            Task<string[]> cleanVesselsTask = Task.Run(() => cleanVessels(input.recipe));
            Task<string[]> prepareIngredientsTask = Task.Run(() => prepareIngredients(input.recipe));

            //cut vegetables            
            Task<String> cutVegeTask = buyVegiesTask.ContinueWith<string>(t =>
            {
                //Collect result
                input.Vegetables = t.Result;
                //Make a copy for cut task, so input is not affected
                string[] vegetables = new string[input.Vegetables.Length];
                input.Vegetables.CopyTo(vegetables, 0);
                return Retry(() =>
                {   //Passing bought vegetables to cut task
                    return cutVegies(vegetables, input.recipe);

                }, 3);
            },TaskContinuationOptions.OnlyOnRanToCompletion);

            //Give message to hungry folks
            Task giveMsg1Task = cutVegeTask.ContinueWith((prevTask) =>
            {
                giveMessageToHungryFolks(prevTask.Result);
            });

            //Wait for all preparation tasks
            Task.WaitAll(new Task[] { buyVegiesTask, cleanVesselsTask, prepareIngredientsTask, cutVegeTask });

            //collect task's results
            input.Vessels = cleanVesselsTask.Result;
            input.ingrdients = prepareIngredientsTask.Result;

            //Cook
            Task<String> cookTask = Task.Factory.StartNew<string>(() =>
            {
                return cook(input.recipe);
            });
          
            //Give message to hungry folks
            Task giveMsg2Task = cookTask.ContinueWith((prevTask) =>
            {
                giveMessageToHungryFolks(prevTask.Result);
            });

            //decorate
            Task<string[]> decorateTask = cookTask.ContinueWith((t) =>
            {   
                return decorate(input.recipe);
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
            
            //serve
            Task<string> serveTask = decorateTask.ContinueWith((t) =>
            {
                //collect decorate task result
                input.decorates = t.Result;
                //prepare serve description
                string description = String.Format("The plate of cooked [{0}] in a mix with good flavor of [{1}], decorated with [{2}] ",
                    string.Join(",", input.Vegetables), string.Join(",", input.ingrdients), string.Join(",", input.decorates));

               return serve(description, input.recipe);
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            serveTask.Wait();

            //collect sevre task result
            input.dish = serveTask.Result;

            Console.WriteLine();
            Console.WriteLine("Cooking process over, got the dish " + input.dish);
            Console.WriteLine();
        }
        /*
        * Buy Vegetables         
        */
        private static string[] buyVegies(Recipe recipe)
        {
            // Select vegetables based on recipe
            string[] vegetables = Recipes.buyVegieAlgorithms[recipe]();

            foreach (String veg in vegetables)
            {
                Console.WriteLine("Buy {0} for {1}", veg, recipe);
                Thread.Sleep(1100);
            }
            Console.WriteLine("Vegetables have been bought and ready for cooking " + recipe);
            return vegetables;
        }
        /*
        * Clean Vessels         
        */
        private static string[] cleanVessels(Recipe recipe)
        {
            // Select cook wares based on recipe
            string[] vessels = Recipes.cleanVesselAlgorithms[recipe]();

            foreach (String vessel in vessels)
            {
                Console.WriteLine("Cleaning {0} for {1}", vessel, recipe);
                Thread.Sleep(1100);
            }

            Console.WriteLine("Vessels have been cleaned and ready for cooking " + recipe);
            return vessels;
        }
        /*
        * Prepare Spices and additional ingredients        
        */
        private static string[] prepareIngredients(Recipe recipe)
        {
            //Select Spices / ingrdients based on recipe
            string[] ingrdients = Recipes.PrepareSpiceIngredientsAlgorithms[recipe]();
                        
            foreach (String ingredient in ingrdients)
            {
                Console.WriteLine("Prepare {0} for {1}", ingredient, recipe);
                Thread.Sleep(1100);
            }            
            Console.WriteLine("Ingredients Preparation done... Good aroma and ready for mix with {0}", recipe );
            return ingrdients;
        }

        static bool simulate_exception = true;
        /*
        *Cut the Vegetables that has been bought and passed in input parameter
        */
        private static string cutVegies(string[] vegetables, Recipe recipe)
        {
            
            foreach (String vegie in vegetables)
            {
                Console.WriteLine("cut cut cut {0} for {1}", vegie, recipe);
                Thread.Sleep(1100);                

                if (recipe == Recipe.Ratatouille && simulate_exception)
                {
                    string badMsg = "oh my god.... I cut my hand :(  while preparing " + recipe;
                    Console.WriteLine();
                    Console.WriteLine(badMsg);
                    Console.WriteLine();
                    TaskRetryableException exception = new TaskRetryableException(badMsg + "... But I can manage");
                    simulate_exception = false;
                    //TaskException exception = new TaskException(badMsg);
                    throw exception;
                }
            }
            return "almost cut task done... going to cook " + recipe;
        }

        /*
        * People are hungry and angry. Give pleasing status message
        */
        private static void giveMessageToHungryFolks(String input)
        {
            Thread.Sleep(100);
            Console.WriteLine("Hey please wait ... {0}", input);
        }

        /*
        * Cook
        */
        private static string cook(Recipe recipe)
        {
            Console.WriteLine("Cooking... " + recipe);
            //assume time consuming task
            Thread.Sleep(4500);
            Console.WriteLine("Cooking done and ready to serve " + recipe);
            return "Cooking done and decorating the food... just give 2 mins for " + recipe;
        }

        /*
        * Decorate
        */
        private static string[] decorate(Recipe recipe)
        {   
            string[] decorates = Recipes.decorateAlgorithms[recipe]();
            foreach (string dec in decorates)
            {
                Console.WriteLine("Decorating with {0} for {1}", dec , recipe);
                //assume time consuming task
                Thread.Sleep(200);
            }
            return decorates;
        }
        /*
        * Serve the dish. 
        * This method copies the input's recipe to dish as an indication of completing the preparation of dish.
        */
        private static string serve(String description, Recipe recipe)
        {   //assume time consuming task
            Thread.Sleep(200);
            Console.WriteLine();
            Console.WriteLine(description + "  - {0} served", recipe);

            return recipe.ToString();
        }

        
        public static T Retry<T>(Func<T> actualWork, int retryCount)
        {
            int maxAllowedRetry = retryCount;
            while (true)
            {
                try
                {
                    return actualWork();
                }
                catch (TaskRetryableException e)
                {   
                    if (retryCount == 0 )
                    {
                        throw new   TaskException ( "Maximum count of retry attempts reached " );
                    }
                    
                    retryCount--;
                    Console.WriteLine();
                    Console.WriteLine("TaskRetryableException occured. So Retrying task {0} time. Exception Message: {1}", maxAllowedRetry - retryCount, e.Message);
                    Console.WriteLine();
                }
            }
        }
    }
}
