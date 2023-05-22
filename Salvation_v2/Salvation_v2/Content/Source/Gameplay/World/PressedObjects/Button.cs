using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Salvation_v2
{
    public class Button : BasicButton
    {
        public Button(Vector2 pos, int mSecond) : base("2D\\button", pos, new Vector2(50,50), mSecond)
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
