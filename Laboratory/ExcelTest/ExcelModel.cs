using System.Collections.Generic;

namespace Laboratory.ExcelTest
{
    /// <summary>
    /// 用户输入数据
    /// </summary>
    public class Node
    {
        public Node(int id, int? parentId, string name)
        {
            this.Id = id;
            this.ParentId = parentId;
            this.Name = name;
        }

        public Node(int id, int? parentId, string name, string goal)
        {
            this.Id = id;
            this.ParentId = parentId;
            this.Name = name;
            this.Goal = goal;
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Goal { get; set; }
    }

    /// <summary>
    /// 根据用户输入数据生成的树形结构
    /// </summary>
    public partial class TreeNode : Node
    {
        public TreeNode(Node node)
            : base(node.Id, node.ParentId, node.Name, node.Goal)
        {
            this.Childrens = new List<TreeNode>();
        }

        public TreeNode(int id, int? parentId, string name)
            : base(id, parentId, name) { }

        public List<TreeNode> Childrens { get; set; }
    }

    /// <summary>
    /// 坐标数据
    /// </summary>
    public class CoordinateData
    {
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public string Value { get; set; }
        public string Tooltip { get; set; }
    }
}
