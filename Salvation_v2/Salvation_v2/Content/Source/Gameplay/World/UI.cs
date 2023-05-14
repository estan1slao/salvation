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
    public class UI
    {
        public SpriteFont font12;
        public SpriteFont font52;
        public QuantityDisplayBar healthBar;
        public UI() 
        {
            font12 = Globals.Content.Load<SpriteFont>("ArialFonts\\Arial12");
            font52 = Globals.Content.Load<SpriteFont>("ArialFonts\\Arial52");
            healthBar = new QuantityDisplayBar(new Vector2(195, 32), 2, Color.Red);
        }
        
        
        public void Update(World world)
        {
            healthBar.Update(world.user.hero.health, world.user.hero.healthMax);
        }

        public void Draw(World world)
        {
            var strKilled = $"Killed {GameGlobals.score}";
            var strKilledSize = font12.MeasureString(strKilled);
            Globals.SpriteBatch.DrawString(font12, strKilled, new Vector2(Globals.screenWidth/2 - strKilledSize.X/2, 10), Color.DarkRed);
            healthBar.Draw(new Vector2(20, Globals.screenHeight - 60));

            if (world.user.hero.dead)
            {
                var deadTitle = "Press \"R\" to Restart";
                var deadTitleSize = font52.MeasureString(deadTitle);
                Globals.SpriteBatch.DrawString(font52, deadTitle, new Vector2(Globals.screenWidth / 2 - deadTitleSize.X / 2, Globals.screenHeight/2 - deadTitleSize.Y / 2), Color.Coral);

            }
        }
    }
}
