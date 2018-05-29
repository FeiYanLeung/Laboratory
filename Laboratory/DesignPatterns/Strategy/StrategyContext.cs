namespace Laboratory.DesignPatterns.Strategy
{
    public class StrategyContext
    {
        private readonly IStrategy _strategy;
        public StrategyContext(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        public void Make()
        {
            this._strategy.Make();
        }
    }
}
