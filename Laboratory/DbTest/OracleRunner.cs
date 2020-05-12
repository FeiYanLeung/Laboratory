using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Laboratory.DbTest
{
    public class OracleRunner : IRunner
    {
        public string Name => "Oracle测试";

        public void Run()
        {
            var watch = new Stopwatch();
            watch.Start();

            var connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=14.23.112.50)(PORT=8333))(CONNECT_DATA=(SID=CVM)));User ID=CVM;Password=CVM";
            var SQLString = "SELECT COUNT(0) TOTAL FROM DUAL";

            var totals = 200;
            var errors = new List<string>();

            Parallel.For(0, totals, (i) =>
                      {
                          Console.WriteLine($"正在打开第{i + 1}个连接");
                          try
                          {
                              using (var connection = new OracleConnection(connectionString))
                              {
                                  connection.Open();
                                  Console.WriteLine($"第{i + 1}个连接已开启成功");

                                  using (OracleCommand cmd = new OracleCommand(SQLString, connection))
                                  {
                                      using (var reader = cmd.ExecuteReader())
                                      {
                                          while (reader.Read())
                                          {
                                              Console.Write(reader["TOTAL"].ToString());
                                          }
                                      }
                                  }
                              }
                              Console.WriteLine();
                          }
                          catch (Exception e)
                          {
                              errors.Add($"处理第{i + 1}个连接失败:{(e.InnerException ?? e).Message}");
                          }
                      });


            Console.WriteLine($"执行结果(共{totals})：失败{errors.Count}次 ");

        }
    }
}
