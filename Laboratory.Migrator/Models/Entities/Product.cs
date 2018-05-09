namespace Laboratory.Migrator.Models.Entities
{
    public class Product
    {
        public Product(string name)
        {
            this.Name = name;
        }
        public Product(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
        }
        public Product(string name, decimal price, int categoryId)
        {
            this.Name = name;
            this.Price = price;
            this.CategoryId = categoryId;
        }

        public Product(int id, string name, decimal price, int categoryId)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.CategoryId = categoryId;
        }

        /// <summary>
        /// 产品编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 产品分类
        /// </summary>
        public virtual ProductCategory Category { get; set; }
    }
}
