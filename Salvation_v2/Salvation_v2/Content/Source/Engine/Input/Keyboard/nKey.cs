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
    public class NKey
    {
        public int state;
        public string key, print, display;

        public NKey(string key, int state)
        {
            this.key = key;
            this.state = state;
            MakePrint(this.key);
        }

        public virtual void Update()
        {
            state = 2;
        }

        public void MakePrint(string KEY)
        {
            display = KEY;
            string tempStr = "";

            if (KeyboardHandlerLetter(KEY))
                tempStr = KEY;
            if (KEY == "Space")
                tempStr = " ";
            if (KEY == "OemCloseBrackets")
            {
                tempStr = "]";
                display = tempStr;
            }
            if (KEY == "OemOpenBrackets")
            {
                tempStr = "[";
                display = tempStr;
            }
            if (KEY == "OemMinus")
            {
                tempStr = "-";
                display = tempStr;
            }
            if (KEY == "OemPeriod" || KEY == "Decimal")
                tempStr = ".";
            if (KeyboardHandlerPresymbol("D", KEY))
                tempStr = KEY.Substring(1);
            else if (KeyboardHandlerPresymbol("NumPad", KEY))
                tempStr = KEY.Substring(6);
            print = tempStr;
        }

        private bool KeyboardHandlerLetter(string KEY)
        {
            if ("A".GetHashCode() <= KEY.GetHashCode() && KEY.GetHashCode() <= "Z".GetHashCode())
                return true;
            if ("А".GetHashCode() <= KEY.GetHashCode() && KEY.GetHashCode() <= "Я".GetHashCode())
                return true;
            return false;
        }

        private bool KeyboardHandlerPresymbol(string prestring, string KEY)
        {
            var strings = new string[10];
            for (var i = 0; i < 10; i++)
                strings[i] = $"{prestring}{i}";
            if (strings.Any(s => s.Equals(KEY)))
                return true;
            return false;
        }
    }
}