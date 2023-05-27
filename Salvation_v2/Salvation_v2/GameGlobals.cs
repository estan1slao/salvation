#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Salvation_v2
{
    public class GameGlobals
    {
        public static int score = 0;
        public static PassObject PassProjectile, PassMob, CheckScroll;
        public static int levelNumber = 1;
        public static bool isParallel = false;
        public static List<Basic2D> parallelObject = new List<Basic2D>();
        public static List<Button> Buttons = new List<Button>();
        public static bool isEnd = false;
    }
}
