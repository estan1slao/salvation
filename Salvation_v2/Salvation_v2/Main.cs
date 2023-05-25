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
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        GamePlay gamePlay;
        Basic2D cursor;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Globals.screenWidth = 1920;
            Globals.screenHeight = 1080;
            Globals.Gravitation = 20f;

            _graphics.PreferredBackBufferWidth = Globals.screenWidth;
            _graphics.PreferredBackBufferHeight = Globals.screenHeight;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.Content = this.Content;
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);

            cursor = new Basic2D("2D\\interface\\cursor",new Vector2(0,0), new Vector2(30,40));
            Globals.Keyboard = new NKeyboard();
            Globals.Mouse = new NMouseControl();

            gamePlay = new GamePlay(GameGlobals.levelNumber);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.GameTime = gameTime;
            Globals.Keyboard.Update();
            Globals.Mouse.Update();

            gamePlay.Update(GameGlobals.levelNumber);

            Globals.Keyboard.UpdateOld();
            Globals.Mouse.UpdateOld();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            Globals.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            if(GameGlobals.isParallel)
                Globals.SpriteBatch.Draw(Content.Load<Texture2D>("2D\\backgrounds\\backgroundPar"), new Vector2(0, 0), Color.White);
            else
                Globals.SpriteBatch.Draw(Content.Load<Texture2D>("2D\\backgrounds\\backgroundReal"), new Vector2(0, 0), Color.White);
            gamePlay.Draw();
            cursor.Draw(new Vector2(Globals.Mouse.newMousePos.X, Globals.Mouse.newMousePos.Y), new Vector2(0, 0), Color.White);

            Globals.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}