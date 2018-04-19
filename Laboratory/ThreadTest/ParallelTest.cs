using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Laboratory.ThreadTest
{
    public class ParallelTest
    {
        private Stopwatch stopWatch = new Stopwatch();

        public void Run1()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Task 1 is cost 2 sec");
        }
        public void Run2()
        {
            Thread.Sleep(3000);
            Console.WriteLine("Task 2 is cost 3 sec");
        }

        public void ParallelInvokeMethod()
        {
            stopWatch.Start();
            Parallel.Invoke(Run1, Run2);
            stopWatch.Stop();
            Console.WriteLine("Parallel run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Restart();
            Run1();
            Run2();
            stopWatch.Stop();
            Console.WriteLine("Normal run " + stopWatch.ElapsedMilliseconds + " ms.");
        }

        public void ParallelForMethod()
        {
            stopWatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 60000; j++)
                {
                    int sum = 0;
                    sum += i;
                }
            }
            stopWatch.Stop();
            Console.WriteLine("NormalFor run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Reset();
            stopWatch.Start();
            Parallel.For(0, 10000, item =>
            {
                for (int j = 0; j < 60000; j++)
                {
                    int sum = 0;
                    sum += item;
                }
            });
            stopWatch.Stop();
            Console.WriteLine("ParallelFor run " + stopWatch.ElapsedMilliseconds + " ms.");

        }
    }
}
