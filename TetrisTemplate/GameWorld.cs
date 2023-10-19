using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TetrisTemplate;


class GameWorld
{

    enum GameState
    {
        Playing,
        GameOver
    }


    public static Random Random { get { return random; } }
    static Random random;
    NextBlock nextBlock;
    SpriteFont font;

    GameState gameState;

    /// <summary>
    /// The main grid of the game.
    /// </summary>
    public TetrisGrid grid;
    TetrisBlock currentblock;
    

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Playing;
        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        grid = new TetrisGrid();
        nextBlock = new NextBlock();
}

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Down)) { currentblock.Down(); }
        if (inputHelper.KeyPressed(Keys.Left)) { currentblock.Left(); }
        if (inputHelper.KeyPressed(Keys.Right)) { currentblock.Right(); }
        if (inputHelper.KeyPressed(Keys.D)) { currentblock.RotateClockwise(); }
        
    }

    public void Update(GameTime gameTime)
    {
        Color[,] gridbezet = new Color[grid.Width, grid.Height];
        currentblock.Update(gameTime); // not sure met deze

    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        grid.Draw(gameTime, spriteBatch);
        currentblock.Draw(gameTime, spriteBatch);

        spriteBatch.End();
    }

    public void Reset()
    {
        currentblock = nextBlock.NewNextBlock();
        currentblock.Start(grid);
    }

}
