using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RS.Bots.ClickBot;
using RS.Bots.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RS.Bots.App
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<App, App>();
            services.AddSingleton<IBot, Bot>();
        }

        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            var builder = new ConfigurationBuilder();

            ConfigureServices(services);

            var configuration = builder.Build();
            var provider = services.BuildServiceProvider();

            var ctSource = new CancellationTokenSource();
            var ct = ctSource.Token;

            var task = Task.Run(async () =>
            {
                App program = provider.GetRequiredService<App>();
                await program.Run(ct);
            });
            try
            {
                task.Wait();
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }

            ctSource.Cancel();
            ctSource.Dispose();
        }
    }
}