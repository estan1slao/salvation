using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvation_v2
{
    public class LevelSwitch : Animated2d
    {
        public int NextLevel;
        public LevelSwitch(string path, Vector2 pos, Vector2 size,Vector2 frames, int nextLevel) : base(path, pos, size, frames, Color.White)
        {
            NextLevel = nextLevel;
        }

        public override void Update(Vector2 offset, List<Basic2D> nonCollidingObjects, List<Door> doors)
        {
            GameGlobals.levelNumber = NextLevel;
            base.Update(offset, nonCollidingObjects, doors);
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
