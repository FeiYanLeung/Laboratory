using System;
using System.Linq.Expressions;

namespace Laboratory.LinqTest
{
    public class LinqExpression
    {
        public LinqExpression()
        {
            LabelTarget labelBreak = Expression.Label();
            ParameterExpression loopIndex = Expression.Parameter(typeof(int), "index");

            BlockExpression block = Expression.Block(
                new[] { loopIndex },
                // 初始化loopIndex值为1
                Expression.Assign(loopIndex, Expression.Constant(1)),
                Expression.Loop(

                    Expression.IfThenElse(
                        // 判断条件 loopIndex <= 5
                        Expression.LessThanOrEqual(loopIndex, Expression.Constant(5)),
                        // 判断条件返回True时执行的语句块
                        Expression.Block(
                            Expression.Call(null,
                            typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }),
                            Expression.Constant("Hello")),
                            Expression.PostIncrementAssign(loopIndex)),
                        // 判断条件返回False时执行的语句块
                        Expression.Break(labelBreak)
                        ),

                    labelBreak));

            Expression<Action> lambdaExpression = Expression.Lambda<Action>(block);
            lambdaExpression.Compile()
                .Invoke();
        }
    }
}
