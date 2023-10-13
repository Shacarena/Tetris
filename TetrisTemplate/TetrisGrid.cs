using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
/// 
class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;



    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }
   
    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }

    // Grid om te bepalen welke kleuren waar getekend moeten worden
    Color[,] gridBezet;

    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        position = Vector2.Zero;
        gridBezet = new Color[Width, Height];
        Clear();
    }

    /// <summary>
    /// Draws the grid on the screen.
    /// </summary>
    /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // tekenen van de lege grid, voor de achtergrond
        for (int hoogte = 0; hoogte < Height; hoogte++) // voor de volledige hoogte een achtergrond-blokje op de grid tekenen
        {
            for (int breedte = 0; breedte < Width; breedte++) // voor de volledige breedte een achtergrond-blokje op de grid tekenen
            {
                spriteBatch.Draw(emptyCell, new Rectangle(breedte * emptyCell.Width, hoogte * emptyCell.Height, emptyCell.Width, emptyCell.Height), Color.White);
            }
        }

        // om de daadwerkelijke blokjes bovenop de achtergrond te tekenen, aan de hand van de gridBezet waarin de kleuren opgeslagen staan

        for (int hoogte = 0; hoogte < Height; hoogte++)
        {
            for (int breedte = 0; breedte < Width; breedte++)
            { 
               // spriteBatch.Draw(emptyCell, gridBezet[breedte, hoogte]);
            }
        }
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Clear()
    {
    }
}

