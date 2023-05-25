#region Includes
using Microsoft.Xna.Framework;
using System.Collections.Generic;
#endregion

namespace Salvation_v2
{
    public class Simply : Mob
    {
        public Simply(Vector2 pos, Vector2 frames, int ownerID, TypeOfDeath diesIn) : base("2D\\Units\\Simply", pos, new Vector2(88, 104), frames, ownerID, diesIn)
        {
            speed = 5f;
            hitDist = 60f;
            visibilityRadius = 500;
            frameAnimations = true;
            currentAnimation = 0;
            diesIn = TypeOfDeath.OnlyReal;
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 2), 8, 133, 0, "WalkRight"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 6), 8, 133, 0, "WalkLeft"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 4), 8, 133, 0, "StandLeft"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 8, 133, 0, "StandRight"));
        }

        public override void Update(Vector2 offset, Player enemy, List<Basic2D> nonCollidingObjects, List<Door> doors)
        {
            base.Update(offset, enemy, nonCollidingObjects, doors);
        }
        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
