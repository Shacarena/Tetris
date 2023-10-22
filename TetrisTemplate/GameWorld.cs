using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TetrisTemplate;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading.Tasks.Dataflow;
using Microsoft.Xna.Framework.Media;
using System.Reflection.Metadata;

class GameWorld
{
    private GameState _gameState = GameState.Playing; // beginnen met een spel
    enum GameState // namen van alle gamestates
    {
        Playing,
        GameOver
    }

    // variabelen definieren
    public static Random Random { get { return random; } }
    static Random random;
    NextBlock newrandomblock;
    TetrisGame game;
    Song theme;
    Song rijleeg;
    Song levelup;
    SpriteFont font;
    GameState gameState;

    /// <summary>
    /// The main grid of the game.
    /// </summary>
    public TetrisGrid grid;
    TetrisBlock currentblock, nextblock;
    public long lastupdate;
    public long timer = (long)0.2;
    bool gameover;

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Playing;
        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont"); // font laden
        grid = new TetrisGrid();
        newrandomblock = new NextBlock();
        theme = TetrisGame.ContentManager.Load<Song>("theme"); // geluiden laden
        rijleeg = TetrisGame.ContentManager.Load<Song>("rijleeg");
        levelup = TetrisGame.ContentManager.Load<Song>("levelup");
        gameover = false;
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper) // alle input die nodig is
    {
        if (inputHelper.KeyPressed(Keys.Down) && currentblock.position.Y + currentblock.shape.GetLength(1) < grid.Height) { currentblock.Down(); } // om blok omlaag te bewegen
        if (inputHelper.KeyPressed(Keys.Left)) { currentblock.Left(); } // blok opzij
        if (inputHelper.KeyPressed(Keys.Space)) { currentblock.Omlaag(); } // direct naar beneden
        if (inputHelper.KeyPressed(Keys.Right)) { currentblock.Right(); } // blok opzij
        if (inputHelper.KeyPressed(Keys.D)) { currentblock.RotateClockwise(); } // blok roteren
        if (inputHelper.KeyPressed(Keys.A)) { currentblock.RotateCounterClockwise(); } // blok roteren

        if (_gameState == GameState.GameOver) // zorgen dat het alleen kan wanneer je GameOver bent
        {
            if (inputHelper.KeyPressed(Keys.Space)) { _gameState = GameState.Playing; } // spacebar indrukken zorgt voor nieuwe game
            {
                grid.GridReset();
                Reset();
            }
        }
    }
    public void Reset() // resetten aan het begin
    {
        currentblock = newrandomblock.NewNextBlock();
        nextblock = newrandomblock.NewNextBlock();
        lastupdate = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
        currentblock.Reset(grid);
    }
    public void Reset2() // resetten voor later in de game
    {
        currentblock = nextblock;
        nextblock = newrandomblock.NewNextBlock();
        currentblock.Reset(grid);
    }
    public void Update(GameTime gameTime)
    {
        
        if (grid.points > 0 && grid.points % 10 == 0) // levelup 
        {
            grid.level++; // level omhoog
            if (timer > 0.2) timer -= (long)0.2; // maakt blok sneller
            MediaPlayer.Play(levelup); // soundeffect levelup
        }

        if (DateTimeOffset.Now.ToUnixTimeSeconds() - lastupdate > timer) // timer
        {
            lastupdate = DateTimeOffset.Now.ToUnixTimeSeconds(); // kijken wanneer laatste timerupdate was
            bool wentdown = currentblock.Down(); // kijken of blok omlaag is gegaan
            if (!wentdown) Reset2(); // nieuw blok spawnen als blok omlaag is geplaatst
        }

        grid.GridLegen(); // om te checken of er iets in de grid moet gebeuren
        if (grid.gameover()) _gameState = GameState.GameOver; // naar gameover state
        
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        if (_gameState == GameState.Playing) // om de juiste gamestate te tekenen
        {
            grid.Draw(gameTime, spriteBatch); // grid tekenen
            currentblock.Draw(gameTime, spriteBatch); // huidige blok over de grid tekenen
            nextblock.DrawNext(gameTime, spriteBatch); // volgende blok aan de zijkant tekenen
        }

        if (_gameState == GameState.GameOver) // voor de gameover tekenen
        {
            grid.DrawGameOver(gameTime, spriteBatch);  // tekent de gameover strings
        }
        spriteBatch.End();
    }

}
