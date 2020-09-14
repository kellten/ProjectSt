using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Kitchen
{
    class Program
    {
        static void Main(string[] args)
        {  
            /*
             * Create Inputs or Requests for 2 dishes in a batch
             * This input count controls the number of threads used for batch processing. Which means 2 threads are employed to do cooking process of 2 dishes.
             * But still some threads are created based on Tasks defined in each cooking process.
             */
            CookingInput[] inputs = new CookingInput[2];

            inputs[0] = new CookingInput(Recipe.Aloo_gobi);
            inputs[1] = new CookingInput(Recipe.Ratatouille);
            int count  = inputs.Count(s => s != null);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Console.WriteLine("Cooking batch started with {0} {1} ", count, "recipies");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine();            

            Task[] cookingTasks = new Task[count];
            try
            {
                for (int i=0; i< count; i++)
                {
                    int index = i;
                    //Start cooking for each request in a thread parallely
                    cookingTasks[i] = Task.Run(() => CookingProcess.start(inputs[index]));

                }
                Task.WaitAll(cookingTasks);                
            }
            catch (AggregateException ae)
            {
                Console.WriteLine();
                Console.WriteLine("Base exception: " + ae.GetBaseException());                                
            }            
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Got Exception:  " + ex);
            }
            
            Console.WriteLine();
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("Cooking batch completed in {0} seconds {1} milliseconds. Status Follows:", ts.Seconds, ts.Milliseconds);
            Console.WriteLine("------------------------------------------------------");
            for (int i= 0; i < count; i++)
            {
                Console.WriteLine("Cooking {0} status {1}", inputs[i].recipe, cookingTasks[i].Status);
            }            

            Console.ReadKey();
        }
    }
}
