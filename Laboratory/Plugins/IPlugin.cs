namespace Laboratory.Plugins
{
    /// <summary>
    /// 插件
    /// </summary>
    public interface IPlugin
    {
        string Name { get; }
        void SaveAs();
    }
}
