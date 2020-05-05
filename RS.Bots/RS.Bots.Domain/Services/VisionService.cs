using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using RS.Bots.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.Bots.Domain.Services
{
    public class VisionService : IVisionService
    {
        private readonly string _subscriptionKey = "ba31d7be54bc430996ce01f09eaf2c1d";
        private readonly string _endpoint = "https://rsbotvision.cognitiveservices.azure.com/";

        private ComputerVisionClient GetClient()
        {
            return new ComputerVisionClient(new ApiKeyServiceClientCredentials(_subscriptionKey))
            {
                Endpoint = _endpoint
            };
        }

        public async Task AnalyseImageAsync(string pathToImage)
        {
            if (!File.Exists(pathToImage))
            {
                Console.WriteLine($"Image {pathToImage} does not exist, skipping");
            }
            else
            {

                using (var stream = File.OpenRead(pathToImage))
                {
                    var client = GetClient();
                    var results = await client.DetectObjectsInStreamAsync(stream);

                    Console.WriteLine($"Detected objects in {Path.GetFileName(pathToImage)}:");

                    // For each detected object in the picture, print out the bounding object detected, confidence of that detection and bounding box within the image
                    foreach (var obj in results.Objects)
                    {
                        Console.WriteLine($"\t {obj.ObjectProperty} with confidence {obj.Confidence} at location {obj.Rectangle.X}, " +
                          $"{obj.Rectangle.X + obj.Rectangle.W}, {obj.Rectangle.Y}, {obj.Rectangle.Y + obj.Rectangle.H}");
                    }
                }
            }
        }
    }
}
