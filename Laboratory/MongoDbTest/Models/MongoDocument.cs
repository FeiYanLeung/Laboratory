using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Laboratory.MongoDbTest
{
    #region 缓存对象实体模型

    public class MongoDocument
    {
        public MongoDocument() { }
        public MongoDocument(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        public MongoDocument(string key, object value, int expir)
        {
            this.Key = key;
            this.Value = value;
            this.Expir = expir;
        }

        public ObjectId _id { get; set; }
        public string Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 过期时间,Unix时间戳
        /// </summary>
        public int Expir { get; set; }
        /// <summary>
        /// 滑动过期时间
        /// </summary>
        public decimal SlidingExpir { get; set; }
    }

    #endregion
}
