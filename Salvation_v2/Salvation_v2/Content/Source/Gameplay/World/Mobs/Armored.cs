#region Includes
using Microsoft.Xna.Framework;
using System.Collections.Generic;
#endregion

namespace Salvation_v2
{
    public class Armored : Mob
    {
        public Armored(Vector2 pos, Vector2 frames, int ownerID, TypeOfDeath diesIn) : base("2D\\Units\\Armored", pos, new Vector2(170, 194), frames, ownerID, diesIn)
        {
            health = 20;
            healthMax = health;
            speed = 3f;
            hitDist = 100f;
            visibilityRadius = 800;
            frameAnimations = true;
            currentAnimation = 0;
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 1), 6, 133, 0, "WalkRight"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 3), 6, 133, 0, "WalkLeft"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(3, 2), 3, 133, 0, "StandLeft"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 3, 133, 0, "StandRight"));
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
