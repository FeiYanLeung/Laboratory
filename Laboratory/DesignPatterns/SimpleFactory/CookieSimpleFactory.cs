namespace Laboratory.DesignPatterns.SimpleFactory
{
    public class CookieSimpleFactory
    {
        public static Cookie Make(char choice)
        {
            switch (choice)
            {
                case 'w':
                    return new WestCookie();
                default:
                    return new ChineseCookie();
            }
        }
    }
}
