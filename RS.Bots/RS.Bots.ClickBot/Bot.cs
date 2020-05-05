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
            Console.WriteLine($"{_botName} started");
            Console.WriteLine("Sleeping 5 seconds...");
            
            await Task.Delay(3000);
            Screenshot.New();

            Console.WriteLine($"Screenshot cpatured");

            var dir = new DirectoryInfo($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\RS");
            var results = new List<VisionResult>();

            foreach (var file in dir.GetFiles("*.jpg"))
            {
                Console.WriteLine($"Analysing screenshot for broccoli...");

                var broccolis = await _visionService.AnalyseImageAsync(file.FullName, "broccoli");
                results.AddRange(broccolis);

                foreach (var broc in broccolis)
                {
                    Console.WriteLine($"\t Broccoli found with mid point {broc.PositionMidX}, {broc.PositionMidY}");
                    Console.WriteLine("Clicking");

                    await Mouse.MoveToPositionAndClickAsync(x: (int) broc.PositionMidX, y: (int) broc.PositionMidY, cursorDelay: 3, cursorSteps: 100);
                    Console.WriteLine("Sleeping 3 seconds");
                }

                file.Delete();
            }

            //RS game boundaries are:
            //  top left 10, 30
            //  bottom left 10, 650
            //  bottom right 990, 650
            //  top right 990, 30
            //check this in screenshot!!!!!!
            
            
            //await Mouse.MoveToPositionAndClickAsync(x: 155, y: 309, cursorDelay: 3, cursorSteps: 100);
            //Console.WriteLine("Sleeping 3s");
            //await Task.Delay(3000);
            //await Mouse.MoveToPositionAndClickAsync(x: 541, y: 252, cursorDelay: 3, cursorSteps: 100);
            //Console.WriteLine("Sleeping 3s");
            //await Task.Delay(3000);
            //await Mouse.MoveToPositionAndClickAsync(x: 752, y: 604, cursorDelay: 3, cursorSteps: 100);
            
            
        }

        public void Stop()
        {
        }
    }
}
