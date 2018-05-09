using System.Collections.Generic;

namespace Laboratory.Migrator.Models.Entities
{
    /// <summary>
    /// 产品分类
    /// </summary>
    public class ProductCategory
    {
        public ProductCategory()
        {
            this.Products = new List<Product>();
            this.SubCategories = new List<ProductCategory>();
        }
        public ProductCategory(string name)
        {
            this.Name = name;
        }
        public ProductCategory(string name, int parentId)
        {
            this.Name = name;
            this.ParentId = parentId;
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父菜单
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 产品分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 对应ParentId的分类信息
        /// </summary>
        public virtual ProductCategory ParentCategory { get; set; }

        /// <summary>
        /// 对应分类的子分类的信息
        /// </summary>
        public virtual ICollection<ProductCategory> SubCategories { get; set; }

        /// <summary>
        /// 产品's
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }
}
