using System;
using System.Collections.Generic;
using System.Threading;

namespace Laboratory.ThreadTest
{
    public class TaskPool
    {
        public List<Action> Tasks = new List<Action>();

        public event Action Completed;

        public void Start()
        {
            for (var i = 0; i < 5; i++)
                StartAsync();
        }

        public void StartAsync()
        {
            lock (Tasks)
            {
                #region 检测线程使用情况

                //获得最大的线程数量  
                ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int miot);
                //获得可用的线程数量  
                ThreadPool.GetAvailableThreads(out int availableWorkerThreads, out int aiot);
                Console.WriteLine("当前线程数：{0}", maxWorkerThreads - availableWorkerThreads);

                #endregion

                if (Tasks.Count > 0)
                {
                    var t = Tasks[Tasks.Count - 1];
                    Tasks.Remove(t);
                    ThreadPool.QueueUserWorkItem(h =>
                    {
                        t();
                        StartAsync();
                    });
                }
                else if (Completed != null)
                    Completed();
            }
        }
    }
}
