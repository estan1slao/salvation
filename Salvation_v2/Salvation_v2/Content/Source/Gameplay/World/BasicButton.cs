using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Salvation_v2
{
    public class BasicButton : Basic2D
    {
        public NTimer ActiveTime;
        public bool isPressed;
        public BasicButton(string path, Vector2 pos, Vector2 size, int mSecond) : base(path, pos, size)
        {
            ActiveTime = new NTimer(mSecond,true);
            isPressed = false;
        }

        public override void Update(Vector2 offset, List<Basic2D> nonCollidingObjects, List<Door> doors)
        {
            foreach (var door in doors)
            {
                if (!ActiveTime.Test())
                {
                    door.IsActive = true;
                    isPressed = true;
                }
                else
                {
                    door.IsActive = false;
                    isPressed = false;
                }  
            }
            base.Update(offset, nonCollidingObjects, doors);
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
