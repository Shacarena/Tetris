﻿using Microsoft.Xna.Framework;
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
    NextBlock newrandomblock;

    SpriteFont font;

    GameState gameState;

    /// <summary>
    /// The main grid of the game.
    /// </summary>
    public TetrisGrid grid;
    TetrisBlock currentblock, nextblock;
    

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Playing;
        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        grid = new TetrisGrid();
        newrandomblock = new NextBlock();
}

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Down) && currentblock.position.Y + currentblock.shape.GetLength(1) < grid.Height) { currentblock.Down(); }
        if (inputHelper.KeyPressed(Keys.Left)) { currentblock.Left(); }
        if (inputHelper.KeyPressed(Keys.Right)) { currentblock.Right(); }
        if (inputHelper.KeyPressed(Keys.D)) { currentblock.RotateClockwise(); }
        if (inputHelper.KeyPressed(Keys.A)) { currentblock.RotateCounterClockwise(); }
        if (inputHelper.KeyPressed(Keys.F)) { grid.AddToGrid(currentblock); Reset(); }
    }

    public void Update(GameTime gameTime)
    {
        
        currentblock.Update(gameTime, grid); // not sure met deze
        for (int breedte = 0; breedte < currentblock.shape.GetLength(0); breedte++) // voor de volledige hoogte een achtergrond-blokje op de grid tekenen
        {
            for (int hoogte = 0; hoogte < currentblock.shape.GetLength(1); hoogte++) // voor de volledige breedte een achtergrond-blokje op de grid tekenen
            {
                if (grid.grid[breedte, hoogte + 1] == true)
                {
                    grid.AddToGrid(currentblock);
                    Reset();

                }
            }
        }

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
        currentblock = newrandomblock.NewNextBlock();
        nextblock = newrandomblock.NewNextBlock();
        currentblock.Reset(grid);
    }

}
