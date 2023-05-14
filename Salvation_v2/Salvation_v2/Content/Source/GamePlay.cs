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
    public class GamePlay
    {
        int playState;
        World world;
        int levelNumber;
        bool[] IsResets = new bool[6]; 
        public GamePlay(int levelNumber)
        {
            playState = 0;
            this.levelNumber = levelNumber;
            ResetWorld(null);
        }

        public virtual void Update(int levelNumber)
        {
            this.levelNumber = levelNumber;
            if (levelNumber == 1)
            {
                if (!IsResets[1])
                {
                    ResetWorld(null);
                    IsResets[1] = true;
                }
                world.Update();
            }
            if (levelNumber == 2)
            {
                if (!IsResets[2])
                {
                    ResetWorld(null);
                    IsResets[2] = true;
                }
                world.Update();
            }
        }

        public virtual void ResetWorld(object info)
        {
            world = new World(ResetWorld, levelNumber);
        }

        public virtual void Draw()
        {
            if (playState == 0)
            {
                world.Draw(Vector2.Zero);
            }
        }
    }
}
