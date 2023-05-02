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
using SharpDX.DirectWrite;
#endregion

namespace Salvation_v2
{
    public class Unit : AttackableObject
    {
        public Unit(string path, Vector2 pos, Vector2 size , int ownerID) : base(path, pos, size, ownerID)
        {
        }
        public override void Update(Vector2 offset, Player enemy)
        {
            if (pos.Y < Globals.screenHeight - size.Y)
                pos += new Vector2(0, Globals.Gravitation);
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
