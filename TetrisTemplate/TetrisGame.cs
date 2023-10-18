using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

class TetrisGame : Game
{
    SpriteBatch spriteBatch;
    InputHelper inputHelper;
    GameWorld gameWorld;
    Texture2D block;

    /// <summary>
    /// A static reference to the ContentManager object, used for loading assets.
    /// </summary>
    public static ContentManager ContentManager { get; private set; }
    

    /// <summary>
    /// A static reference to the width and height of the screen.
    /// </summary>
    public static Point ScreenSize { get; private set; }

    [STAThread]
    static void Main(string[] args)
    {
        TetrisGame game = new TetrisGame();
        game.Run();
    }

    public TetrisGame()
    {        
        // initialize the graphics device
        GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);

        // store a static reference to the content manager, so other objects can use it
        ContentManager = Content;
        
        // set the directory where game assets are located
        Content.RootDirectory = "Content";

        // set the desired window size
        ScreenSize = new Point(800, 600);
        graphics.PreferredBackBufferWidth = ScreenSize.X;
        graphics.PreferredBackBufferHeight = ScreenSize.Y;

        // create the input helper object
        inputHelper = new InputHelper();
        bool[,] t_block = new bool[3, 3] { { true, true, true }, { false, true, false }, { false, false, false } };
}

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        // create and reset the game world
        gameWorld = new GameWorld();
        gameWorld.Reset();
        block = TetrisGame.ContentManager.Load<Texture2D>("block");
    }

    protected override void Update(GameTime gameTime)
    {
        inputHelper.Update(gameTime);
        gameWorld.HandleInput(gameTime, inputHelper);
        gameWorld.Update(gameTime);
}

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Gray);
        gameWorld.Draw(gameTime, spriteBatch);
        // om zo meteen de juiste blokken te kunnen tekenen




    }
}

