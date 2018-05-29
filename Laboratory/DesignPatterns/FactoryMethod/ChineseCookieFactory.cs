namespace Laboratory.DesignPatterns.FactoryMethod
{
    public class ChineseCookieFactory : CookieFactory
    {
        public override Cookie CreateCookieFactory()
        {
            return new ChineseCookie();
        }
    }
}
