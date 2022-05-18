using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Lesson_3
{
    public class Game1 : Game
    {
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
            greyTribbleSpeed = new Vector2(0, 6);

            orangeTribbleRect = new Rectangle(300, 10, 100, 100);
            orangeTribbleSpeed = new Vector2(11, 0);

            creamTribbleRect = new Rectangle(300, 10, 100, 100);
            creamTribbleSpeed = new Vector2(5, 0);

            brownTribbleRect = new Rectangle(300, 10, 100, 100);
            brownTribbleSpeed = new Vector2(5, 7);

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

            tribbleCreamTexture = Content.Load<Texture2D>("tribbleCream");
            creamTribbleRect = new Rectangle(0, 200, 100, 100);

            tribbleBrownTexture = Content.Load<Texture2D>("tribbleBrown");
            brownTribbleRect = new Rectangle(300, 10, 100, 100);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            greyTribbleRect.X += (int)greyTribbleSpeed.X;
            greyTribbleRect.Y += (int)greyTribbleSpeed.Y;
            if (greyTribbleRect.Bottom > _graphics.PreferredBackBufferHeight || greyTribbleRect.Top < 0)
                greyTribbleSpeed.Y *= -1;

            orangeTribbleRect.X += (int)orangeTribbleSpeed.X;
            orangeTribbleRect.Y += (int)orangeTribbleSpeed.Y;
            if (orangeTribbleRect.Right > _graphics.PreferredBackBufferWidth || orangeTribbleRect.Left < 0)
                orangeTribbleSpeed.X *= -1;

            creamTribbleRect.X += (int)creamTribbleSpeed.X;
            creamTribbleRect.Y += (int)creamTribbleSpeed.Y;
            if (creamTribbleRect.Right > _graphics.PreferredBackBufferWidth + 100)
                creamTribbleRect = new Rectangle(0, 200, 100, 100);

            brownTribbleRect.X += (int)brownTribbleSpeed.X;
            brownTribbleRect.Y += (int)brownTribbleSpeed.Y;
            if (brownTribbleRect.Right > _graphics.PreferredBackBufferWidth + 100)
                brownTribbleRect = creamTribbleRect;
            if (brownTribbleRect.Bottom > _graphics.PreferredBackBufferHeight + 100)
                brownTribbleRect = creamTribbleRect;

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);
            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(tribbleGreyTexture, greyTribbleRect, Color.White);
            _spriteBatch.Draw(tribbleOrangeTexture, orangeTribbleRect, Color.White);
            _spriteBatch.Draw(tribbleCreamTexture, creamTribbleRect, Color.White);
            _spriteBatch.Draw(tribbleBrownTexture, brownTribbleRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
