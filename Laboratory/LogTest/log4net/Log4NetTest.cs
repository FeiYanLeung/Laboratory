using log4net;
using System.IO;
using System.Reflection;

namespace Laboratory.LogTest
{
    public class Log4NetTest
    {
        /// <summary>
        /// log4net
        /// </summary>
        public static void Write()
        {
            var config = new FileInfo("./LogTest/log4net/log4net.config");
            if (config.Exists)
            {
                log4net.Config.XmlConfigurator.ConfigureAndWatch(config);
            }

            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            for (int i = 0; i < 100; i++)
            {
                log.Error("error" + i);
            }
        }
    }
}
