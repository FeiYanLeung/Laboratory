using System;
using System.Xml.Serialization;

namespace Laboratory.XmlConfigTest.Entities
{
    /// <summary>
    /// MongoDb配置
    /// </summary>
    [Serializable]
    [XmlRoot("mongo")]
    public sealed class MongoDbConfig
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        [XmlElement("connection")]
        public MongoConnection Connection { get; set; }

        /// <summary>
        /// 默认数据库
        /// </summary>
        [XmlElement("defaultDb")]
        public MongoDb DefaultDb { get; set; }

        /// <summary>
        /// 统计数据 库名
        /// </summary>
        [XmlElement("statisticsDb")]
        public MongoDb StatisticsDb { get; set; }

        /// <summary>
        /// 缓存数据库
        /// </summary>
        [XmlElement("cacheDb")]
        public MongoCacheDb MongoCache { get; set; }
    }

    /// <summary>
    /// db连接配置
    /// </summary>
    [Serializable]
    public sealed class MongoConnection
    {
        [XmlAttribute("value")]
        public string Value { get; set; }
    }

    /// <summary>
    /// db通用配置
    /// </summary>
    [Serializable]
    public class MongoDb
    {
        /// <summary>
        /// 统计数据库名称
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// 缓存数据库配置
    /// </summary>
    [Serializable]
    public sealed class MongoCacheDb : MongoDb
    {
        /// <summary>
        /// 缓存数据集合(表)名称
        /// </summary>
        [XmlElement("collection")]
        public string CollectionName { get; set; }
    }
}
