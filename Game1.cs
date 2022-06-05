using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Lesson_3
{
    public class Game1 : Game
    {
        enum Screen
        {
            Intro,
            TribbleYard,
            Outro
        }
        Screen screen;

        MouseState mouseState;
        Texture2D tribbleIntroTexture;
        SpriteFont tribbleIntroText;

        KeyboardState keyboardState;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D tribbleGreyTexture;
        Rectangle greyTribbleRect;
        Vector2 greyTribbleSpeed;
        Texture2D tribbleOrangeTexture;
        Rectangle orangeTribbleRect;
        Vector2 orangeTribbleSpeed;
        Texture2D tribbleCreamTexture;
        Rectangle creamTribbleRect;
        Vector2 creamTribbleSpeed;
        Texture2D tribbleBrownTexture;
        Rectangle brownTribbleRect;
        Vector2 brownTribbleSpeed;
        Random generator = new Random();

        SoundEffect tribbleCoo;
        SoundEffectInstance tribbleCooInstance;
        bool wallHit;

        Color backgroundColor = Color.White;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800; // Sets the width of the window
            _graphics.PreferredBackBufferHeight = 500; // Sets the height of the window
            _graphics.ApplyChanges(); // Applies the new dimensions
            // TODO: Add your initialization logic here
            greyTribbleRect = new Rectangle(300, 10, 100, 100);
            greyTribbleSpeed = new Vector2(0, 3);

            orangeTribbleRect = new Rectangle(300, 10, 100, 100);
            orangeTribbleSpeed = new Vector2(6, 0);

            brownTribbleRect = new Rectangle(300, 10, 100, 100);
            brownTribbleSpeed = new Vector2(5, 7);

            creamTribbleRect = new Rectangle(300, 200,100, 100);
            creamTribbleSpeed = new Vector2(5, 0);

            wallHit = false;

            screen = Screen.Intro;

            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            tribbleGreyTexture = Content.Load<Texture2D>("tribbleGrey");
            greyTribbleRect = new Rectangle(0, 0, 100, 100);

            tribbleOrangeTexture = Content.Load<Texture2D>("tribbleOrange");
            orangeTribbleRect = new Rectangle(0, 0, 100, 100);

            tribbleBrownTexture = Content.Load<Texture2D>("tribbleBrown");
            brownTribbleRect = new Rectangle(300, 10, 100, 100);

            tribbleCreamTexture = Content.Load<Texture2D>("tribbleCream");
            creamTribbleRect = new Rectangle(0, generator.Next(1,500), 100, 100);

            tribbleCoo = Content.Load<SoundEffect>("tribble_coo");
            tribbleCooInstance = tribbleCoo.CreateInstance();

            tribbleIntroText = Content.Load<SpriteFont>("tribbleIntroText");
            tribbleIntroTexture = Content.Load<Texture2D>("tribble_intro");

        }
        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            mouseState = Mouse.GetState();
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.TribbleYard;
            }
            else if (screen == Screen.TribbleYard)
            {
                // TODO: Add your update logic here
                greyTribbleRect.X += (int)greyTribbleSpeed.X;
                greyTribbleRect.Y += (int)greyTribbleSpeed.Y;
                if (greyTribbleRect.Bottom > _graphics.PreferredBackBufferHeight || greyTribbleRect.Top < 0)
                {
                    greyTribbleSpeed.Y *= -1;
                    tribbleCooInstance.Stop();
                    wallHit = true;
                }
                orangeTribbleRect.X += (int)orangeTribbleSpeed.X;
                orangeTribbleRect.Y += (int)orangeTribbleSpeed.Y;
                if (orangeTribbleRect.Right > _graphics.PreferredBackBufferWidth || orangeTribbleRect.Left < 0)
                {
                    orangeTribbleSpeed.X *= -1;
                    tribbleCooInstance.Stop();
                    wallHit = true;
                }
                brownTribbleRect.X += (int)brownTribbleSpeed.X;
                brownTribbleRect.Y += (int)brownTribbleSpeed.Y;
                if (brownTribbleRect.Top > _graphics.PreferredBackBufferHeight || brownTribbleRect.Left > _graphics.PreferredBackBufferWidth || brownTribbleRect.Right < 0 || brownTribbleRect.Bottom < 0)
                {
                    brownTribbleRect = new Rectangle(creamTribbleRect.X, creamTribbleRect.Y, 100, 100);
                    brownTribbleSpeed.X = generator.Next(-8, 8);
                    brownTribbleSpeed.Y = generator.Next(-8, 8);
                    tribbleCooInstance.Stop();
                    wallHit = true;

                    while (brownTribbleSpeed.X == 0 && brownTribbleSpeed.Y == 0)
                    {
                        brownTribbleSpeed.X = generator.Next(-8, 8);
                        brownTribbleSpeed.Y = generator.Next(-8, 8);
                    }
                }
                creamTribbleRect.X += (int)creamTribbleSpeed.X;
                creamTribbleRect.Y += (int)creamTribbleSpeed.Y;
                if (creamTribbleRect.Left > _graphics.PreferredBackBufferWidth)
                {
                    creamTribbleRect.X = -creamTribbleRect.Width;
                    creamTribbleRect.Y = generator.Next(_graphics.PreferredBackBufferHeight - creamTribbleRect.Height);
                    tribbleCooInstance.Stop();
                    wallHit = true;
                }
                if (wallHit == true)
                    tribbleCooInstance.Play();
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (screen == Screen.Outro)
            {
                _spriteBatch.Draw(tribbleIntroTexture, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.DrawString(tribbleIntroText, "OUTRO SCREEN", new Vector2(250, 150), Color.Red);
                tribbleCooInstance.Stop();
            }

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(tribbleIntroTexture, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.DrawString(tribbleIntroText, "CLICK ANYWHERE TO CONTINUE", new Vector2(90, 10), Color.White);
            }
            else if (keyboardState.IsKeyDown(Keys.Space))
            {
                screen = Screen.Outro;
            }
            else if (screen == Screen.TribbleYard)
            {
                _spriteBatch.DrawString(tribbleIntroText, "PRESS SPACE FOR OUTRO SCREEN", new Vector2(85, 10), Color.White);
                _spriteBatch.Draw(tribbleGreyTexture, greyTribbleRect, Color.White);
                _spriteBatch.Draw(tribbleOrangeTexture, orangeTribbleRect, Color.White);
                _spriteBatch.Draw(tribbleBrownTexture, brownTribbleRect, Color.White);
                _spriteBatch.Draw(tribbleCreamTexture, creamTribbleRect, Color.White);
                
                if (orangeTribbleRect.Right >= _graphics.PreferredBackBufferWidth)
                {
                    backgroundColor = Color.Green;
                }
                else if (orangeTribbleRect.Left < 0)
                {
                    backgroundColor = Color.Blue;
                }
                else if (greyTribbleRect.Bottom > _graphics.PreferredBackBufferHeight)
                {
                    backgroundColor = Color.Red;
                }
                else if (greyTribbleRect.Top < 0)
                {
                    backgroundColor = Color.Purple;
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
