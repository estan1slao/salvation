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
        }

        public override void Update(Player enemy, Vector2 offset, List<Basic2D> nonCollidingObjects, List<Door> doors)
        {
            base.Update(enemy, offset, nonCollidingObjects, doors);
        }
    }
}
