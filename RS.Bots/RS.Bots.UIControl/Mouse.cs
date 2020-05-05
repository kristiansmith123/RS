using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RS.Bots.UIControl
{
    public static class Mouse
    {
        public static async Task MoveToPositionAndClickAsync(int x, int y, int cursorSteps = 100, int cursorDelay = 2, bool click = true)
        {
            var startPosition = Cursor.Position;
            var currentPosition = startPosition;

            //Get line to destination
            var line = new Point(x - startPosition.X, y - startPosition.Y);

            //Divide by steps
            line.X = line.X / cursorSteps;
            line.Y = line.Y / cursorSteps;

            //Move the mouse to each point on line to destination
            for (int i = 0; i < cursorSteps; i++)
            {
                currentPosition = new Point(currentPosition.X + line.X, currentPosition.Y + line.Y);
                Move(currentPosition.X, currentPosition.Y);

                //Add delay to every 5th step
                if (i % 5 == 0)
                {
                    await Task.Delay(cursorDelay);
                }
            }

            //Move to destination
            Move(x, y);

            if (click)
            {
                await Task.Delay(cursorDelay);
                Click();
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private static void Move(int x, int y)
        {
            Cursor.Position = new Point(x, y);
        }

        private static void Click()
        {
            var X = (uint) Cursor.Position.X;
            var Y = (uint) Cursor.Position.Y;

            mouse_event((int) MouseEventFlags.LeftDown | (int) MouseEventFlags.LeftUp, X, Y, 0, 0);
        }
    }
}
