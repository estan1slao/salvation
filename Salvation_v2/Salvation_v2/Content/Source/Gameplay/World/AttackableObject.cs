﻿#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
#endregion

namespace Salvation_v2
{
    public class AttackableObject : Animated2d
    {
        public int ownerID;
        public bool dead;
        public float speed, hitDist, health, healthMax, visibilityRadius;
        public TypeOfDeath diesIn;
        public AttackableObject(string path, Vector2 pos, Vector2 size, Vector2 frames, int ownerID, TypeOfDeath diesIn) : base(path, pos, size, frames, Color.White)
        {
            dead = false;
            speed = 1.0f;

            health = 1;
            healthMax = health;

            visibilityRadius = 1.0f;
            hitDist = 1f;
            this.ownerID = ownerID;
            this.diesIn = diesIn;
        }
        public virtual void Update(Vector2 offset, Player enemy, List<Basic2D> nonCollidingObjects, List<Door> doors)
        {
            base.Update(offset, nonCollidingObjects, doors);
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

        #region Collision
        protected bool IsTouchingLeft(Basic2D sprite)
        {
            return HitBox.Right + speed > sprite.HitBox.Left &&
                   HitBox.Left < sprite.HitBox.Left &&
                   HitBox.Bottom - Globals.Gravitation > sprite.HitBox.Top &&
                   HitBox.Top < sprite.HitBox.Bottom;
        }

        protected bool IsTouchingRight(Basic2D sprite)
        {
            return HitBox.Left - speed < sprite.HitBox.Right &&
                   HitBox.Right > sprite.HitBox.Right &&
                   HitBox.Bottom - Globals.Gravitation > sprite.HitBox.Top &&
                   HitBox.Top < sprite.HitBox.Bottom;
        }

        protected bool IsTouchingTop(Basic2D sprite)
        {
            return HitBox.Bottom + speed > sprite.HitBox.Top &&
                   HitBox.Top < sprite.HitBox.Top &&
                   HitBox.Right > sprite.HitBox.Left &&
                   HitBox.Left < sprite.HitBox.Right;
        }

        protected bool IsTouchingBottom(Basic2D sprite)
        {
            return HitBox.Top - speed < sprite.HitBox.Bottom &&
                   HitBox.Bottom > sprite.HitBox.Bottom &&
                   HitBox.Right > sprite.HitBox.Left &&
                   HitBox.Left < sprite.HitBox.Right;
        }

        protected bool IsTouching(Basic2D sprite) =>
            IsTouchingLeft(sprite) || IsTouchingRight(sprite) || IsTouchingTop(sprite) || IsTouchingBottom(sprite);

        public void MoveObjectRight(List<Basic2D> nonCollidingObjects, Vector2 nextPos) =>
            MoveObjectAside(nonCollidingObjects, nextPos, IsTouchingLeft);

        public void MoveObjectLeft(List<Basic2D> nonCollidingObjects, Vector2 nextPos) =>
            MoveObjectAside(nonCollidingObjects, nextPos, IsTouchingRight);

        public void MoveObjectBottom(List<Basic2D> nonCollidingObjects, Vector2 nextPos) =>
            MoveObjectAside(nonCollidingObjects, nextPos, IsTouchingTop);

        private void MoveObjectAside(List<Basic2D> nonCollidingObjects, Vector2 nextPos, Func<Basic2D, bool> IsTouch)
        {
            var isTouch = false;
            foreach (var sprite in nonCollidingObjects)
            {
                if (sprite == this)
                    continue;
                if (IsTouch(sprite))
                {
                    isTouch = true;
                    break;
                }
            }
            if (!isTouch)
                pos = nextPos;
        }

        public void MoveObjectTop(List<Basic2D> nonCollidingObjects, Vector2 nextPos)
        {
            var isTouchingBottom = false;
            var isTouchingLeft = false;
            var isTouchingRight = false;
            foreach (var sprite in nonCollidingObjects)
            {
                if (sprite == this)
                    continue;
                if (IsTouchingBottom(sprite))
                    isTouchingBottom = true;
                if (IsTouchingLeft(sprite))
                    isTouchingLeft = true;
                if (IsTouchingRight(sprite))
                    isTouchingRight = true; 
                if (isTouchingBottom && isTouchingLeft && isTouchingRight)
                    break;
            }
            if (isTouchingBottom && !isTouchingBottom && !isTouchingLeft)
                pos.X = nextPos.X;
            if ((isTouchingLeft || isTouchingRight) && !isTouchingBottom)
                pos.Y = nextPos.Y;
            if (!(isTouchingBottom || isTouchingLeft || isTouchingRight))
                pos = nextPos;
        }
        #endregion
    }
}
