#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
    public class User : Player
    {
        public User(int id, XElement data) : base(id, data)
        {
            //hero = new Hero("2D\\hero", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(108, 140), id);
        }

        public override void Update(Player enemy, Vector2 offset)
        {
            base.Update(enemy, offset);
        }
    }
}
