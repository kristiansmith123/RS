using RS.Bots.Domain.Entities;
using RS.Bots.Domain.Interfaces;
using RS.Bots.UIControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RS.Bots.ClickBot
{
    public class Bot : IBot
    {
        private static bool _Active;
        private readonly string _botName = "Click Bot";
        private readonly IVisionService _visionService;
        private readonly IClassificationService _classificationService;
        private bool _stop;

        public Bot(IVisionService visionService, IClassificationService classificationService)
        {
            _visionService = visionService;
            _classificationService = classificationService;
        }

        public string GetName()
        {
            return _botName;
        }

        private async Task<MatchedModelResult> ExecuteAsync()
        {
            _Active = true;

            Screenshot.New();
            Console.WriteLine($"Screenshot captured");

            var dir = new DirectoryInfo($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\RS");
            var results = new List<VisionResult>();

            var imageToSearch = dir.GetFiles("*.jpg").FirstOrDefault();

            if (imageToSearch == null)
            {
                throw new FileNotFoundException($"No images to search");
            }

            var result = _classificationService.MatchModelInImage(imageToSearch.FullName, @"C:\Users\kristian.smith\Pictures\RS\model.PNG");
            //var result = await _visionService.AnalyseImageAsync(imageToSearch.FullName, "broccoli");

            if (result != null)
            //if (result != null && result.Any())
            {
                Console.WriteLine($"Match found at X:{result.PositionMidX}, Y:{result.PositionMidY}.");

                //await Mouse.MoveToPositionAndClickAsync(x: (int)matchedResult.LocationX, y: (int)matchedResult.LocationY, cursorDelay: 3, cursorSteps: 100);
                imageToSearch.Delete();

                _Active = false;

                return result;
                //return result.First();
            }

            imageToSearch.Delete();
            _Active = false;
            return null;
        }

        public async Task StartAsync()
        {
            Console.WriteLine($"{_botName} started");
            Console.WriteLine($"Waiting 5 seconds...");
            var rand = new Random();
            var lastResult = new MatchedModelResult();

            await Task.Delay(5000);

            while (true)
            {
                if (!_Active)
                {
                    var waitAfterTreeClick = rand.Next(5000, 10000);
                    var waitAfterRandomClick = rand.Next(2000, 3000);

                    Console.WriteLine("Executing");
                    var result = await ExecuteAsync();
                    
                    if (lastResult != null && result != null && lastResult.PositionMidX == result.PositionMidY && lastResult.PositionMidY == result.PositionMidY)
                    {
                        //Move randomly as we're getting the same result
                        Console.WriteLine($"Clicking randomly - same result found");
                        await Mouse.MoveToPositionAndClickAsync(x: rand.Next(30, 990), y: rand.Next(30, 650), cursorDelay: 3, cursorSteps: 100);
                        Console.WriteLine($"Waiting {waitAfterRandomClick}ms...");
                        await Task.Delay(waitAfterRandomClick);
                    }
                    else
                    {
                        if (result == null)
                        {
                            Console.WriteLine($"Clicking randomly - nothing found");
                            await Mouse.MoveToPositionAndClickAsync(x: rand.Next(30, 990), y: rand.Next(30, 650), cursorDelay: 3, cursorSteps: 100);
                            Console.WriteLine($"Waiting {waitAfterRandomClick}ms...");
                            await Task.Delay(waitAfterRandomClick);
                        }
                        else
                        {
                            Console.WriteLine($"Clicking at position");
                            await Mouse.MoveToPositionAndClickAsync(x: (int)result.PositionMidX, y: (int)result.PositionMidY, cursorDelay: 3, cursorSteps: 100);
                            Console.WriteLine($"Waiting {waitAfterTreeClick}ms...");
                            await Task.Delay(waitAfterTreeClick);
                        }
                    }
                    lastResult = result;
                }

                if (_stop == true)
                {
                    break;
                }
            }


            //foreach (var file in dir.GetFiles("*.jpg"))
            //{
            //    Console.WriteLine($"Analysing screenshot for broccoli...");

            //    var broccolis = await _visionService.AnalyseImageAsync(file.FullName, "broccoli");
            //    results.AddRange(broccolis);

            //    foreach (var broc in broccolis)
            //    {
            //        Console.WriteLine($"\t Broccoli found with mid point {broc.PositionMidX}, {broc.PositionMidY}");
            //        Console.WriteLine("Clicking");

            //        await Mouse.MoveToPositionAndClickAsync(x: (int)broc.PositionMidX, y: (int)broc.PositionMidY, cursorDelay: 3, cursorSteps: 100);
            //        Console.WriteLine("Sleeping 3 seconds");
            //    }

            //    file.Delete();
            //}

            ////RS game boundaries are:
            ////  top left 10, 30
            ////  bottom left 10, 650
            ////  bottom right 990, 650
            ////  top right 990, 30
            ////check this in screenshot!!!!!!


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
