using System;
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

            var coordinateDatas = new List<CoordinateData>();

            var level2Axis = nodes.Where(w => w.ParentId.HasValue && nodes.Where(x => 1 == x.ParentId).Select(q => q.Id).Contains(w.ParentId.Value));

            var random = new Random();
            foreach (var item in level2Axis)
            {
                for (int i = 0; i < 12; i++)
                {
                    var m = (i + 10) % 12;
                    var YAxisValue = $"{(m == 0 ? 12 : m)} 月";

                    coordinateDatas.Add(new CoordinateData()
                    {
                        XAxis = item.Id.ToString(),
                        YAxis = YAxisValue,
                        Value = random.Next(1200).ToString("f2")
                    });
                }
            }


            using (var outputStream = new FileStream(excelFile.FullName, FileMode.CreateNew))
            {
                var excelBuildTemplate = new BuildExcelReportTemplate(coordinateDatas);
                excelBuildTemplate.Build(outputStream, nodes);
            }
        }
    }
}
