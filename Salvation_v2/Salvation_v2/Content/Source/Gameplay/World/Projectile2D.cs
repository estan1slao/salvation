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
    public class Projectile2D : Basic2D
    {
        public float speed;
        public Unit owner;
        public NTimer timer;
        public bool done;
        public Vector2 direction;

        public Projectile2D(string path, Vector2 pos, Vector2 size, Unit owner, Vector2 target) : base(path, pos, size)
        {
            speed = 45f;
            this.owner = owner;
            done = false;

            direction = target - pos;
            direction.Normalize();

            rotation = Globals.RotateTowards(pos, new Vector2(target.X, target.Y));

            timer = new NTimer(1500);
        }

        public virtual void Update(Vector2 offset, List<AttackableObject> units, List<Basic2D> nonCollidingObjects)
        {
            pos += direction * speed;
            timer.UpdateTimer();
            if (timer.Test())
                done = true;
            if (HitSomething(units, nonCollidingObjects))
                done = true;
        }

        public virtual bool HitSomething(List<AttackableObject> units, List<Basic2D> nonCollidingObjects)
        {
            for (int i =0; i < units.Count; i++)
            {
                if (owner.ownerID != units[i].ownerID && Globals.GetDistance(pos, units[i].pos + units[i].size/2) < units[i].hitDist)
                {
                    if (units[i].diesIn == TypeOfDeath.Everywhere)
                        units[i].GetHit(1);
                    else if (units[i].diesIn == TypeOfDeath.OnlyReal && !GameGlobals.isParallel)
                        units[i].GetHit(1);
                    else if (units[i].diesIn == TypeOfDeath.OnlyParallel && GameGlobals.isParallel)
                        units[i].GetHit(1);
                    return true;
                }
            }
            for(int i = 0; i < nonCollidingObjects.Count; i++)
            {
                if (Globals.IsInside(pos, nonCollidingObjects[i].HitBox))
                    return true;
            }
            return false;
        }

        public override void Draw(Vector2 offset) => base.Draw(offset);
    }
}
