using RS.Bots.Domain.Entities;
using RS.Bots.Domain.Interfaces;
using RS.Bots.UIControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RS.Bots.ClickBot
{
    public class Bot : IBot
    {
        private readonly string _botName = "Click Bot";
        private readonly IVisionService _visionService;

        public Bot(IVisionService visionService)
        {
            _visionService = visionService;
        }

        public string GetName()
        {
            return _botName;
        }

        public async Task StartAsync()
        {
            var dir = new DirectoryInfo(@"C:\Users\kristian.smith\Pictures\RS");
            var results = new List<VisionResult>();

            Console.WriteLine($"{_botName} started");
            Console.WriteLine($"Analysing images...");

            foreach (var file in dir.GetFiles("*.PNG"))
            {
                var broccolis = await _visionService.AnalyseImageAsync(file.FullName, "broccoli");
                results.AddRange(broccolis);

                foreach (var broc in broccolis)
                {
                    Console.WriteLine($"\t {broc.Type} with confidence {broc.Confidence} at location {broc.PositionX}, " +
                      $"{broc.PositionX + broc.Width}, {broc.PositionY}, {broc.PositionY + broc.Height}. Height {broc.Height} Width {broc.Width}");
                }
            }

            //await Mouse.MoveToPositionAsync(x: 0, y: 0, cursorDelay: 1, cursorSteps: 100);
            //await Mouse.MoveToPositionAsync(x: 100, y: 100, cursorDelay: 5, cursorSteps: 100);
            //await Mouse.MoveToPositionAsync(x: 1000, y: 700, cursorDelay: 1, cursorSteps: 50);
            //await Mouse.MoveToPositionAsync(x: 200, y: 200, cursorDelay: 2, cursorSteps: 100);
            //await Mouse.MoveToPositionAsync(x: 1000, y: 100, cursorDelay: 2, cursorSteps: 100);
            //await Mouse.MoveToPositionAsync(x: 150, y: 800, cursorDelay: 2, cursorSteps: 50);
            //await Mouse.MoveToPositionAsync(x: 200, y: 900, cursorDelay: 1, cursorSteps: 200);
            //await Mouse.MoveToPositionAsync(x: 10, y: 200, cursorDelay: 1, cursorSteps: 100);
            //await Mouse.MoveToPositionAsync(x: 1200, y: 1000, cursorDelay: 2, cursorSteps: 100);

        }

        public void Stop()
        {
        }
    }
}
