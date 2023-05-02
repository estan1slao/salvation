#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Drawing;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Salvation_v2;
#endregion

namespace Salvation_v2
{
    public class NMouseControl
    {
        public bool dragging, rightDrag;

        public Vector2 newMousePos, oldMousePos, firstMousePos, newMouseAdjustedPos, systemCursorPos, screenLoc;

        public MouseState newMouse, oldMouse, firstMouse;

        private const int deltaPixels = 8;

        public NMouseControl()
        {
            dragging = false;

            newMouse = Mouse.GetState();
            oldMouse = newMouse;
            firstMouse = newMouse;

            newMousePos = new Vector2(newMouse.Position.X, newMouse.Position.Y);
            oldMousePos = new Vector2(newMouse.Position.X, newMouse.Position.Y);
            firstMousePos = new Vector2(newMouse.Position.X, newMouse.Position.Y);

            GetMouseAndAdjust();

            //screenLoc = new Vector2((int)(systemCursorPos.X/Globals.screenWidth), (int)(systemCursorPos.Y/Globals.screenHeight));

        }

        #region Properties

        public MouseState First
        {
            get { return firstMouse; }
        }

        public MouseState New
        {
            get { return newMouse; }
        }

        public MouseState Old
        {
            get { return oldMouse; }
        }

        #endregion

        public void Update()
        {
            GetMouseAndAdjust();
            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                firstMouse = newMouse;
                firstMousePos = newMousePos = GetScreenPos(firstMouse);
            }
        }

        public void UpdateOld()
        {
            oldMouse = newMouse;
            oldMousePos = GetScreenPos(oldMouse);
        }

        public virtual float GetDistanceFromClick() => Globals.GetDistance(newMousePos, firstMousePos);

        public virtual void GetMouseAndAdjust()
        {
            newMouse = Mouse.GetState();
            newMousePos = GetScreenPos(newMouse);
        }

        public int GetMouseWheelChange() => newMouse.ScrollWheelValue - oldMouse.ScrollWheelValue;

        public Vector2 GetScreenPos(MouseState MOUSE) => new Vector2(MOUSE.Position.X, MOUSE.Position.Y);

        public virtual bool LeftClick() =>
            newMouse.LeftButton == ButtonState.Pressed && 
            oldMouse.LeftButton != ButtonState.Pressed && 
            newMouse.Position.X >= 0 && 
            newMouse.Position.X <= Globals.screenWidth && 
            newMouse.Position.Y >= 0 && 
            newMouse.Position.Y <= Globals.screenHeight;

        public virtual bool LeftClickHold()
        {
            bool holding = false;
            if (newMouse.LeftButton == ButtonState.Pressed && 
                oldMouse.LeftButton == ButtonState.Pressed && 
                newMouse.Position.X >= 0 && 
                newMouse.Position.X <= Globals.screenWidth && 
                newMouse.Position.Y >= 0 && 
                newMouse.Position.Y <= Globals.screenHeight)
            {
                holding = true;
                if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > deltaPixels || 
                    Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > deltaPixels)
                    dragging = true;
            }
            return holding;
        }

        public virtual bool LeftClickRelease()
        {
            if (newMouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed)
            {
                dragging = false;
                return true;
            }
            return false;
        }

        public virtual bool RightClick() => 
            newMouse.RightButton == ButtonState.Pressed && 
            oldMouse.RightButton != ButtonState.Pressed && 
            newMouse.Position.X >= 0 && 
            newMouse.Position.X <= Globals.screenWidth && 
            newMouse.Position.Y >= 0 && 
            newMouse.Position.Y <= Globals.screenHeight;


        public virtual bool RightClickHold()
        {
            bool holding = false;
            if (newMouse.RightButton == ButtonState.Pressed && 
                oldMouse.RightButton == ButtonState.Pressed && 
                newMouse.Position.X >= 0 && 
                newMouse.Position.X <= Globals.screenWidth && 
                newMouse.Position.Y >= 0 && 
                newMouse.Position.Y <= Globals.screenHeight)
            {
                holding = true;
                if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > deltaPixels || 
                    Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > deltaPixels)
                    rightDrag = true;
            }
            return holding;
        }

        public virtual bool RightClickRelease()
        {
            if (newMouse.RightButton == ButtonState.Released && 
                oldMouse.RightButton == ButtonState.Pressed)
            {
                dragging = false;
                return true;
            }
            return false;
        }

        public void SetFirst()
        {

        }
    }
}

