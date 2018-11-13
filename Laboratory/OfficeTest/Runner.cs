using Laboratory.OfficeTest.Interfaces;
using System.Collections.Generic;

namespace Laboratory.OfficeTest
{
    public class Runner : IRunner
    {
        public string Name => "Office相关操作";

        public void Run()
        {
            IBuilder builder = new EPPBuilder();
            builder.BuildExcel("/export/excel", new List<Models.QuoteCompareExportViewModel>()
            {
                new Models.QuoteCompareExportViewModel(){
                    Source = 1,
                    SourceName = "不见不散"
                }
            });
        }
    }
}
