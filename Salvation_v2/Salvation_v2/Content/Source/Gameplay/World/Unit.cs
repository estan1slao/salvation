#region Includes
using Microsoft.Xna.Framework;
using System.Collections.Generic;
#endregion

namespace Salvation_v2
{
    public class Unit : AttackableObject
    {
        public Unit(string path, Vector2 pos, Vector2 size, Vector2 frames, int ownerID, TypeOfDeath diesIn) : base(path, pos, size,frames, ownerID, diesIn)
        {
        }
        public override void Update(Vector2 offset, Player enemy, List<Basic2D> nonCollidingObjects, List<Door> doors)
        {
            var nextPos = new Vector2(pos.X, pos.Y);
            if (pos.Y < Globals.screenHeight - size.Y)
            {
                nextPos += new Vector2(0, Globals.Gravitation);
                MoveObjectBottom(nonCollidingObjects, nextPos);
            }
            base.Update(offset, enemy, nonCollidingObjects, doors);
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
