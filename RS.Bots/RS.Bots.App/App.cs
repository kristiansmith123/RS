using RS.Bots.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RS.Bots.App
{
    class App
    {
        private IBot _bot;

        public App(IBot bot)
        {
            _bot = bot;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if(_bot == null)
            {
                Console.WriteLine("Error: No bot configured! Press any key to exit");
                Console.ReadKey();
                Environment.Exit(0);
            }

            System.Console.WriteLine($"Configured to start {_bot.GetName()}...");
            await _bot.StartAsync();
            
            Console.ReadLine();
        }
    }
}
