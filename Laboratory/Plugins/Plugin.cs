using System;

namespace Laboratory.Plugins
{
    public class Plugin : IPlugin
    {
        public string Name => "插件项";

        public void SaveAs()
        {
            Console.WriteLine("开发人员1编写的插件");
        }
    }
}
