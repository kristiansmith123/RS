using RS.Bots.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RS.Bots.App
{
    class App
    {
        private readonly IBot _bot;

        public App(IBot bot)
        {
            _bot = bot;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            IntPtr handle = GetConsoleWindow();
            SetWindowPos(handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);

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

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd,
                                    IntPtr hWndInsertAfter,
                                    int X,
                                    int Y,
                                    int cx,
                                    int cy,
                                    uint uFlags);
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        const uint SWP_NOSIZE = 0x0001, SWP_NOMOVE = 0x0002, SWP_SHOWWINDOW = 0x0040;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
    }
}
