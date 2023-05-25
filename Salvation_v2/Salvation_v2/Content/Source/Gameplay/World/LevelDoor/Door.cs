using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Salvation_v2
{
    public class Door : LevelSwitch
    {
        public bool IsActive { get; set; }
        public Door(Vector2 pos, int nextLevel) : base("2D\\Doors\\Door", pos, new Vector2(120, 192), new Vector2(2, 1), nextLevel)
        {
            IsActive = true;
            frameAnimations = true;
            currentAnimation = 0;
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 1, 50, 0, "Close"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(1, 0), 1, 50, 0, "Open"));
        }

        public Door(Vector2 pos, int nextLevel, bool isActive) : base("2D\\Doors\\Door", pos, new Vector2(120, 192), new Vector2(2, 1), nextLevel)
        {
            IsActive = isActive;
            frameAnimations = true;
            currentAnimation = 0;
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 1, 50, 0, "Close"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(1, 0), 1, 50, 0, "Open"));
        }

        public override void Update(Vector2 offset, List<Basic2D> nonCollidingObjects, List<Door> doors)
        {
            if (IsActive)
                base.Update(offset, nonCollidingObjects, doors);
        }

        public override void Draw(Vector2 offset)
        {
            if (IsActive)
                SetAnimationByName("Open");
            else
                SetAnimationByName("Close");
            base.Draw(offset);
        }
    }
}
