using System;
using System.IO;
using System.Xml.Serialization;

namespace Laboratory.Core.Helpers
{
    public class SerializationHelper
    {
        public SerializationHelper() { }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="filename">文件路径</param>
        /// <returns></returns>
        public static object Load(Type type, string filename)
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    XmlSerializer serializer = new XmlSerializer(type);
                    return serializer.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public static void Save(object obj, string filename)
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(fs, obj);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
