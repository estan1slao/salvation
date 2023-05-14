using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvation_v2
{
    public class Door : LevelSwitch
    {
        public Door(Vector2 pos, int nextLevel) : base("2D\\Door", pos, new Vector2(120, 192), nextLevel)
        {
        }

        public override void Update(Vector2 offset, List<Basic2D> nonCollidingObjects, List<Door> doors)
        {
            base.Update(offset, nonCollidingObjects, doors);
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
