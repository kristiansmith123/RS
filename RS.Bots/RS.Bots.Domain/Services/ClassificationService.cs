using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using RS.Bots.Domain.Entities;
using RS.Bots.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace RS.Bots.Domain.Services
{
    public class ClassificationService : IClassificationService
    {
        public Task<IEnumerable<VisionResult>> AnalyseImageAsync(string pathToImage, string keywords = null)
        {
            throw new NotImplementedException();
        }

        public bool Detect(string pathToImage, string pathToModel)
        {
            var image = CvInvoke.Imread(pathToImage, ImreadModes.Grayscale); 
            var model = CvInvoke.Imread(pathToModel, ImreadModes.Grayscale);

            return Detect_object(image.ToImage<Gray, Byte>(), model.ToImage<Gray, Byte>());
        }

        private bool Detect_object(Image<Gray, Byte> Area_Image, Image<Gray, Byte> image_object)
        {
            bool success = false;

            //Work out padding array size
            Point dftSize = new Point(Area_Image.Width + (image_object.Width * 2), Area_Image.Height + (image_object.Height * 2));
            //Pad the Array with zeros
            using (Image<Gray, Byte> pad_array = new Image<Gray, Byte>(dftSize.X, dftSize.Y))
            {
                //copy centre
                pad_array.ROI = new Rectangle(image_object.Width, image_object.Height, Area_Image.Width, Area_Image.Height);
                CvInvoke.cvCopy(Area_Image, pad_array, IntPtr.Zero);

                pad_array.ROI = (new Rectangle(0, 0, dftSize.X, dftSize.Y));

                //Match Template
                using (Image<Gray, float> result_Matrix = pad_array.MatchTemplate(image_object, TemplateMatchingType.CcoeffNormed))
                {
                    Point[] MAX_Loc, Min_Loc;
                    double[] min, max;
                    //Limit ROI to look for Match

                    result_Matrix.ROI = new Rectangle(image_object.Width, image_object.Height, Area_Image.Width - image_object.Width, Area_Image.Height - image_object.Height);

                    result_Matrix.MinMax(out min, out max, out Min_Loc, out MAX_Loc);

                    var location = new Point((MAX_Loc[0].X), (MAX_Loc[0].Y));
                    success = true;
                    var results = result_Matrix.Convert<Gray, Double>();


                    Area_Image.Draw(new Rectangle(location, new Size(MAX_Loc.Last().X, MAX_Loc.Last().Y)), new Gray(), 2);
                    //Display Results
                    Area_Image.Save($"{Guid.NewGuid().ToString().Replace('-', '.')}.jpg");

                }
            }
            return success;
        }
    }
}
