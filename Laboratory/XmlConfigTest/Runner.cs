using Laboratory.Core;
using Laboratory.XmlConfigTest.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Laboratory.XmlConfigTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get
            {
                return "XML操作";
            }
        }

        /// <summary>
        /// 初始化config
        /// </summary>
        private void initAppConfig()
        {
            #region aliyun

            var aliyun = new AliyunConfig()
            {
                App = new AliyunApp()
                {
                    Id = AppConfig.ALI_CLOUD_KEY,
                    Sercet = AppConfig.ALI_CLOUD_SECRET,
                    Signature = AppConfig.ALI_CLOUD_SIGNATURE
                },
                Apis = new List<Api>()
                {
                    new Api(){
                        Name="server-rest",
                        Url="https://eco.taobao.com/router/rest",
                        Description="淘宝 RESTful API"
                    }
                }
            };
            this.Save<AliyunConfig>("aliyun.config", aliyun);

            #endregion

            #region wechat

            var wechat = new WeChatConfig()
            {
                App = new AppInfo()
                {
                    Id = AppConfig.WX_KEY,
                    Sercet = AppConfig.WX_SECRET
                },
                Apis = new List<Api>()
                {
                    new Api(){
                        Name = "token",
                        Url="https://api.weixin.qq.com/sns/oauth2/access_token",
                        Description="授权请求地址"
                    },
                    new Api(){
                        Name ="user",
                        Url="https://api.weixin.qq.com/sns/userinfo",
                        Description="用户信息请求地址"
                    }
                }
            };
            this.Save<WeChatConfig>("wechat.config", wechat);

            #endregion

            #region mongodb

            var mongo = new MongoDbConfig()
            {
                Connection = new MongoConnection() { Value = AppConfig.MONGO_CONNECTION_STRING },
                DefaultDb = new MongoDb() { Name = "DefaultDb" },
                StatisticsDb = new MongoDb() { Name = "StatisticDb" },
                MongoCache = new MongoCacheDb() { Name = $"{AppConfig.PREFIX}Caching", CollectionName = "CacheCollection" }
            };

            this.Save<MongoDbConfig>("mongodb.config", mongo);

            #endregion

            #region getui

            var getui = new GetuiConfig()
            {
                Host = "http://sdk.open.api.igexin.com/apiex.htm",
                AppId = AppConfig.GT_ID,
                AppKey = AppConfig.GT_KEY,
                MasterSecret = AppConfig.GT_MASTER_SECRET
            };

            this.Save<GetuiConfig>("getui.config", getui);

            #endregion

            #region qiniu

            var qiniu = new QiniuConfig()
            {
                AccessKey = AppConfig.QINIU_ACCOUNT,
                SecretKey = AppConfig.QINIU_SECRET,
                Bucket = "bucket"
            };

            this.Save<GetuiConfig>("qiniu.config", getui);

            #endregion

            #region site

            var site = new SiteConfig()
            {
                MessageInterval = 10,
                MaxFileSize = 2,
                DefaultPic = "/Content/img/avatars/male.png",
                SHA1Salt = AppConfig.SHA1_SALT,
                Version = "1.0.141027 内测版",

                #region Domain

                Domains = new List<Domain>(){
                    new Domain () { Name="fileserver" ,Url= "http://files.test.gu.com/"},
                    new Domain () { Name="webserver" ,Url= "http://www.test.gu.com/"},
                    new Domain () { Name="homepage", Url="http://www.test.gu.com/"},
                    new Domain () { Name="webapi" , Url="http://open.gu.com/"}
                },

                #endregion

                #region Email

                Email = new EmailConfig()
                {
                    From = AppConfig.EMAIL_ACCOUNT,
                    UserName = AppConfig.APP,
                    SMTP = "smtp.163.com",
                    Port = 25,
                    NickName = AppConfig.APP,
                    Password = AppConfig.EMAIL_PWD,
                    SSL = false,
                },

                #endregion

                #region MSMQ

                MSMQ = $@"FormatName:Direct=TCP:{AppConfig.DB_IP}\private$\",

                #endregion

                #region IM

                IM = new IMConfig()
                {
                    Host = $"imhost.test.{AppConfig.DOMAIN_SUFFIX}",
                    Port = 5222
                }

                #endregion
            };

            this.Save<SiteConfig>("site.config", site);

            #endregion
        }

        public void Run()
        {
            XPathTest();
            return;

            this.initAppConfig();

            return;
            DocIconMapping mapping = new DocIconMapping();

            List<FileType> lst = new List<FileType>();
            lst.Add(new FileType()
            {
                Name = "excel",
                IconPath = "icon-excel.png",
                Extensions = "xls,xlsx"
            });
            lst.Add(new FileType()
            {
                Name = "text",
                IconPath = "icon-text.png",
                Extensions = "txt,ini,log"
            });

            mapping.FileType = lst;

            this.Save<DocIconMapping>("doc.xml", mapping);
        }

        /// <summary>
        /// 保存xml到文件
        /// </summary>
        /// <param name="path">保存路径</param>
        /// <param name="entity">保存对象</param>
        private void Save<T>(string path, T entity) where T : class
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                //创建XML命名空间
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(fs, entity, ns);
            }
        }

        public void Test2()
        {
            XmlDocument document = new XmlDocument();
            XmlElement element = document.CreateElement("mapping");

            XmlElement item = document.CreateElement("type");
            item.SetAttribute("name", "excel");
            item.SetAttribute("path", "icon-excel.png");

            XmlElement item_content = document.CreateElement("item");
            item_content.InnerText = "xls";
            XmlElement item_content2 = document.CreateElement("item");
            item_content2.InnerText = "xlsx";

            item.AppendChild(item_content);
            item.AppendChild(item_content2);


            element.AppendChild(item);
            document.AppendChild(element);

            document.Save("doc.xml");
        }


        public void XPathTest()
        {
            #region XPath构建XML

            var xdoc = new XDocument(
                new XElement("app",
                    new XElement("uniqueidentifier", Guid.NewGuid().ToString("n")),
                    new XElement("debug", false),
                    new XElement("environment",
                        new XElement("platform", "x86|x64|ARM"),
                        new XElement("hardware",
                            new XAttribute("ram", "8GB"),
                            new XAttribute("rom", "64GB"),
                            new XText("minimum requirements."))
                    ))
            );

            xdoc.Save(Console.Out);
            Console.WriteLine();

            #endregion

            #region XPath方式

            Console.WriteLine("============= XPath.edit ==============");

            xdoc.XPathSelectElement("//environment/hardware[@ram=\"8GB\"]")
                .SetAttributeValue("ram", "2GB");

            xdoc.Save(Console.Out);
            Console.WriteLine();

            #endregion

            #region Linq 方式

            xdoc.Descendants("hardware")
                .FirstOrDefault(item => item.HasAttributes && item.Attribute("ram").Value == "2GB")
                .SetAttributeValue("ram", "6GB");

            xdoc.Save(Console.Out);
            Console.WriteLine();

            #endregion
        }
    }
}
