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
    public class QuantityDisplayBar
    {
        public int boarder;
        public Basic2D bar, barBKG;
        public Color color;
        public QuantityDisplayBar(Vector2 size, int boarder, Color color)
        {
            this.boarder = boarder;
            this.color = color;

            bar = new Basic2D("2D\\interface\\solid", new Vector2(0,0), new Vector2(size.X - boarder*2, size.Y - boarder*2));
            barBKG = new Basic2D("2D\\interface\\shade", new Vector2(0, 0), new Vector2(size.X , size.Y));
        }

        public virtual void Update(float current, float max)
        {
            bar.size = new Vector2(current/max*(barBKG.size.X-boarder*2),bar.size.Y);
        }

        public virtual void Draw(Vector2 offset)
        {
            barBKG.Draw(offset, new Vector2 (0,0), Color.Black);
            bar.Draw(offset + new Vector2(boarder, boarder), new Vector2(0, 0), color);
        }
    }
}
