using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Laboratory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("运行模式：");
#if DEBUG
            Console.Write("DEBUG");
#else
            Console.Write("Release");
#endif
            Console.WriteLine();

            var iRunners = new List<IRunner>();
            var iType = typeof(IRunner);
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(sm => sm.GetExportedTypes().Where(w => w.GetInterfaces().Contains(iType)))
                .OrderBy(o => o.FullName)
                .ToList()
                .ForEach(e =>
                {
                    iRunners.Add(Activator.CreateInstance(e) as IRunner);
                });

            while (true)
            {
                var runnerCount = iRunners.Count;
                var lstLength = runnerCount.ToString().Length;
                for (int i = 0, j = runnerCount; i < j; i++)
                {
                    var runner = iRunners[i];
                    Console.WriteLine("[{0}]：{1}", i.ToString().PadLeft(lstLength, '0'), runner.Name);
                }

                Console.WriteLine("请选择运行序号(退出请输入 exit )：");

                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;
                else if ("exit".Equals(input.Trim().ToLower()))
                {
                    break;
                }
                else
                {
                    int runnerMaxIndex = iRunners.Count - 1;
                    var regInput = new Regex(@"^\d+$", RegexOptions.IgnorePatternWhitespace);
                    if (regInput.IsMatch(input))
                    {
                        var inputIndex = int.Parse(input);
                        if (inputIndex > runnerMaxIndex)
                        {
                            Console.WriteLine("请输入正确的序列号，序列号范围为：0-{0}", runnerMaxIndex);
                            continue;
                        }

                        var stopwatch = new Stopwatch();

                        var runner = iRunners[inputIndex];
                        Console.WriteLine("运行开始\r\n==========================");
                        stopwatch.Start();
                        runner.Run();
                        stopwatch.Stop();

                        Console.WriteLine("==========================\r\n运行结束,花费时间：{0}毫秒", stopwatch.ElapsedMilliseconds);
                    }
                    else
                    {
                        Console.WriteLine("请输入正确的序列号，序列号范围为：0-{0}", runnerMaxIndex);
                    }
                    continue;
                }
            }
        }
    }
}
