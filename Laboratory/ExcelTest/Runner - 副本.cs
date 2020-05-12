using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Laboratory.ExcelTest
{
    public class Runner : IRunner
    {
        public string Name => "Excel";

        public void Run()
        {
            var excelFile = new FileInfo(@"D:\export.xlsx");
            if (excelFile.Exists) excelFile.Delete();

            var nodes = new List<Node>()
            {
                new Node(1, null, "OP-销售部分"),

                new Node(2, 1, "基础数据"),
                new Node(3, 2, "【A】新车销售目标台数"),
                new Node(4, 2, "【B】新车订单台数"),
                new Node(5, 2, "【C】新车销售台数"),
                new Node(6, 2, "【D】销售线索量"),
                new Node(7, 2, "【E】销售线索成交台数"),
                new Node(8, 2, "【F】销售线索成交率"),
                new Node(9, 2, "【G】销售线索成交率区域排名"),
                new Node(10, 2, "【H】区域店铺数量"),
                new Node(11, 2, "【I】销售线索24小时内跟进率"),

                new Node(12, 1, "应用KPI"),
                new Node(13, 12, "客户信息留存率"),
                new Node(14, 12, "试乘试驾率"),
                new Node(15, 12, "成交率"),

                 new Node(16, 1, "16"),
                 new Node(17, 16, "16.1")
            };

            int row = 1, column = 1;

            using (var package = new ExcelPackage(excelFile))
            {
                using (var worksheet = package.Workbook.Worksheets.Add("test"))
                {
                    var treeNodes = this.buildTreeNodes(nodes);

                    buildExcel(treeNodes, row, column, worksheet);

                    package.Save();
                }
            }
        }

        #region MyRegion

        #endregion

        #region Core

        /// <summary>
        /// 构建Excel
        /// </summary>
        /// <param name="treeNodes">树形结构</param>
        /// <param name="row">开始行</param>
        /// <param name="column">开始列</param>
        /// <param name="worksheet">工作薄</param>
        private void buildExcel(IEnumerable<TreeNode> treeNodes, int row, int column, ExcelWorksheet worksheet)
        {
            for (int i = 0, j = treeNodes.Count(); i < j; i++)
            {
                var treeNode = treeNodes.ElementAt(i);
                int totalNodeChildrens = 0;
                this.totalMaxDepthTreeNodes(treeNode, ref totalNodeChildrens);

                if (totalNodeChildrens > 0)
                {
                    var latestRow = row + totalNodeChildrens - 1;
                    var latestColumn = column;

                    var mergedCells = worksheet.Cells[row, latestColumn, latestRow, latestColumn];
                    mergedCells.Value = treeNode.Name;
                    mergedCells.Merge = true;
                    this.buildExcel(treeNode.Childrens, row, ++latestColumn, worksheet);
                    row += totalNodeChildrens;
                }
                else
                {
                    worksheet.Cells[row++, column].Value = treeNode.Name;
                }
            }
        }

        /// <summary>
        /// 构建树形菜单
        /// </summary>
        /// <param name="nodes">原始行数据</param>
        /// <returns></returns>
        private IEnumerable<TreeNode> buildTreeNodes(IEnumerable<Node> nodes)
        {
            var treeNodes = nodes.Select(q => new TreeNode(q)).ToList();

            return treeNodes.Where(node =>
           {
               var branchs = treeNodes.Where(child => node.Id == child.ParentId);

               if (branchs != null && branchs.Any()) node.Childrens = branchs.ToList();
               return !node.ParentId.HasValue || 0 == node.ParentId;
           });
        }


        /// <summary>
        /// 获取节点底层子菜单数量
        /// </summary>
        /// <param name="treeNode">树形结构节点</param>
        /// <param name="total">ref 节点总数</param>
        /// <returns></returns>
        private int totalMaxDepthTreeNodes(TreeNode treeNode, ref int total)
        {
            if (treeNode.Childrens.Count == 0)
            {
                ++total;
            }
            else
            {
                foreach (var child in treeNode.Childrens)
                {
                    totalMaxDepthTreeNodes(child, ref total);
                }
            }
            return total;
        }

        #endregion
    }
}
