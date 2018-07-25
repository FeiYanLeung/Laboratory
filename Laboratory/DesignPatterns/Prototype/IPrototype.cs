namespace Laboratory.DesignPatterns.Prototype
{
    public interface IPrototype<T>
    {
        T Clone();
    }
}
