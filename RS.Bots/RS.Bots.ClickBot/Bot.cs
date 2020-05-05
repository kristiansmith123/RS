using RS.Bots.Domain.Interfaces;
using RS.Bots.UIControl;
using System;
using System.Threading.Tasks;

namespace RS.Bots.ClickBot
{
    public class Bot : IBot
    {
        private string _botName = "Click Bot";

        public string GetName()
        {
            return _botName;
        }

        public async Task StartAsync()
        {
            Console.WriteLine($"{_botName} started");

            await Mouse.MoveToPositionAsync(x: 0, y: 0, cursorDelay: 1, cursorSteps: 100);
            await Mouse.MoveToPositionAsync(x: 100, y: 100, cursorDelay: 5, cursorSteps: 100);
            await Mouse.MoveToPositionAsync(x: 1000, y: 700, cursorDelay: 1, cursorSteps: 50);
            await Mouse.MoveToPositionAsync(x: 200, y: 200, cursorDelay: 2, cursorSteps: 100);
            await Mouse.MoveToPositionAsync(x: 1000, y: 100, cursorDelay: 2, cursorSteps: 100);
            await Mouse.MoveToPositionAsync(x: 150, y: 800, cursorDelay: 2, cursorSteps: 50);
            await Mouse.MoveToPositionAsync(x: 200, y: 900, cursorDelay: 1, cursorSteps: 200);
            await Mouse.MoveToPositionAsync(x: 10, y: 200, cursorDelay: 1, cursorSteps: 100);
            await Mouse.MoveToPositionAsync(x: 1200, y: 1000, cursorDelay: 2, cursorSteps: 100);

        }

        public void Stop()
        {
        }
    }
}
