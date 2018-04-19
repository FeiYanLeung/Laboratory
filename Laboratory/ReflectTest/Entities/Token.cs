using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratory.ReflectTest.Entities
{
    [Serializable]
    public class BaseToken
    {
        /// <summary>
        /// virtual修饰的字段
        /// </summary>
        public virtual int IntUID { get; set; }
    }

    [Serializable]
    public class Token : BaseToken
    {
        /// <summary>
        /// 可为空的值类型字段
        /// </summary>
        public int? IntValueId { get; set; }

        /// <summary>
        /// 重写父类的字段
        /// </summary>
        public override int IntUID { get; set; }

        /// <summary>
        /// 包含NotMappedAttrbute的字段
        /// </summary>
        [NotMapped]
        public string StrUnionID { get; set; }

        /// <summary>
        /// bool类型
        /// </summary>
        public bool IsValidate { get; set; }

        /// <summary>
        /// 引用类型
        /// </summary>
        public TokenParameter Parameter { get; set; }

        /// <summary>
        /// 枚举
        /// </summary>
        public EnumToken EntityEnum { get; set; }

        /// <summary>
        /// 可为空值类型
        /// </summary>
        public Guid? GuidEmp { get; set; }

        /// <summary>
        /// 集合
        /// </summary>
        public ICollection<int> lstInt { get; set; }

        /// <summary>
        /// virtual字段
        /// </summary>
        public virtual ICollection<int> lstVirtualInt { get; set; }

        /// <summary>
        /// virtual对象
        /// </summary>
        public virtual BaseToken VirtualBaseToken { get; set; }

        /// <summary>
        /// 导航属性
        /// </summary>
        public virtual ICollection<BaseToken> lstVirtualProperty { get; set; }
    }
}
