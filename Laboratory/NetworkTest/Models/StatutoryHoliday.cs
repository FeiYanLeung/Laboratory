using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Laboratory.NetworkTest
{
    /// <summary>
    /// 假期安排
    /// </summary>
    [Serializable, DataContract]
    public class StatutoryHoliday
    {
        /// <summary>
        /// 法定假日
        /// </summary>
        public StatutoryHoliday()
        {
            this.Days = new Dictionary<string, int>();
        }
        /// <summary>
        /// 月份
        /// <example>e.g. 201712</example>
        /// </summary>
        [DataMember]
        public string Month { get; set; }

        /// <summary>
        /// 日期及类型键值对
        /// <remarks>key:日期；value:类型(工作日：0, 休息日：1, 节假日：2)</remarks>
        /// <example>[{"01",1},{"10",2}]</example>
        /// </summary>
        [DataMember]
        public Dictionary<string, int> Days { get; set; }
    }
}