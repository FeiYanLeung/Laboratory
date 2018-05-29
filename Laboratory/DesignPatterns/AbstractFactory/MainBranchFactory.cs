namespace Laboratory.DesignPatterns.AbstractFactory
{
    public class MainBranchFactory : AbstractFactory
    {
        public override Cookie MakeCookie()
        {
            return new MainBranchCookie();
        }

        public override Noodle MakeNoodles()
        {
            return new MainBranchNoodle();
        }
    }
}
