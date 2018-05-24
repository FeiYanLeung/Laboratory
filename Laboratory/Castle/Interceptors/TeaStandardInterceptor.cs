using Castle.DynamicProxy;
using System.IO;

namespace Laboratory.Castle
{
    public class TeaStandardInterceptor : StandardInterceptor
    {
        private readonly TextWriter _output;
        public TeaStandardInterceptor(TextWriter writer)
        {
            this._output = writer;
        }

        /// <summary>
        /// 方法调用前
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation)
        {
            this._output.WriteLine($"[Castle-Standard-调用前]{invocation.Method.Name}");
            base.PreProceed(invocation);
        }

        /// <summary>
        /// 方法返回时
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation)
        {
            base.PerformProceed(invocation);
            this._output.WriteLine($"[Castle-Standard-方法返回时]{invocation.Method.Name}");
        }

        /// <summary>
        /// 方法调用后
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            base.PostProceed(invocation);
            this._output.WriteLine($"[Castle-Standard-调用后]{invocation.Method.Name}");
        }
    }
}
