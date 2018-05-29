using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Laboratory.Plugins
{
    public class Runner : IRunner
    {
        public string Name => "插件";

        public void Run()
        {
            var finalizers = new Dictionary<string, Action>();
            var assembly = Assembly.GetExecutingAssembly();
            var pluginType = typeof(IPlugin);
            var plugins = assembly.GetExportedTypes()
                .Where(w => w.GetInterfaces().Contains(pluginType))
                .ToList();

            foreach (var plugin in plugins)
            {
                var constructor = plugin.GetConstructor(Type.EmptyTypes);

                if (constructor != null)
                {
                    var instance = constructor.Invoke(null) as IPlugin;
                    var name = plugin.Name;
                    finalizers.Add(name, instance.SaveAs);
                }
            }
        }
    }
}
