#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
#endregion

namespace Salvation_v2
{
    public class UI
    {
        public SpriteFont font12;
        public SpriteFont font36;
        public SpriteFont font52;
        public QuantityDisplayBar healthBar;
        public UI()
        {
            font12 = Globals.Content.Load<SpriteFont>("ArialFonts\\Arial12");
            font36 = Globals.Content.Load<SpriteFont>("ArialFonts\\Arial36");
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
            Globals.SpriteBatch.DrawString(font12, strKilled, new Vector2(Globals.screenWidth / 2 - strKilledSize.X / 2, 10), Color.Red);
            healthBar.Draw(new Vector2(20, Globals.screenHeight - 60));

            if (world.user.hero.dead)
            {
                var deadTitle = "Press \"R\" to Restart";
                var deadTitleSize = font52.MeasureString(deadTitle);
                var position = new Vector2(Globals.screenWidth / 2 - deadTitleSize.X / 2, Globals.screenHeight / 2 - deadTitleSize.Y / 2);
                var background = new Basic2D("2D\\bgDeadHero", new Vector2(position.X - 40f, position.Y - 40f), new Vector2(deadTitleSize.X + 80f, deadTitleSize.Y + 80f));
                background.Draw(Vector2.Zero);
                Globals.SpriteBatch.DrawString(font52, deadTitle, position, Color.Blue);
            }

            for (int i = 0; i < world.buttons.Count; i++)
            {
                var button = world.buttons[i];
                if (button.isPressed)
                {
                    var time = new TimeSpan(0, 0, 0, 0, button.ActiveTime.MSec - button.ActiveTime.Timer);
                    var timerTitle = $"{time.ToString(@"ss\.ff")}";
                    var timerTitleSize = font36.MeasureString(timerTitle);
                    Globals.SpriteBatch.DrawString(font36, timerTitle, new Vector2(button.pos.X + button.size.X / 2 - timerTitleSize.X / 2, button.pos.Y - timerTitleSize.Y - 75), Color.Blue);
                }
            }
        }
    }
}
