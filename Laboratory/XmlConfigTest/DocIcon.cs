
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
namespace Laboratory.XmlConfigTest
{
    [Serializable]
    [XmlRoot(ElementName = "Mapping")]
    public class DocIconMapping
    {
        [XmlElement(ElementName = "FileType")]
        public List<FileType> FileType { get; set; }
    }

    [Serializable]
    public class FileType
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        /// <summary>
        /// 对应Icon路径
        /// </summary>
        [XmlAttribute(AttributeName = "IconPath")]
        public string IconPath { get; set; }
        /// <summary>
        /// 文件类型归档
        /// </summary>
        [XmlElement(ElementName = "Extensions")]
        public string Extensions { get; set; }

        public FileType()
        {

        }
        public FileType(string _name, string _icon_path, string _extensions)
        {
            this.Name = _name;
            this.IconPath = _icon_path;
            this.Extensions = _extensions;
        }


        [OnSerializing]
        void OnSerializing(StreamingContext context)
        {
            Console.WriteLine("OnSerializing");
        }

        [OnSerialized]
        void OnSerialized(StreamingContext context)
        {
            Console.WriteLine("OnSerialized");
        }

        [OnDeserializing]
        void OnDeserializing(StreamingContext context)
        {
            Console.WriteLine("OnDeserializing");
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            Console.WriteLine("OnDeserialized");
        }
    }
}
