using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvation_v2
{
    public class Spike : AttackableObject
    {
        public Spike(Vector2 pos, Vector2 size, float rotation, Vector2 frames, int ownerID, TypeOfDeath diesIn) : base("2D\\Spike", pos, size, frames, ownerID, diesIn)
        {
            this.rotation = rotation;
            health = int.MaxValue;
            healthMax = health;
        }

        public virtual void Update(Vector2 offset, List<AttackableObject> units)
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (IsTouching(units[i]) || Globals.IsInside(pos,units[i].HitBox))
                    units[i].GetHit(1);
            }  
            //base.Update(offset, nonCollidingObjects);
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
