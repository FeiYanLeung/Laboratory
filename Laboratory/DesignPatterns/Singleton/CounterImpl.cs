namespace Laboratory.DesignPatterns.Singleton
{
    public sealed class CounterImpl
    {
        private static readonly object locker = new object();

        private CounterImpl() { }

        private static Counter counter = null;
        public static Counter Instance
        {
            get
            {
                if (counter == null)
                {
                    lock (locker)
                    {
                        if (counter == null)
                        {
                            counter = new Counter();
                        }
                    }
                }
                return counter;
            }
        }
    }
}
