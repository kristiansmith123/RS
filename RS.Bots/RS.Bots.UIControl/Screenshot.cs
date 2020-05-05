using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace RS.Bots.UIControl
{
    public static class Screenshot
    {
        public static void New()
        {
            var captureRectangle = Screen.AllScreens[0].Bounds;
            var captureBitmap = new Bitmap(captureRectangle.Size.Width, captureRectangle.Size.Height, PixelFormat.Format32bppArgb);
            var captureGraphics = Graphics.FromImage(captureBitmap);

            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
            captureBitmap.Save($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Screenshot_{Guid.NewGuid().ToString()}.jpg", ImageFormat.Jpeg);
        }
    }
}
