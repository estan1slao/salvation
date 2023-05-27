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
        private State _currentState;
        public State _nextState;
        Basic2D cursor;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public void ChangeState(State state) => _nextState = state;

        protected override void Initialize()
        {
            Globals.screenWidth = 1920;
            Globals.screenHeight = 1080;

            _graphics.PreferredBackBufferWidth = Globals.screenWidth;
            _graphics.PreferredBackBufferHeight = Globals.screenHeight;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.Content = Content;
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);

            cursor = new Basic2D("2D\\interface\\cursor",new Vector2(0,0), new Vector2(30,40));
            Globals.Keyboard = new NKeyboard();
            Globals.Mouse = new NMouseControl();

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (_currentState is GameState)
                {
                    _nextState = new MenuState(this, _graphics.GraphicsDevice, Content);
                    Thread.Sleep(200);
                }
                else
                    Exit();
            }

            if(GameGlobals.isEnd)
                _nextState = new MenuState(this, _graphics.GraphicsDevice, Content);
            GameGlobals.isEnd = false;

            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            Globals.GameTime = gameTime;
            Globals.Keyboard.Update();
            Globals.Mouse.Update();

            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            Globals.Keyboard.UpdateOld();
            Globals.Mouse.UpdateOld();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Globals.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("2D\\backgrounds\\bgMain"), new Vector2(0, 0), Color.White);
            Globals.SpriteBatch.End();

            _currentState.Draw(gameTime, Globals.SpriteBatch);
            
            Globals.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            cursor.Draw(new Vector2(Globals.Mouse.newMousePos.X, Globals.Mouse.newMousePos.Y), new Vector2(0, 0), Color.White);
            Globals.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}