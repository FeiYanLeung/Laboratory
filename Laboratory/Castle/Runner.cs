using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using System;
using System.Reflection;

namespace Laboratory.Castle
{
    public class Runner : IRunner
    {
        public string Name => "Castle";

        public void Run()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            /**
             * 关闭配置：EnableClassInterceptors|EnableInterfaceInterceptors详见
             * <see cref="http://docs.autofac.org/en/latest/advanced/interceptors.html"/>
             */
            builder.RegisterType<GreenTea>()
                .As<ITea>()
                .Named<ITea>("green_tea")
                .EnableInterfaceInterceptors();

            builder.RegisterType<RedTea>()
                .As<ITea>()
                .Named<ITea>("red_tea")
                .EnableInterfaceInterceptors();

            //接口实现拦截
            builder.RegisterType<TeaInterceptor>()
                .As<IInterceptor>()
                .WithParameter("writer", Console.Out)
                .Named<IInterceptor>("tea-interceptor");

            //继承实现拦截
            builder.Register(c => new TeaStandardInterceptor(Console.Out))
                .As<IInterceptor>()
                .Named<IInterceptor>("tea-standard-interceptor");

            using (var container = builder.Build())
            {
                var green_tea = container.ResolveNamed<ITea>("green_tea");
                green_tea.Make();

                var red_tea = container.ResolveNamed<ITea>("red_tea");
                red_tea.Make();
            }
        }
    }
}
