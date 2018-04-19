using Laboratory.ReflectTest.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Laboratory.ReflectTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "反射"; }
        }

        public void Run()
        {
            var token = new Token();
            var properties = typeof(Token).GetNavigationProperties();
            foreach (var property in properties)
            {
                Console.WriteLine(property.Name);
            }

            return;
            var tokenType = typeof(Token);
            var entityProperties = tokenType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in entityProperties)
            {
                Console.WriteLine(property.Name);
                Console.WriteLine("IsDefined [NotMappedAttribute] -> {0}", property.IsDefined(typeof(NotMappedAttribute)));
                Console.WriteLine("IsPrimitive(基元类型) -> {0}", property.PropertyType.IsPrimitive);
                Console.WriteLine("IsEnum -> {0}", property.PropertyType.IsEnum);
                Console.WriteLine("IsValueType(值类型) -> {0}", property.PropertyType.IsValueType);
                Console.WriteLine("IsString -> {0}", property.PropertyType.Equals(typeof(string)));
                Console.WriteLine("IsVirtual(虚函数) -> {0}", property.GetMethodInfo().IsVirtual);
                Console.WriteLine("IsOverride()(重写的字段) -> {0}", property.IsOverride());

                Console.WriteLine("=========================");
            }
        }

        private void loadAssembly()
        {
            var iType = typeof(IRunner);
            var instances = Assembly.GetExecutingAssembly()
                .GetExportedTypes()
                .Where(w => w.GetInterfaces().Contains(iType));

            if (instances != null && instances.Any())
            {
                //var instance = Activator.CreateInstance(item) as IRunner;
                foreach (var item in instances)
                {
                    Console.WriteLine(item.FullName);
                }
            }

        }
    }
}
