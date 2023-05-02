#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.MediaFoundation;
#endregion

namespace Salvation_v2
{
    public class Basic2D
    {
        public Texture2D Texture;
        public Vector2 pos;
        public Vector2 size = Vector2.Zero;
        public float rotation;

        public Basic2D(string path, Vector2 pos, Vector2 size) 
        {
            this.pos = pos;
            this.size = size;
            Texture = Globals.Content.Load<Texture2D>(path);
        }

        public virtual void Update(Vector2 offset)
        { 

        }

        public virtual void Draw(Vector2 offset) 
        {
            if (Texture != null) 
                Globals.SpriteBatch.Draw(Texture, new Rectangle((int)(pos.X+offset.X) , (int)(pos.Y + offset.Y), (int)size.X, (int)size.Y), 
                    null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0);
        }

        public virtual void Draw(Vector2 offset, Vector2 origin, Color color)
        {
            if (Texture != null)
                Globals.SpriteBatch.Draw(Texture, new Rectangle((int)(pos.X + offset.X), (int)(pos.Y + offset.Y), (int)size.X, (int)size.Y),
                    null, color, rotation, new Vector2(origin.X, origin.Y), SpriteEffects.None, 0);
        }
    }
}
