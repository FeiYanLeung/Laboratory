using Laboratory.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratory.EncryptionTest
{
    public class Runner : IRunner
    {
        private EncryptionService _encryptionServ;

        public Runner()
        {
            this._encryptionServ = new EncryptionService();
        }

        public string Name
        {
            get
            {
                return "加密解密";
            }
        }

        public void Run()
        {
            var op = "20001";

            var et = this._encryptionServ.EncryptText(op);
            Console.WriteLine("1：" + et);

            var hexet = et.ToHex();
            Console.WriteLine("2：" + hexet);


            var ret = hexet.HexToString();
            Console.WriteLine("3：" + ret);

            var rop = this._encryptionServ.DecryptText(ret);
            Console.WriteLine("4：" + rop);



            return;
            var sortddict = new SortedDictionary<string, string>()
            {
                {"noncestr","3gv8cb2z"},
                {"ticket","C3060889D82A0926DB51AE53E7B8831A90F43E2B32D6238BAE801EE1595FB744"},
                {"timestamp","1500647159"},
                {"url",HttpUtility.UrlDecode($"http://crmm.test.{AppConfig.DOMAIN_SUFFIX}/index.html")},
            }
            .Select(q => String.Format("{0}={1}", q.Key, q.Value));

            var assemble = String.Join("&", sortddict);
            var signature = this._encryptionServ.SHA1(assemble);
            //37CD7377CC7AF261CBAC65FBDDB5A58B0B0470C4
            Console.WriteLine("target:{0} = signature:{1}", "68A9BA8DC8E79E317C8C5D913E5B70C13ADCE01B", signature);
        }
    }
}
