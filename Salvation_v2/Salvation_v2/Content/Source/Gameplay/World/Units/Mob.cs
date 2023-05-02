#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using SharpDX.MediaFoundation;
#endregion

namespace Salvation_v2
{
    public class Mob : Unit
    {
        public Mob(string path, Vector2 pos, Vector2 size, int ownerID) : base(path, pos, size, ownerID)
        {
        }
        public override void Update(Vector2 offset, Player enemy)
        {
            GameGlobals.PassMob(new Simply(new Vector2(Globals.screenWidth / 2 - 44, Globals.screenHeight / 2 - 54), 1));
            GameGlobals.PassMob(new Simply(new Vector2(Globals.screenWidth - 88, Globals.screenHeight - 104), 2));
            AI(enemy);
            pos -= offset; 
            base.Update(offset, enemy);
        }

        public virtual void AI(Player enemy)
        {
            if (Globals.GetDistance(pos, enemy.hero.pos) < visibilityRadius)
                pos.X += Globals.RadialMovement(enemy.hero.pos, pos, speed).X;

            if (Globals.GetDistance(pos, enemy.hero.pos) < hitDist)
            {
                enemy.hero.GetHit(1);
                //dead = true;
            }
        }
        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
