using Laboratory.Migrator.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace Laboratory.Migrator.Models.DbInitializer
{
    public sealed class SQLDbinitializer : DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            new List<ProductCategory>() {
                new ProductCategory(){
                    Name = "文具",
                    Products = new List<Product>(){
                        new Product("铅笔",1.5M),
                        new Product("墨水",2),
                        new Product("钢笔",29.99M),
                        new Product("笔记本",3.5M)
                    }
                },
                new ProductCategory(){
                    Name = "食品",
                    SubCategories = new List<ProductCategory>(){
                        new ProductCategory("水果"){
                            Products = new List<Product>(){
                                new Product("火龙果",6),
                                new Product("苹果",8),
                                new Product("橘子",8),
                                new Product("梨子",5),
                                new Product("杏子",10)
                            }
                        },
                        new ProductCategory("蔬菜"){
                            Products = new List<Product>(){
                                new Product("大白菜",2.1M),
                                new Product("小白菜",2.3M),
                                new Product("菠菜",1.5M),
                                new Product("豇豆",6),
                                new Product("生姜",8),
                                new Product("蒜苗",5)
                            }
                        },
                        new ProductCategory("初加工"){
                            Products = new List<Product>(){
                                new Product("腊肉",30),
                                new Product("干黄花菜",20),
                                new Product("活豆腐",2),
                                new Product("生菜油",15)
                            }
                        },
                        new ProductCategory("熟食"){
                            Products = new List<Product>(){
                                new Product("烤五花肉",42),
                                new Product("茶叶蛋",1.5M),
                                new Product("炸丸子",20)
                            }
                        }
                    }
                }
            }
            .ForEach(item =>
            {
                context.ProductCategories.Add(item);
            });
            context.SaveChanges();
        }
    }
}
