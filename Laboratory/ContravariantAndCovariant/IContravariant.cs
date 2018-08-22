namespace Laboratory.ContravariantAndCovariant
{
    /// <summary>
    /// 协变 <![CDATA[Foo<父类> = Foo<子类> ]]>
    /// </summary>
    
    /// <summary>
    /// 逆变 <![CDATA[Foo<子类> = Foo<父类>]]>
    /// </summary>
    
    public interface IBar<in T> { }
    //应该是in
    public interface IFoo<in T>
    {
        //void Test(IBar<T> bar);
    }
    //还是out
    //public interface IFoo<out T>
    //{
    //    void Test(IBar<T> bar);
    //}
}
