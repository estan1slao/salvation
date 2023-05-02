#region Includes
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Text;
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
    public class Hero : Unit
    {
        private Point currentFrame = new Point(0, 0);
        private Point spriteSize = new Point(8, 2);
        public int frameWidth = 108;
        public int frameHeight = 140;
        private int currentTime = 0;        //Вынос из класса Hero? Позже класс Animated
        private const int period = 50;      //Вынос из класса Hero? Позже класс Animated

        private int spaceCounter = 0;
        private int deltaSpace = 0;
        private float[] speedSpace = new[] { 100f, 150f, 50f };
        private bool spaceNow = false;
        public Hero(string path, Vector2 pos, Vector2 size, int ownerID) : base(path, pos, size, ownerID)
        {
            speed = 10f;
            health = 20;
            healthMax = 20;
        }
        public override void Update(Vector2 offset)
        {
            currentTime += Globals.GameTime.ElapsedGameTime.Milliseconds;
            checkScroll = false;
            if (currentTime > period)
            {
                pos -= offset;
                currentTime -= period;
                if (Globals.Keyboard.GetPress("A") && pos.X > 0)
                {
                    currentFrame.Y = 1;
                    pos = new Vector2(pos.X-speed, pos.Y);
                    ++currentFrame.X;
                    if (currentFrame.X >= spriteSize.X)
                        currentFrame.X = 0;
                }
                if (Globals.Keyboard.GetPress("D") && pos.X < Globals.screenWidth - frameWidth)
                {
                    currentFrame.Y = 0;
                    pos = new Vector2(pos.X + speed, pos.Y);
                    ++currentFrame.X;
                    if (currentFrame.X >= spriteSize.X)
                        currentFrame.X = 0;
                }
                if (Globals.Keyboard.GetPress("Space") && pos.Y > 0)
                {
                    spaceCounter++;
                    if (spaceCounter == 1)
                    {
                        spaceNow = true;
                    }
                    if (spaceCounter == 2)
                    {
                        spaceNow = true;
                    }
                    if (true) // Нужна колизия
                        spaceCounter = 0;
                }
                if(Globals.Keyboard.pressedKeys.Count == 0)
                    currentFrame.X = 0;
            }

            if (spaceNow)
            {
                pos.Y -= speedSpace[deltaSpace];
                deltaSpace++;
                if (deltaSpace == speedSpace.Length)
                {
                    deltaSpace = 0;
                    spaceNow = false;
                }
            }

            if (Globals.Mouse.LeftClick())
                GameGlobals.PassProjectile(new Bullet(new Vector2(pos.X, pos.Y),this,new Vector2(Globals.Mouse.newMousePos.X, Globals.Mouse.newMousePos.Y) - offset));

            base.Update(offset, null);
        }
        public override void Draw(Vector2 offset)
        {
            Globals.SpriteBatch.Draw(Texture, pos, new Rectangle(currentFrame.X*frameWidth, currentFrame.Y * frameHeight, frameWidth, frameHeight), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
