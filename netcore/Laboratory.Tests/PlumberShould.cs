using Laboratory.NetCore.Tests.Models;
using System;
using Xunit;

namespace Laboratory.NetCore.Tests
{
    [Trait("class", "plumber_should")]
    public class PlumberShould
    {
        #region Float Assert

        /// <summary>
        /// Assert.Equal/precision:3
        /// <remark>
        /// 精度验证
        /// </remark>
        /// </summary>
        [Fact]
        [Trait("category", "float")]
        public void HaveCorrectSalary()
        {
            var plumber = new Plumber();
            var expected = Math.Round(plumber.TotalReward / plumber.Hours, 2);  //precision：2|4

            Assert.Equal(expected, plumber.Salary, precision: 3);
        }

        #endregion

        #region String Assert

        /// <summary>
        /// Assert.Null
        /// </summary>
        [Fact]
        [Trait("category", "string")]
        public void NotHaveNameByDefault()
        {
            var plumber = new Plumber();
            Assert.Null(plumber.Name);
        }

        /// <summary>
        /// Assert.NotNull
        /// </summary>
        [Fact]
        [Trait("category", "string")]
        public void HaveNameValue()
        {
            var plumber = new Plumber
            {
                Name = "Brian"
            };
            Assert.NotNull(plumber.Name);
        }

        #endregion

        #region Collection Assert

        /// <summary>
        /// <![CDATA[ Assert.Contains<ICollection> ]]>
        /// </summary>
        [Fact]
        [Trait("category","collection")]
        public void HaveScrewdriver()
        {
            var plumber = new Plumber();
            Assert.Contains("螺丝刀", plumber.Tools);
        }

        /// <summary>
        /// <![CDATA[ Assert.DoesNotContain<ICollection> ]]>
        /// </summary>
        [Fact]
        [Trait("category", "collection")]
        public void NotHaveKeyboard()
        {
            var plumber = new Plumber();
            Assert.DoesNotContain("键盘", plumber.Tools);
        }

        /// <summary>
        /// Predicate筛选
        /// </summary>
        [Fact]
        [Trait("category", "collection")]
        public void HaveAtLeastOneScrewdriver()
        {
            var plumber = new Plumber();
            Assert.Contains(plumber.Tools, t => t.Contains("螺丝刀"));
        }

        /// <summary>
        /// 比较两个集合是否相等
        /// </summary>
        [Fact]
        [Trait("category", "collection")]
        public void HaveAllTools()
        {
            var plumber = new Plumber();
            var expectedTools = new[]
            {
                "螺丝刀",
                "扳子",
                "钳子"
            };
            Assert.Equal(expectedTools, plumber.Tools);
        }

        /// <summary>
        /// 比较两个集合内的元素是否相等
        /// </summary>
        [Fact]
        [Trait("category", "collection")]
        public void HaveNoEmptyDefaultTools()
        {
            var plumber = new Plumber();
            Assert.All(plumber.Tools, t => Assert.False(string.IsNullOrEmpty(t)));
        }

        #endregion

        /// <summary>
        /// 不被测试的方法
        /// </summary>
        [Fact(Skip = "IgnoreMethod")]
        [Trait("category", "ignore")]
        public void IgnoreMethod()
        {
            Assert.False(true);
        }
    }
}
