using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RS.Bots.UIControl
{
    public static class Mouse
    {
        private static void Move(int x, int y)
        {
            Cursor.Position = new Point(x, y);
        }

        public static async Task MoveToPositionAsync(int x, int y, int cursorSteps = 100, int cursorDelay = 2)
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
        }




    }
}
