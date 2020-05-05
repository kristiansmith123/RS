using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using RS.Bots.Domain.Entities;
using RS.Bots.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async Task<IEnumerable<VisionResult>> AnalyseImageAsync(string pathToImage, string keywords = null)
        {
            var keywordMatchResults = new List<DetectedObject>();

            if (!File.Exists(pathToImage))
            {
                Console.WriteLine($"Image {pathToImage} does not exist, skipping");
                return null;
            }
            else
            {
                using (var stream = File.OpenRead(pathToImage))
                {
                    var client = GetClient();
                    var results = await client.DetectObjectsInStreamAsync(stream);

                    if(keywords != null)
                    {
                        keywordMatchResults = results.Objects.Where(o => keywords.ToLower().Contains(o.ObjectProperty.ToLower())).ToList();
                    }
                    else
                    {
                        keywordMatchResults = results.Objects.ToList();
                    }

                    return keywordMatchResults.Select(r => new VisionResult
                    {
                        Type = r.ObjectProperty.ToLower(),
                        Confidence = r.Confidence,
                        PositionX = r.Rectangle.X,
                        PositionY = r.Rectangle.Y,
                        Height = r.Rectangle.H,
                        Width = r.Rectangle.W
                    });
                }
            }
        }
    }
}
