#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using SharpDX.MediaFoundation;
#endregion

namespace Salvation_v2
{
    public class Mob : Unit
    {
        public Mob(string path, Vector2 pos, Vector2 size, Vector2 frames, int ownerID, TypeOfDeath diesIn) : base(path, pos, size,frames, ownerID, diesIn)
        {
        }
        public override void Update(Vector2 offset, Player enemy, List<Basic2D> nonCollidingObjects, List<Door> doors)
        {
            AI(enemy, nonCollidingObjects);
            base.Update(offset, enemy, nonCollidingObjects, doors);
        }

        public virtual void AI(Player enemy, List<Basic2D> nonCollidingObjects)
        {
            var direction = Globals.RadialMovement(enemy.hero.pos, pos, speed);
            if (Globals.GetDistance(pos, enemy.hero.pos) < visibilityRadius)
            {
                var isInside = false;
                var box = new Rectangle(new Point((int)pos.X, (int)pos.Y), new Point((int)(enemy.hero.pos.X - pos.X), (int)(enemy.hero.pos.Y- pos.Y)));
                if (box.Width < 0 && box.Height < 0)
                    box = new Rectangle(new Point((int)enemy.hero.pos.X, (int)(enemy.hero.pos.Y)), new Point(-box.Width, -box.Height));
                else if (box.Width >= 0 && box.Height < 0)
                    box = new Rectangle(new Point((int)pos.X, (int)enemy.hero.pos.Y), new Point(box.Width, -box.Height));
                else if (box.Width < 0 && box.Height >= 0)
                    box = new Rectangle(new Point((int)enemy.hero.pos.X, (int)pos.Y), new Point(-box.Width, box.Height));
                for (int i = 0; i < nonCollidingObjects.Count; i++)
                {
                    if (nonCollidingObjects[i] is Hero) continue;
                    if (Globals.IsInside(box, nonCollidingObjects[i].HitBox))
                    {
                        isInside = true;
                        break;
                    }
                }
                var nextPos = new Vector2(pos.X, pos.Y);

                if (!isInside)
                {
                    if (direction.X > 0)
                    {
                        nextPos.X += direction.X;
                        MoveObjectRight(nonCollidingObjects, nextPos);
                        SetAnimationByName("WalkRight");
                    }

                    else if (direction.X <= 0)
                    {
                        nextPos.X += direction.X;
                        MoveObjectLeft(nonCollidingObjects, nextPos);
                        SetAnimationByName("WalkLeft");
                    }
                }
                //pos = nextPos;
            }
            else if (direction.X > 0)
                SetAnimationByName("StandRight");
            else
                SetAnimationByName("StandLeft");


            if (Globals.GetDistance(pos, enemy.hero.pos) < hitDist)
            {
                if (direction.X > 0)
                    SetAnimationByName("AttackRight");
                else
                    SetAnimationByName("AttackLeft");
                enemy.hero.GetHit(1);
                //dead = true;
            }
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
