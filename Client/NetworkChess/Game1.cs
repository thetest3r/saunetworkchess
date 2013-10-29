using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Board_piece;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
namespace NetworkChess
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        //Represents the white_pawn piece
        ChessPieces white_pawn;

        //Keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        //Gamepas used to determine button presses
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        //Mouse states to track Mouse button presses
        MouseState currentMouseState;
        MouseState previousMouseState;
        
        //A movement speed for the player
        float playerMoveSpeed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Initizalize the pieces
            white_pawn = new ChessPieces();
            playerMoveSpeed = 8.0f;
            //Enable the FreeDrag gesture
            TouchPanel.EnabledGestures = GestureType.FreeDrag;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Vector2 white_pawn_position = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
                GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            white_pawn.Initialize(Content.Load<Texture2D>("Graphics\\white_pawn"), white_pawn.Position);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            //Save the previos state of the keyboard and game pad so we can determine single key/button presses
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;
            //Read the current state of the kaybord and gamepad and store it
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            // Update the player
            UpdatePlayer(gameTime);

            base.Update(gameTime);
        }
        private void UpdatePlayer(GameTime gameTime)
        {
            //Get thumbstick controls
            white_pawn.Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
            white_pawn.Position.Y += currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;
            //Use the Keyboard/Dpad
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                white_pawn.Position.X -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                white_pawn.Position.X += playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                white_pawn.Position.Y -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                white_pawn.Position.Y += playerMoveSpeed;
            }

            white_pawn.Position.X = MathHelper.Clamp(white_pawn.Position.X, 0, GraphicsDevice.Viewport.Width - white_pawn.Width);
            white_pawn.Position.Y = MathHelper.Clamp(white_pawn.Position.Y, 0, GraphicsDevice.Viewport.Height - white_pawn.Height);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //Start drawing
            _spriteBatch.Begin();
            //Draw the player
            white_pawn.Draw(_spriteBatch);
            //Stop drawing
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
