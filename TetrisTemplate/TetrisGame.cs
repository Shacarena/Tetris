using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

class TetrisGame : Game
{
    SpriteBatch spriteBatch;
    InputHelper inputHelper;
    public GameWorld gameWorld;
    Texture2D block;
    Song theme;
    Song rijleeg;
    Song levelup;
    public GameWorld gameworld { get { return gameWorld; } }

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
        
        
}

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        // create and reset the game world
        gameWorld = new GameWorld();
        gameWorld.Reset();
        // laden van alle content die nodig is en themesong afspelen
        block = ContentManager.Load<Texture2D>("block");
        theme = Content.Load<Song>("theme");
        rijleeg = Content.Load<Song>("rijleeg");
        levelup = Content.Load<Song>("levelup");
        MediaPlayer.Play(theme);
    }

    protected override void Update(GameTime gameTime) // alles updaten uit alle classes
    {
        inputHelper.Update(gameTime);
        gameWorld.HandleInput(gameTime, inputHelper);
        gameWorld.Update(gameTime);     
    }

    protected override void Draw(GameTime gameTime) // tekenen van de gameworld en achtergrondkleur
    {
        GraphicsDevice.Clear(Color.Gray);
        gameWorld.Draw(gameTime, spriteBatch);
    }
}

