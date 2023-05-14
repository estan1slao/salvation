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
    public class Speedy : Mob
    {
        public Speedy(Vector2 pos, Vector2 frames, int ownerID, TypeOfDeath diesIn) : base("2D\\Units\\Speedy", pos, new Vector2(99, 117),frames, ownerID, diesIn)
        {
            health = 3;
            healthMax = health;
            speed = 10f;
            hitDist = 60f;
            visibilityRadius = 600;
            frameAnimations = true;
            currentAnimation = 0;
            diesIn = TypeOfDeath.OnlyParallel;
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 1), 8, 133, 0, "WalkRight"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 4), 8, 133, 0, "WalkLeft"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 3), 8, 133, 0, "StandLeft"));
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
