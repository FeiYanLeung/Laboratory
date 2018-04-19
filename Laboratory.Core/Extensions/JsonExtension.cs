using Newtonsoft.Json;

namespace Laboratory.Core
{
    public static class JsonExtension
    {
        /// <summary>
        /// 将对象序列化成Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 将对象序列化成Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 将Json反序列化成对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ToModel<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
