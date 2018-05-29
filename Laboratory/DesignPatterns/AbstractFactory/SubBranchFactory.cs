namespace Laboratory.DesignPatterns.AbstractFactory
{
    public class SubBranchFactory : AbstractFactory
    {
        public override Cookie MakeCookie()
        {
            return new SubBranchCookie();
        }

        public override Noodle MakeNoodles()
        {
            return new SubBranchNoodle();
        }
    }
}
