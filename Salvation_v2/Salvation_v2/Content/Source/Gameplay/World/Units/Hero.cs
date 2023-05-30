#region Includes
using Microsoft.Xna.Framework;
using System.Collections.Generic;
#endregion

namespace Salvation_v2
{
    public class Hero : Unit
    {
        private int spaceCounter = 0;
        private int deltaSpace = 0;
        private float speedSpace = 35f;
        private bool spaceNow = false;
        NTimer timer = new NTimer(100);

        public Hero(string path, Vector2 pos, Vector2 size, Vector2 frames, int ownerID) : base(path, pos, size, frames, ownerID, TypeOfDeath.Everywhere)
        {
            speed = 7f;
            health = 50;
            healthMax = 50;
            HitBox = new Rectangle((int)pos.X, (int)pos.Y, (int)frameSize.X, (int)frameSize.Y);
            frameAnimations = true;
            currentAnimation = 0;
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 8, 50, 0, "WalkRight"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 1), 8, 50, 0, "WalkLeft"));
            frameAnimationList.Add(new FrameAnimation(new Vector2(frameSize.X, frameSize.Y), frames, new Vector2(0, 0), 1, 50, 0, "Stand"));
        }
        public override void Update(Vector2 offset, List<Basic2D> nonCollidingObjects, List<Door> doors)
        {
            if (timer.Timer >= 100)
                timer.Reset();
            timer.UpdateTimer();
            Vector2 nextPos = new Vector2(pos.X, pos.Y);
            pos -= offset;
            if (Globals.Keyboard.GetPress("A") && pos.X > 0)
            {
                SetAnimationByName("WalkLeft");
                nextPos = new Vector2(pos.X - speed, pos.Y);
                MoveObjectLeft(nonCollidingObjects, nextPos);
            }
            if (Globals.Keyboard.GetPress("D") && pos.X < Globals.screenWidth - frameSize.X)
            {
                SetAnimationByName("WalkRight");
                nextPos = new Vector2(pos.X + speed, pos.Y);
                MoveObjectRight(nonCollidingObjects, nextPos);
            }
            if (Globals.Keyboard.GetPress("Space") && timer.Test() && pos.Y > 0)
            {
                timer.Reset();
                spaceCounter++;
                if (spaceCounter == 1)
                    spaceNow = true;
                if (spaceCounter == 2)
                {
                    deltaSpace = 0;
                    spaceNow = true;
                }
            }
            if (Globals.Keyboard.pressedKeys.Count == 0)
                SetAnimationByName("Stand");
            if (spaceNow)
            {
                var oldPos = pos;
                nextPos.Y -= speedSpace;
                MoveObjectTop(nonCollidingObjects, nextPos);
                deltaSpace++;
                if (deltaSpace == 10 || oldPos == pos)
                {
                    deltaSpace = 0;
                    spaceNow = false;
                }
            }
            if (pos.Y + frameSize.Y >= Globals.screenHeight)
                spaceCounter = 0;
            if (Globals.Keyboard.GetPress("Q") && timer.Test())
            {
                timer.Reset();
                if (GameGlobals.isParallel)
                    GameGlobals.isParallel = false;
                else
                    GameGlobals.isParallel = true;
            }
            foreach (var obj in nonCollidingObjects)
            {
                if (obj == this) continue;
                if (IsTouchingTop(obj))
                {
                    spaceCounter = 0;
                    break;
                }
            }
            if (Globals.Mouse.LeftClick())
                GameGlobals.PassProjectile(new Bullet(new Vector2(pos.X, pos.Y), this, new Vector2(Globals.Mouse.newMousePos.X, Globals.Mouse.newMousePos.Y) - offset));

            foreach (var door in doors)
                if (Globals.Keyboard.GetPress("E") && door.IsActive && Globals.IsInside(HitBox, door.HitBox) )
                    door.Update(offset, nonCollidingObjects, doors);

            foreach (var button in GameGlobals.Buttons)
            {
                button.ActiveTime.UpdateTimer();
                button.Update(offset, nonCollidingObjects, doors);
                if (Globals.Keyboard.GetPress("E") && Globals.IsInside(HitBox, button.HitBox))
                    button.ActiveTime.Reset();
            }

            base.Update(offset, null, nonCollidingObjects, doors);
        }
        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
