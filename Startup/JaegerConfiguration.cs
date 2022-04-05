using Jaeger;
using Jaeger.Senders;
using Jaeger.Senders.Thrift;
using OpenTracing;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing.Util;
using static Jaeger.Configuration;

namespace MasstransitRabbitMq
{
    public static class JaegerConfiguration
    {
        public static void AddJaeger(this IServiceCollection services)
        {
            services.AddOpenTracing();
            services.AddSingleton<ITracer>(serviceProvider =>
            {
                var serviceName = "jaeger-weather";
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                SenderConfiguration.DefaultSenderResolver = new SenderResolver(loggerFactory).RegisterSenderFactory<ThriftSenderFactory>();
                var tracer = new Tracer.Builder(serviceName).WithLoggerFactory(loggerFactory).Build();

                // Allows code that can't use DI to also access the tracer.
                if (!GlobalTracer.IsRegistered())
                {
                    GlobalTracer.Register(tracer);
                }

                return tracer;
            });

            // skipping the health check, metrics and documentation end points.
            services.Configure<AspNetCoreDiagnosticOptions>(options =>
            {
                options.Hosting.IgnorePatterns.Add(context => context.Request.Path.Value.StartsWith("/status"));
                options.Hosting.IgnorePatterns.Add(context => context.Request.Path.Value.StartsWith("/metrics"));
                options.Hosting.IgnorePatterns.Add(context => context.Request.Path.Value.StartsWith("/swagger"));
            });
        }
    }
}