#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion


namespace Salvation_v2
{
    public delegate void PassObject(object i);
    public delegate object PassObjectAndReturn(object i);
    public class Globals
    {
        public static int screenHeight, screenWidth;

        public static ContentManager Content;
        public static SpriteBatch SpriteBatch;

        public static NKeyboard Keyboard;
        public static NMouseControl Mouse;

        public static GameTime GameTime;

        public static float Gravitation;

        public static System.Globalization.CultureInfo cultureRU = new System.Globalization.CultureInfo("ru-RU");
        public static System.Globalization.CultureInfo cultureUS = new System.Globalization.CultureInfo("en-US");

        public static float GetDistance(Vector2 pos, Vector2 target) 
            => (float)Math.Sqrt((pos.X - target.X) * (pos.X - target.X) + (pos.Y - target.Y) * (pos.Y - target.Y));

        public static bool IsInside(Vector2 pos, Rectangle rect) =>
                pos.X >= rect.Location.X && pos.X <= rect.Location.X + rect.Width &&
                pos.Y >= rect.Location.Y && pos.Y <= rect.Location.Y + rect.Height;

        public static bool IsInside(Rectangle rect1, Rectangle rect2) => rect1.Intersects(rect2);

        public static Vector2 RadialMovement(Vector2 focus, Vector2 pos, float speed)
        {
            var dist = GetDistance(pos, focus);
            if (dist <= speed) { return focus - pos; }
            else { return (focus - pos) * speed/dist; }
        }

        public static float RotateTowards(Vector2 Pos, Vector2 focus)
        {
            float h, sineTheta, angle;
            if (Pos.Y - focus.Y != 0)
            {
                h = (float)Math.Sqrt(Math.Pow(Pos.X - focus.X, 2) + Math.Pow(Pos.Y - focus.Y, 2));
                sineTheta = (float)(Math.Abs(Pos.Y - focus.Y) / h);
            }
            else
            {
                h = Pos.X - focus.X;
                sineTheta = 0;
            }

            angle = (float)Math.Asin(sineTheta);

            //Quadrant 2
            if (Pos.X - focus.X > 0 && Pos.Y - focus.Y > 0)
                angle = (float)(Math.PI * 3 / 2 + angle);
            //Quadrant 3
            else if (Pos.X - focus.X > 0 && Pos.Y - focus.Y < 0)
                angle = (float)(Math.PI * 3 / 2 - angle);
            //Quadrant 1
            else if (Pos.X - focus.X < 0 && Pos.Y - focus.Y > 0)
                angle = (float)(Math.PI / 2 - angle);
            else if (Pos.X - focus.X < 0 && Pos.Y - focus.Y < 0)
                angle = (float)(Math.PI / 2 + angle);
            else if (Pos.X - focus.X > 0 && Pos.Y - focus.Y == 0)
                angle = (float)Math.PI * 3 / 2;
            else if (Pos.X - focus.X < 0 && Pos.Y - focus.Y == 0)
                angle = (float)Math.PI / 2;
            else if (Pos.X - focus.X == 0 && Pos.Y - focus.Y > 0)
                angle = (float)0;
            else if (Pos.X - focus.X == 0 && Pos.Y - focus.Y < 0)
                angle = (float)Math.PI;
            return angle;
        }
    }
}
