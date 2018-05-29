using System;

namespace Laboratory.DesignPatterns.Proxy
{
    public class ProxyImpls
    {
        public class EventImpl : IProxy
        {
            public void Invoke()
            {
                Console.WriteLine("EventImpl 被调用");
            }
        }

        public class ProxyEventImpl : IProxy
        {
            private readonly IProxy _proxy;
            public ProxyEventImpl(IProxy proxy)
            {
                this._proxy = proxy;
            }
            public void Invoke()
            {
                Console.WriteLine("ProxyEventImpl 被调用");
                this._proxy.Invoke();
            }
        }
    }
}
