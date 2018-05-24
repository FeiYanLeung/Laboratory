using Castle.DynamicProxy;
using System;
using System.IO;

namespace Laboratory.Castle
{
    public class TeaInterceptor : IInterceptor
    {
        private readonly TextWriter _output;
        public TeaInterceptor()
        {
            this._output = Console.Out;
        }
        public TeaInterceptor(TextWriter writer)
        {
            this._output = writer;
        }
        public void Intercept(IInvocation invocation)
        {
            try
            {
                this._output.WriteLine($"[Castle-调用前]{invocation.Method.Name}");
                invocation.Proceed();
                this._output.WriteLine($"[Castle-调用后]{invocation.Method.Name}");
            }
            catch
            {
                this._output.WriteLine($"[Castle-执行时]{invocation.Method.Name}");
            }
            finally
            {
                this._output.WriteLine("[Castle-Finally]");
            }
        }
    }
}
