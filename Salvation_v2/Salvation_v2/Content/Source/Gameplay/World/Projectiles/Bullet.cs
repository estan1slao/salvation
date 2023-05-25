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
    public class Bullet : Projectile2D
    {
        public Bullet(Vector2 pos, Unit owner, Vector2 target) : base("2D\\bullet", pos + owner.size / 2, new Vector2(14, 25), owner, target)
        {
        }

        public override void Update(Vector2 offset, List<AttackableObject> units, List<Basic2D> nonCollidingObjects)
        {
            base.Update(offset, units, nonCollidingObjects);
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
