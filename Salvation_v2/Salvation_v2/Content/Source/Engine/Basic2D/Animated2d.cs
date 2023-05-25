#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
#endregion

namespace Salvation_v2
{
    public class Animated2d : Basic2D
    {
        public Vector2 frames;
        public List<FrameAnimation> frameAnimationList = new List<FrameAnimation>();
        public Color color;
        public bool frameAnimations;
        public int currentAnimation = 0;

        #region Properties
        public Vector2 Frames
        {
            set
            {
                frames = value;
                if (Texture != null)
                    frameSize = new Vector2(Texture.Bounds.Width / frames.X, Texture.Bounds.Height / frames.Y);
            }
            get
            {
                return frames;
            }
        }
        #endregion

        public Animated2d(string path, Vector2 pos, Vector2 size, Vector2 frames, Color color) : base(path, pos, size)
        {
            Frames = new Vector2(frames.X, frames.Y);
            this.color = color;
        }

        public override void Update(Vector2 offset, List<Basic2D> nonCollidingObjects, List<Door> doors)
        {
            if (frameAnimations && frameAnimationList != null && frameAnimationList.Count > currentAnimation)
                frameAnimationList[currentAnimation].Update();

            base.Update(offset, nonCollidingObjects, doors);
        }

        public virtual int GetAnimationFromName(string animationName)
        {
            for (int i = 0; i < frameAnimationList.Count; i++)
                if (frameAnimationList[i].Name == animationName)
                    return i;
            return -1;
        }

        public virtual void SetAnimationByName(string name)
        {
            int tempAnimation = GetAnimationFromName(name);
            if (tempAnimation != -1)
            {
                if (tempAnimation != currentAnimation)
                    frameAnimationList[tempAnimation].Reset();
                currentAnimation = tempAnimation;
            }
        }

        public override void Draw(Vector2 screenShift)
        {
            if (frameAnimations && frameAnimationList[currentAnimation].Frames > 0)
                frameAnimationList[currentAnimation].Draw(Texture, size, frameSize, screenShift, new Vector2(HitBox.Center.X, HitBox.Center.Y), rotation, color, new SpriteEffects());
            else
                base.Draw(screenShift);
        }
    }
}
