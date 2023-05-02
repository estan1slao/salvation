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
    public class AttackableObject : Basic2D
    {
        public int ownerID;
        public bool dead;
        public float speed, hitDist, health, healthMax, visibilityRadius;
        public bool checkScroll = false;
        public AttackableObject(string path, Vector2 pos, Vector2 size, int ownerID) : base(path, pos, size)
        {
            dead = false;
            speed = 0.0f;

            health = 0;
            healthMax = health;

            visibilityRadius = 0.0f;
            hitDist = 0f;
            this.ownerID = ownerID;
        }
        public virtual void Update(Vector2 offset, Player enemy)
        {
            base.Update(offset);
        }

        public virtual void GetHit(float damage)
        {
            health -= damage;
            if (health <= 0) 
                dead = true;
        }
        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
