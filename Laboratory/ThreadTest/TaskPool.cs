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
