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
            //var dir = new DirectoryInfo(@"C:\Users\kristian.smith\Pictures\RS");
            //var results = new List<VisionResult>();

            //Console.WriteLine($"{_botName} started");
            //Console.WriteLine($"Analysing images...");

            //foreach (var file in dir.GetFiles("*.PNG"))
            //{
            //    var broccolis = await _visionService.AnalyseImageAsync(file.FullName, "broccoli");
            //    results.AddRange(broccolis);

            //    foreach (var broc in broccolis)
            //    {
            //        Console.WriteLine($"\t {broc.Type} with confidence {broc.Confidence} at location {broc.PositionX}, " +
            //          $"{broc.PositionX + broc.Width}, {broc.PositionY}, {broc.PositionY + broc.Height}. Height {broc.Height} Width {broc.Width}");
            //    }
            //}

            //RS game boundaries are:
            //  top left 10, 30
            //  bottom left 10, 650
            //  bottom right 990, 650
            //  top right 990, 30

            Console.WriteLine("Sleeping 5s");
            await Task.Delay(3000);
            
            await Mouse.MoveToPositionAndClickAsync(x: 155, y: 309, cursorDelay: 3, cursorSteps: 100);
            Console.WriteLine("Sleeping 3s");
            await Task.Delay(3000);
            await Mouse.MoveToPositionAndClickAsync(x: 541, y: 252, cursorDelay: 3, cursorSteps: 100);
            Console.WriteLine("Sleeping 3s");
            await Task.Delay(3000);
            await Mouse.MoveToPositionAndClickAsync(x: 752, y: 604, cursorDelay: 3, cursorSteps: 100);
            
            
            //Screenshot.New();
        }

        public void Stop()
        {
        }
    }
}
