using Dapper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace lab.impala
{
    class Program
    {
        static void Main(string[] args)
        {
            DMP();
            //Export();
        }

        static void Export()
        {
            string exportFilePath = "D:\\export.xlsx";

            var regions = new string[] { "鲁豫", "西南", "华北", "华南1", "华南2", "东北", "浙沪", "西北", "湘鄂", "苏皖" };
            var models = new string[] { "YARIS", "CAMRY", "LEVHV", "LYRS", "HLDER", "IMPTC", "EZHI", "LEVIN", "CMYHV", "YRSL", "NEWHR" };
            // EPPlus
            using (var pkg = new ExcelPackage())
            {
                for (int k = 0; k < 2; k++)
                {
                    var worksheet = pkg.Workbook.Worksheets.Add($"Sheet{k}");
                    {
                        int row = 1, col = 1;


                        #region 全局样式

                        worksheet.DefaultColWidth = 6;
                        worksheet.Cells.Style.Font.Name = "等线";
                        worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        //worksheet.Cells.Style.Border.BorderAround(ExcelBorderStyle.Dashed, Color.Black);

                        worksheet.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells.Style.Border.Top.Color.SetColor(Color.Black);

                        worksheet.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells.Style.Border.Right.Color.SetColor(Color.Black);

                        worksheet.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells.Style.Border.Bottom.Color.SetColor(Color.Black);

                        worksheet.Cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells.Style.Border.Left.Color.SetColor(Color.Black);

                        #endregion

                        #region 局部样式

                        var freezeHeaderStyle = worksheet.Workbook.Styles.CreateNamedStyle($"freezeHeader{k}");
                        freezeHeaderStyle.Style.Font.Size = 11;
                        freezeHeaderStyle.Style.Font.Bold = true;
                        freezeHeaderStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        freezeHeaderStyle.Style.Fill.BackgroundColor.SetColor(1, 180, 198, 231);
                        freezeHeaderStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        freezeHeaderStyle.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        freezeHeaderStyle.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        freezeHeaderStyle.Style.Border.Top.Color.SetColor(Color.Black);

                        freezeHeaderStyle.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        freezeHeaderStyle.Style.Border.Right.Color.SetColor(Color.Black);

                        freezeHeaderStyle.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        freezeHeaderStyle.Style.Border.Bottom.Color.SetColor(Color.Black);

                        freezeHeaderStyle.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        freezeHeaderStyle.Style.Border.Left.Color.SetColor(Color.Black);

                        #endregion


                        worksheet.View.FreezePanes(6, 2);

                        #region 冻结行首

                        // 每一区域下总栏数，+1 标识”全车系“
                        var regionCols = models.Length * 3;

                        // 所有栏数，+1 标识“全国”
                        var totalRegionCols = regions.Length * regionCols;

                        var titleCells = worksheet.Cells[row, col, ++row, col + totalRegionCols];
                        titleCells.Value = "全国入库台次情况";
                        titleCells.Merge = true;
                        titleCells.Style.Font.Size = 24;
                        titleCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                        var freezeCol = worksheet.Cells[++row, col, row + 2, col];
                        worksheet.Column(col).Width = 16;
                        freezeCol.Value = "日期";
                        freezeCol.Merge = true;
                        freezeCol.StyleName = freezeHeaderStyle.Name;

                        for (int i = 0, j = regions.Length; i < j; i++)
                        {
                            // 区域维度
                            var areaDimension = worksheet.Cells[row, col + i * regionCols + 1,
                                row, col + (i + 1) * regionCols];

                            areaDimension.Value = regions[i];
                            areaDimension.Merge = true;
                            areaDimension.StyleName = freezeHeaderStyle.Name;

                            // 下一栏col起始列
                            var nextCol = col + i * regionCols;

                            for (int m = 0, n = models.Length; m < n; m++)
                            {
                                var vehicleDimension = worksheet.Cells[row + 1, nextCol + m * 3 + 1,
                                     row + 1, nextCol + (m + 1) * 3];
                                vehicleDimension.Value = models[m];
                                vehicleDimension.Merge = true;
                                vehicleDimension.StyleName = freezeHeaderStyle.Name;

                                var storageNumCell = worksheet.Cells[row + 2, nextCol + m * 3 + 1];

                                worksheet.Column(nextCol + m * 3 + 1).Width = 12.5;
                                storageNumCell.Value = "实绩（台）";
                                storageNumCell.StyleName = freezeHeaderStyle.Name;
                                storageNumCell.Style.Font.Bold = false;

                                var storageYoYCell = worksheet.Cells[row + 2, nextCol + m * 3 + 2];
                                storageYoYCell.Value = "同比";
                                storageYoYCell.StyleName = freezeHeaderStyle.Name;
                                storageYoYCell.Style.Font.Bold = false;

                                var storageQoQCell = worksheet.Cells[row + 2, nextCol + m * 3 + 3];
                                storageQoQCell.Value = "环比";
                                storageQoQCell.StyleName = freezeHeaderStyle.Name;
                                storageQoQCell.Style.Font.Bold = false;
                            }
                        }

                        #endregion

                        #region 填充数据



                        #endregion

                        //worksheet.Cells[1, 1, row, ++col].AutoFitColumns(5.5);
                    }
                }

                #region saveas

                var exportFile = new FileInfo(exportFilePath);
                if (exportFile.Exists) exportFile.Delete();
                pkg.SaveAs(exportFile);

                #endregion
            }
        }

        static void DMP()
        {
            try
            {
                var appId = "icm-znkh";
                var appSecret = "4b21258ec1cc446e99bee31a5d7317ad";

                var openServ = new OpenWeChatService.QyhWeixinUtilServiceClient();
                var signDict = new Dictionary<string, string>()
                {
                    { "callSystem", appId },
                    { "code","HJehlBgTaOP0ad0RZWVj3oTIlHaRq5TThyXYFVCMuP8" },
                    { "nonceStr","Next12" },
                    { "timestamp",(DateTime.UtcNow - DateTime.Parse("1970/01/01")).TotalSeconds.ToString("f0") },
                    { "userId","" }
                };

                var signatureText = string.Join("&", signDict
                    .Where(w => !string.IsNullOrWhiteSpace(w.Value))
                    .OrderBy(o => o.Key)
                    .Select(q => $"{q.Key}={q.Value}")
                    .Concat(new string[] { "key=" + appSecret }));

                var signature = string.Join("", SHA1.Create()
                    .ComputeHash(Encoding.UTF8.GetBytes(signatureText))
                    .Select(q => q.ToString("x2")));

                var userResponse = openServ.getLocalQyhUserByUserIdOrCode(
                    signDict["userId"],
                    signDict["code"],
                    signDict["callSystem"],
                    signDict["nonceStr"],
                    signDict["timestamp"],
                    signature);

                Console.WriteLine($"{nameof(userResponse)}: {userResponse}");

                /**
                var tokenResponse = openServ.getAccessToken(signDict["callSystem"], signDict["nonceStr"], signDict["timestamp"], signature);
                Console.WriteLine($"{nameof(tokenResponse)}: {tokenResponse}");
                */
                Console.ReadLine();

                return;
                var connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

                Console.WriteLine($"connectionString: {connectionString}");

                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.ConnectionTimeout = 5;
                    connection.Open();
                    var commandText = new StringBuilder(" SELECT * FROM dl_dlrtag_dev.kpi_storage_year_num_all ");
                    commandText.Append(" where quota = '1' AND region_code = 'C0000' AND balances_date = 2019 ");
                    commandText.Append(" AND car_type = ? ");
                    commandText.Append(" AND city_code = ? ");

                    var param = new
                    {
                        car_type = "CAMRY",
                        city_code = "CAE001"
                    };

                    using (var reader = connection.ExecuteReader(commandText.ToString(), param))
                    {
                        // kpi_storage_day_num_all.dlr_regionname
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write($"{reader[i]} ");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"异常：{(e.InnerException ?? e).Message}");
            }
            Console.ReadLine();
        }
    }
}
