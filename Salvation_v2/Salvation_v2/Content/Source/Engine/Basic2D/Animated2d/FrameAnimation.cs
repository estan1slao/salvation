#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
#endregion

namespace Salvation_v2
{
    public class FrameAnimation
    {
        public bool hasFired;
        public int frames, currentFrame, maxPasses, currentPass, fireFrame;
        public string name;
        public Vector2 sheet, startFrame, sheetFrame, spriteDims;
        public NTimer frameTimer;
        public PassObject FireAction;

        public FrameAnimation(Vector2 SpriteDims, Vector2 sheetDims, Vector2 start, int totalframes, int timePerFrame, int MAXPASSES, string NAME = "")
        {
            spriteDims = SpriteDims;
            sheet = sheetDims;
            startFrame = start;
            sheetFrame = new Vector2(start.X, start.Y);
            frames = totalframes;
            currentFrame = 0;
            frameTimer = new NTimer(timePerFrame);
            maxPasses = MAXPASSES;
            currentPass = 0;
            name = NAME;
            FireAction = null;
            hasFired = false;
            fireFrame = 0;
        }

        public FrameAnimation(Vector2 SpriteDims, Vector2 sheetDims, Vector2 start, int totalframes, int timePerFrame, int MAXPASSES, int FIREFRAME, PassObject FIREACTION, string NAME = "")
        {
            spriteDims = SpriteDims;
            sheet = sheetDims;
            startFrame = start;
            sheetFrame = new Vector2(start.X, start.Y);
            frames = totalframes;
            currentFrame = 0;
            frameTimer = new NTimer(timePerFrame);
            maxPasses = MAXPASSES;
            currentPass = 0;
            name = NAME;
            FireAction = FIREACTION;
            hasFired = false;
            fireFrame = FIREFRAME;
        }

        #region Properties
        public int Frames { get => frames; }
        public int CurrentFrame { get => currentFrame; }
        public int CurrentPass { get => currentPass; }
        public int MaxPasses { get => maxPasses; }
        #endregion

        public void Update()
        {
            if (frames > 1)
            {
                frameTimer.UpdateTimer();
                if (frameTimer.Test() && (maxPasses == 0 || maxPasses > currentPass))
                {
                    currentFrame++;
                    if (currentFrame >= frames)
                        currentPass++;
                    if (maxPasses == 0 || maxPasses > currentPass)
                    {
                        sheetFrame.X += 1;
                        if (sheetFrame.X >= sheet.X)
                        {
                            sheetFrame.X = 0;
                            sheetFrame.Y += 1;
                        }
                        if (currentFrame >= frames)
                        {
                            currentFrame = 0;
                            hasFired = false;
                            sheetFrame = new Vector2(startFrame.X, startFrame.Y);
                        }
                    }
                    frameTimer.Reset();
                }
            }
            if (FireAction != null && fireFrame == currentFrame && !hasFired)
            {
                FireAction(null);
                hasFired = true;
            }
        }

        public void Reset()
        {
            currentFrame = 0;
            currentPass = 0;
            sheetFrame = new Vector2(startFrame.X, startFrame.Y);
            hasFired = false;
        }

        public bool IsAtEnd() => currentFrame + 1 >= frames;


        public void Draw(Texture2D myModel, Vector2 dims, Vector2 imageDims, Vector2 screenShift, Vector2 pos, float ROT, Color color, SpriteEffects spriteEffect)
        {
            Globals.SpriteBatch.Draw(myModel, new Rectangle((int)((pos.X + screenShift.X)), (int)((pos.Y + screenShift.Y)), (int)Math.Ceiling(dims.X), (int)Math.Ceiling(dims.Y)), new Rectangle((int)(sheetFrame.X * imageDims.X), (int)(sheetFrame.Y * imageDims.Y), (int)imageDims.X, (int)imageDims.Y), color, ROT, imageDims / 2, spriteEffect, 0);
            //Globals.SpriteBatch.Draw(Texture, pos, new Rectangle(currentFrame.X * frameWidth, currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }

    }
}
