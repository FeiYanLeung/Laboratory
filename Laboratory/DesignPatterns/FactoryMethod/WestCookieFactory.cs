namespace Laboratory.DesignPatterns.FactoryMethod
{
    public class WestCookieFactory : CookieFactory
    {
        public override Cookie CreateCookieFactory()
        {
            return new WestCookie();
        }
    }
}
