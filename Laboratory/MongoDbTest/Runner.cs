using Laboratory.Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laboratory.MongoDbTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get
            {
                return "MongoDb";
            }
        }

        public void Run()
        {
            r1();
        }

        /// <summary>
        /// 基础操作
        /// </summary>
        public void r1()
        {
            string separator = "_";
            var mongoClient = new MongoClient(AppConfig.MONGO_CONNECTION_STRING);
            var mongoDb = mongoClient.GetDatabase($"test_{AppConfig.PREFIX}Caching");
            var docs = mongoDb.GetCollection<MongoDocument>("Records");

            var key = "key";
            var region = "region";

            var _filter_docs = docs.Find<MongoDocument>(Builders<MongoDocument>.Filter.Empty).ToList();

            if (!BsonClassMap.IsClassMapRegistered(typeof(MongoDocument)))
            {
                BsonClassMap.RegisterClassMap<MongoDocument>();
            }

            foreach (var item in _filter_docs)
            {
                Console.WriteLine("_id=>{0}, k=>{1}, v=>{2}", item._id, item.Key, item.Value);

                var filter = Builders<MongoDocument>.Filter.Eq("_id", item._id);

                var update = Builders<MongoDocument>.Update
                    .Set("Key", "K 1")
                    .Set("Expir", 3000)
                    .Set("SlidingExpir", 3000);

                update = update.Set("Key", "K 2");


                //删除字段：删除字段需要放置到最后执行，因为Replace或者其他操作会覆盖此次删除的字段信息
                //update = update.Unset((m) => m.Expir);
                //docs.FindOneAndUpdate<MongoDocument>(Builders<MongoDocument>.Filter.Empty, Builders<MongoDocument>.Update.Unset((m) => m.Expir));
                docs.UpdateMany<MongoDocument>((m) => m.SlidingExpir != -1, Builders<MongoDocument>.Update.Unset((m) => m.SlidingExpir));

                //docs.UpdateOne(filter, update);

                docs.FindOneAndReplace<MongoDocument>(filter, item);
            }


            return;

            docs.InsertOne(new MongoDocument()
            {
                Key = string.Join(separator, key, region),
                Value = new MongoDocument(key, region), //DateTime.Now.ToUnixTimestamp(),
                Expir = DateTime.Now.AddDays(1).ToUnixTimestamp()
            });



            //docs.DeleteMany(Builders<MongoDocument>.Filter.Lte("Expir", 1516809945));

            //return;
            var conditions = new List<FilterDefinition<MongoDocument>>()
            {
                Builders<MongoDocument>.Filter.Regex("Key", "/"+region+"$/")
            };

            var filters = new List<FilterDefinition<MongoDocument>>() {
                Builders<MongoDocument>.Filter.Gte("Expir", DateTime.Now.ToUnixTimestamp())
            };

            if (conditions.Count > 0) filters.AddRange(conditions);

            var result = docs.Find(Builders<MongoDocument>.Filter.Gte("Expir", DateTime.Now.ToUnixTimestamp()))
                .ToList();

            var result2 = docs.Find(Builders<MongoDocument>.Filter.And(filters))
                .ToList();

            Console.WriteLine("result1 keys:{0}", String.Join(",", result.Select(q => q.Key)));
            Console.WriteLine("result2 keys:{0}", String.Join(",", result2.Select(q => q.Key)));
        }
    }
}
