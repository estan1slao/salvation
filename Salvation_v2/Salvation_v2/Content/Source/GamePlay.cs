#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
        World world;
        int levelNumber;
        bool[] IsResets; 
        public GamePlay(int levelNumber)
        {
            this.levelNumber = levelNumber;
            IsResets = new bool[new DirectoryInfo("XML\\Levels").GetFiles().Length + 2];
            ResetWorld(null);
        }

        public virtual void Update(int levelNumber)
        {
            this.levelNumber = levelNumber;
            LoadLevel(levelNumber);
        }

        private void LoadLevel(int levelNumber)
        {
            if (!IsResets[levelNumber])
            {
                ResetWorld(null);
                IsResets[levelNumber] = true;
            }
            world.Update();
        }

        public virtual void ResetWorld(object info)
        {
            world = new World(ResetWorld, levelNumber);
        }

        public virtual void Draw()
        {
            world.Draw(Vector2.Zero);
        }
    }
}
