#region Includes
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Salvation_v2
{
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Main game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("2D\\Controls\\ButtonCtrl");
            var buttonFont = _content.Load<SpriteFont>("ArialFonts\\Arial52");

            var newGameButton = new ButtonCtrl(buttonTexture, buttonFont, new Vector2(600, 150), Color.Black)
            {
                Position = new Vector2(660, 465),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var quitGameButton = new ButtonCtrl(buttonTexture, buttonFont, new Vector2(600, 150), Color.Red)
            {
                Position = new Vector2(660, 815),
                Text = "Quit",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
            {
                newGameButton,
                quitGameButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
