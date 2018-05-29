using System;

namespace Laboratory.DesignPatterns.Strategy
{
    public class StrategyImpls
    {
        public class Strategy1Impl : IStrategy
        {
            public void Make()
            {
                Console.WriteLine("策略.1");
            }
        }

        public class Strategy2Impl : IStrategy
        {
            public void Make()
            {
                Console.WriteLine("策略.2");
            }
        }
    }
}
