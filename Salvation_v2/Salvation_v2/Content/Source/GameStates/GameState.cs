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
#endregion
namespace Salvation_v2
{
    public class GameState : State
    {
        GamePlay gamePlay;
        public GameState(Main game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            GameGlobals.levelNumber = 1;
            gamePlay = new GamePlay(GameGlobals.levelNumber);
        }

        public override void Update(GameTime gameTime)
        {
            gamePlay.Update(GameGlobals.levelNumber);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Globals.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            if (GameGlobals.isParallel)
                Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("2D\\backgrounds\\backgroundPar"), new Vector2(0, 0), Color.White);
            else
                Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("2D\\backgrounds\\backgroundReal"), new Vector2(0, 0), Color.White);
            gamePlay.Draw();
            Globals.SpriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }
    }
}
