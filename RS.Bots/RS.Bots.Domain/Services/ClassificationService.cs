using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using RS.Bots.Domain.Entities;
using RS.Bots.Domain.Interfaces;
using System;
using System.Drawing;
using System.Linq;

namespace RS.Bots.Domain.Services
{
    public class ClassificationService : IClassificationService
    {
        public MatchedModelResult MatchModelInImage(string pathToImage, string pathToModel)
        {
            var image = CvInvoke.Imread(pathToImage, ImreadModes.Grayscale); 
            var model = CvInvoke.Imread(pathToModel, ImreadModes.Grayscale);

            return MatchModelInImage(image.ToImage<Gray, Byte>(), model.ToImage<Gray, Byte>());
        }

        private MatchedModelResult MatchModelInImage(Image<Gray, Byte> areaImage, Image<Gray, Byte> modelImage)
        {
            //Work out padding array size
            var dftSize = new Point(areaImage.Width + (modelImage.Width * 2), areaImage.Height + (modelImage.Height * 2));

            //Pad the Array with zeros
            using (var pad_array = new Image<Gray, Byte>(dftSize.X, dftSize.Y))
            {
                //copy centre
                pad_array.ROI = new Rectangle(modelImage.Width, modelImage.Height, areaImage.Width, areaImage.Height);
                CvInvoke.cvCopy(areaImage, pad_array, IntPtr.Zero);

                pad_array.ROI = (new Rectangle(0, 0, dftSize.X, dftSize.Y));

                //Match Template
                using (var result_Matrix = pad_array.MatchTemplate(modelImage, TemplateMatchingType.CcoeffNormed))
                {
                    Point[] maxLocation, MinLocation;
                    double[] min, max;

                    //Limit ROI to look for Match
                    result_Matrix.ROI = new Rectangle(modelImage.Width, modelImage.Height, areaImage.Width - modelImage.Width, areaImage.Height - modelImage.Height);
                    result_Matrix.MinMax(out min, out max, out MinLocation, out maxLocation);

                    //var results = result_Matrix.Convert<Gray, Double>();
                    //Area_Image.Draw(new Rectangle(midPoint, new Size(15, 15)), new Gray(), 2);
                    //Area_Image.Save($"{Guid.NewGuid().ToString().Replace('-', '.')}.jpg");

                    if((MinLocation.Last().X == 0.0 && maxLocation.Last().Y == 0.0))
                    {
                        return null;
                    }

                    //Middle of the area
                    return new MatchedModelResult 
                    {
                        PositionMidX = maxLocation[0].X + (modelImage.Width / 2),
                        PositionMidY = maxLocation[0].Y + (modelImage.Height / 2)
                    };                  
                }
            }
        }
    }
}
