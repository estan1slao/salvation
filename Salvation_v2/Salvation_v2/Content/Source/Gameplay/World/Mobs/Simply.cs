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
    public class Simply : Mob
    {
        public Simply(Vector2 pos, int ownerID) : base("2D\\simplyNoAnim", pos, new Vector2(88, 104), ownerID)
        {
            speed = 5f;
            hitDist = 60f;
            visibilityRadius = 500;
        }

        public override void Update(Vector2 offset, Player enemy)
        {
            base.Update(offset, enemy);
        }
        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
