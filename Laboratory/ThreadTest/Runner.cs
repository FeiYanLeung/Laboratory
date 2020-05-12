using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Laboratory.ThreadTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get
            {
                return "多线程";
            }
        }

        int count = 0;
        private async Task<bool> run1()
        {
            return await new TaskFactory().StartNew<bool>(() =>
             {
                 for (int i = 0; i < 5; i++)
                 {
                     Console.WriteLine("{0:HH:mm:ss:fff} {1}", DateTime.Now, ++count);
                     Thread.Sleep(500);
                 }
                 return false;
             });
        }

        #region 多线程示例

        public class threadParameter
        {
            public string Name { get; private set; }
            public int Age { get; private set; }
            public threadParameter(string name, int age)
            {
                this.Name = name;
                this.Age = age;
            }

            public override string ToString()
            {
                return $" 【CLASS.thread.parameter】 {this.Name} - {this.Age}";
            }
        }

        public void runFunc()
        {

            try
            {
                Console.WriteLine("【FUNC.thread.id】: {0} {1}", DateTime.Now.ToGreenwichTimestamp(), Thread.CurrentThread.ManagedThreadId);
                for (int n = 0; n < 10; n++)
                {
                    if (n >= 4) //当n等于4时，终止线程
                    {
                        Thread.CurrentThread.Abort(n);
                    }
                    Thread.Sleep(300);
                    Console.WriteLine("【FUNC.thread.id.abort[N]】:" + n.ToString());
                }
            }
            catch (ThreadAbortException ex)
            {
                //输出终止线程时n的值
                if (ex.ExceptionState != null)
                    Console.WriteLine("Thread abort when the number is: {0}!", ex.ExceptionState.ToString());

                Thread.ResetAbort(); //取消终止，继续执行线程
                Console.WriteLine("Thread ResetAbort!");
                Console.WriteLine("Thread Close!"); //线程结束
                Console.ReadKey();
            }
        }

        void runParameterFunc(object parameter)
        {
            if (null != parameter)
            {
                var _threadParameter = parameter as threadParameter;
                Console.WriteLine("【FUNC.thread.parameter】: {0} {1}", DateTime.Now.ToGreenwichTimestamp(), _threadParameter.ToString());
                return;
            }
            Console.WriteLine("【FUNC.thread.NO.parameter】: {0}", DateTime.Now.ToGreenwichTimestamp());
        }

        #endregion


        public void Run()
        {
            var _thread = new Thread(new ThreadStart(runFunc));

            _thread.Start();
            var _thread_timeout = _thread.Join(10);       // 阻塞线程，等待 _thread 执行完成
            if (_thread_timeout) Console.WriteLine("join.timeout");

            var _thread_parameter = new Thread(new ParameterizedThreadStart(runParameterFunc));
            _thread_parameter.Start(new threadParameter("Alina", 30));


            //Thread.Sleep(100);   // 挂起当前线程 100 ms
            Console.WriteLine("【ENTRY.thread.id】: {0} {1}", DateTime.Now.ToGreenwichTimestamp(), Thread.CurrentThread.ManagedThreadId);

            ThreadPool.QueueUserWorkItem(new WaitCallback(runParameterFunc), new threadParameter("Leung", 20));

            return;
            var backgroundThread = new Thread(new ThreadStart(() =>
              {
                  for (int i = 0; i < 10; i++)
                  {
                      Console.WriteLine($"后台线程计数：{i}");
                      Thread.Sleep(1000);
                  }
              }));

            backgroundThread.IsBackground = true;
            backgroundThread.Start();

            var forceThread = new Thread(new ThreadStart(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"主线程计数：{i}");
                    Thread.Sleep(800);
                }
            }));
            forceThread.Start();




            return;


            runMTaskPool();
            return;
            new Action(async () =>
            {
                var x = await run1();
                Console.Write(x);
            })();

            return;
            var watch = new Stopwatch();

            var task_main = Task.Run(() =>
            {
                watch.Start();
                Thread.Sleep(3000);
            });


            var task_complete = Task.Run(() =>
            {
                watch.Stop();
                Console.WriteLine("导入完成，耗时{0}毫秒", watch.ElapsedMilliseconds);
            });

            var task_timer = Task.Run(() =>
            {
                string _symbol = "";
                while (watch.IsRunning)
                {
                    if (_symbol.Length < 3)
                    {
                        _symbol += ".";
                    }
                    else
                    {
                        _symbol = ".";
                    }
                    Console.WriteLine(_symbol);
                    Thread.Sleep(500);
                }
            });

            return;
            ConcurrentDictionary<string, string> dict = new ConcurrentDictionary<string, string>();

            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            watch.Start();
            Parallel.For(0, 1000, (i, state) =>
            {
                if (bag.Count == 300)
                {
                    state.Stop();
                    return;
                }
                bag.Add(i);
            });
            watch.Stop();
            Console.WriteLine("Bag count is " + bag.Count + ", " + watch.ElapsedMilliseconds);

            return;
            Task<Int32> t = new Task<Int32>(n => Sum((Int32)n), 1000);
            t.Start();

            Task cwt = t.ContinueWith(task => Console.WriteLine("The result is {0}", t.Result));


            return;

            var parallelTest = new ParallelTest();
            parallelTest.ParallelInvokeMethod();
            parallelTest.ParallelForMethod();

            return;

            int threadCount = 0;
            while (threadCount < 100)
            {
                var thread = new Thread(() =>
                 {
                     threadCount++;
                     Console.WriteLine("线程：{0}；总线程数：{1}", Thread.CurrentThread.ManagedThreadId, threadCount);
                 });

                thread.IsBackground = true;
                thread.Start();
            }
        }

        private static Int32 Sum(Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; --n)
                checked { sum += n; } //结果太大，抛出异常
            return sum;
        }

        private void Example1()
        {
            var t1 = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("不带参数的线程{0}", Thread.CurrentThread.ManagedThreadId);
            }));

            var t2 = new Thread(new ParameterizedThreadStart((obj) =>
            {
                Console.WriteLine("带参数的线程{0}，参数为：{1}", Thread.CurrentThread.ManagedThreadId, obj);
            }));

            t1.IsBackground = true;
            t2.IsBackground = true;

            t1.Start();
            t2.Start("hello");
        }

        /// <summary>
        /// 同时运行n个任务,但同一时刻最多运行x个线程
        /// </summary>
        private void runMTaskPool()
        {
            var rnd = new Random();
            var lst = new TaskPool();
            for (var i = 0; i < 100; i++)
            {
                var s = rnd.Next(10);
                var j = i;
                var testTask = new Action(() =>
                {
                    Console.WriteLine(string.Format("第{0}个任务（用时{1}秒）已经开始", j, s));
                    Thread.Sleep(s * 1000);
                    Console.WriteLine(string.Format("第{0}个任务（用时{1}秒）已经结束", j, s));
                });
                lst.Tasks.Add(testTask);
            }
            lst.Completed += () => Console.WriteLine("没有更多的任务了！");
            lst.Start();
        }
    }
}
