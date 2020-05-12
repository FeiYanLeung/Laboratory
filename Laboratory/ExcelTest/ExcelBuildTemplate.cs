using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Laboratory.ExcelTest
{
    public abstract class ExcelBuildTemplate
    {
        public abstract void Build(Stream outputStream, List<Node> nodes);

        #region Core

        /// <summary>
        /// 构建Excel
        /// </summary>
        /// <param name="treeNodes">树形结构</param>
        /// <param name="row">开始行</param>
        /// <param name="column">开始列</param>
        /// <param name="worksheet">工作薄</param>
        /// </param>
        protected virtual void buildExcel(IEnumerable<TreeNode> treeNodes, int row, int column, ExcelWorksheet worksheet, Action<TreeNode, int, int> onFillCellValue = null)
        {
            for (int i = 0, j = treeNodes.Count(); i < j; i++)
            {
                var treeNode = treeNodes.ElementAt(i);
                int totalNodeChildrens = 0;
                this.TotalMaxDepthTreeNodes(treeNode, ref totalNodeChildrens);

                if (treeNode.Childrens.Count == 0 && null != onFillCellValue)
                {
                    onFillCellValue.Invoke(treeNode, row, column);
                }

                var latestRow = row + totalNodeChildrens - 1;
                var latestColumn = column;

                var mergedCells = worksheet.Cells[row, latestColumn, latestRow, latestColumn];
                mergedCells.Value = treeNode.Name;
                mergedCells.Merge = true;
                this.buildExcel(treeNode.Childrens, row, ++latestColumn, worksheet, onFillCellValue);
                row += totalNodeChildrens;
            }
        }

        /// <summary>
        /// 构建树形菜单
        /// </summary>
        /// <param name="nodes">原始行数据</param>
        /// <returns></returns>
        protected virtual IEnumerable<TreeNode> BuildTreeNodes(IEnumerable<Node> nodes)
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
        protected virtual int TotalMaxDepthTreeNodes(TreeNode treeNode, ref int total)
        {
            if (treeNode.Childrens.Count == 0)
            {
                ++total;
            }
            else
            {
                foreach (var child in treeNode.Childrens)
                {
                    TotalMaxDepthTreeNodes(child, ref total);
                }
            }
            return total;
        }

        /// <summary>
        /// 获取最大深度层级
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        protected virtual int MaxDepthLevelTree(TreeNode treeNode, ref int level, bool includeSelf = true)
        {
            if (includeSelf && 0 == level) level = 1;

            if (treeNode.Childrens.Count > 0)
            {
                ++level;
                if (treeNode.Childrens.Any(w => w.Childrens.Count > 0)) this.MaxDepthLevelTree(treeNode.Childrens.First(w => w.Childrens.Count > 0), ref level, includeSelf);
            }

            return ++level;
        }

        #endregion
    }

    public class BuildExcelReportTemplate : ExcelBuildTemplate
    {
        private readonly List<CoordinateData> _coordinateDatas;
        public BuildExcelReportTemplate(List<CoordinateData> coordinateDatas)
        {
            this._coordinateDatas = coordinateDatas;
        }

        public override void Build(Stream outputStream, List<Node> nodes)
        {
            int row = 1, column = 1;

            using (var package = new ExcelPackage(outputStream))
            {
                using (var worksheet = package.Workbook.Worksheets.Add("Sheet1"))
                {
                    // 构建树形菜单
                    var treeNodes = this.BuildTreeNodes(nodes);

                    #region 构建 Excel 表头

                    var maxDepth = 0;
                    this.MaxDepthLevelTree(treeNodes.ElementAt(0), ref maxDepth, true);

                    var yAxisValues = this._coordinateDatas.GroupBy(gb => gb.YAxis)
                        .Select(q => q.Key);

                    var maxColumn = maxDepth + yAxisValues.Count();

                    #region 标题

                    var headerTitleCells = worksheet.Cells[row, column, ++row, maxColumn];
                    headerTitleCells.Value = "2018年e-CRB自主研究会成员店月度KPI报表";
                    headerTitleCells.Merge = true;
                    headerTitleCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    headerTitleCells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    #endregion

                    #endregion

                    #region 数据分类名称

                    var yAxisFixedColumns = new string[] { "序号", "分类", "KPI 项目" };
                    yAxisFixedColumns = yAxisFixedColumns.Union(yAxisValues).ToArray();

                    ++row;
                    for (int i = 0, j = yAxisFixedColumns.Length; i < j; i++)
                    {
                        var xAxisFixedColumn = yAxisFixedColumns[i];
                        var xAxisFixedCell = worksheet.Cells[row, column + i];

                        xAxisFixedCell.Value = xAxisFixedColumn;
                    }

                    #endregion

                    #region 构建分类树形结构，合并单元格

                    buildExcel(treeNodes, ++row, column, worksheet, (onTreeNode, onRow, onColumn) =>
                    {
                        foreach (var yAxisValue in yAxisValues)
                        {
                            var coordinateData = this._coordinateDatas.FirstOrDefault(w => onTreeNode.Id.ToString() == w.XAxis && yAxisValue.Equals(w.YAxis));
                            worksheet.Cells[onRow, ++onColumn].Value = coordinateData.Value;
                        }
                    });

                    #endregion

                    // 输出 Excel 流
                    package.Save();
                }
            }
        }

    }
}
