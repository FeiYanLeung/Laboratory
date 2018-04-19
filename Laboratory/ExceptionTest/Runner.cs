using System;
using System.Reflection;

namespace Laboratory.ExceptionTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get
            {
                return "异常";
            }
        }
        public void Run()
        {
            try
            {
                Cat cat = new Cat();
                cat.Sleep();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}：{1}", MethodBase.GetCurrentMethod().DeclaringType.Name, e.TargetSite.DeclaringType.Name);
            }
        }
    }
}
