using Laboratory.Migrator.Models;
using System;
using System.Linq;
namespace Laboratory.Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new DataContext())
            {
                context.Configuration.UseDatabaseNullSemantics = false;

                if (context.ProductCategories.Any())
                {

                }
            }

            Console.ReadLine();
        }
    }
}
