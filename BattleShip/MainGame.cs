using BattleShip.GameFolder;
using BattleShip.GameFolder.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleShip
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Controller control;
        Screen screen;
       

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = (int)Screen.ScreenDim.X;
            graphics.PreferredBackBufferHeight = (int)Screen.ScreenDim.Y;
        }

 
        protected override void Initialize()
        {
            IsMouseVisible = true;

            control = new Controller(this);
            screen = new Screen();        
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Image.LoadTextures(this);
            screen.GetBatch(spriteBatch);            
            screen.CreateBoard();
           
        }

        protected override void UnloadContent() { Content.Unload(); }


        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            control.UpdateState();

            control.HoveredTile();
            control.CheckButtonClicks();
            control.RunPhase();

            control.UpdateOldState();
            base.Update(gameTime);
        }

 
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);
            spriteBatch.Begin();

            screen.ShowDebug(control.oPhase);
            screen.ShowBoard();
            screen.ShowShips();

            screen.ShowButtons();

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
