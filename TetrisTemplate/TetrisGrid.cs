using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel.Design;
using TetrisTemplate;

class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;
    SpriteFont font;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;



    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }

    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }

    // Grid om te bepalen welke kleuren waar getekend moeten worden
    public Color[,] gridBezet;
    public bool[,] grid;

    

    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        position = Vector2.Zero;
        gridBezet = new Color[Width, Height];
        grid = new bool[Width, Height];
    }
    public void AddToGrid(TetrisBlock currentblock)
    {
        for (int col = 0; col < currentblock.shape.GetLength(0); col++) // loop through shape
        {
            for (int row = 0; row < currentblock.shape.GetLength(1); row++)
            {
                if (currentblock.shape[col, row]) gridBezet[currentblock.position.X + col, currentblock.position.Y + row] = currentblock.color;
                if (currentblock.shape[col, row]) grid[currentblock.position.X + col, currentblock.position.Y + row] = currentblock.shape[col, row];
            }
        }
    }

    public bool IsRijVol(int rij)
    {
        for (int i = 0; i < Width; i++)
        {
            if (grid[i, rij] == false)
            {
                return false;
            }
        }
        return true;
    }

    public void RijLeegmaken(int rij)
    {
        if (IsRijVol(rij))
        {
            for (int i = 0; i < Width; i++)
            {
                gridBezet[i, rij] = Color.Transparent;
                grid[i, rij] = false;
            }
        }
    }

    private void RijOmlaag(int a, int omlaag)
   {
        for (int i = Height; i < Height; i--)
        {
            gridBezet[a, i + omlaag] = gridBezet[a, i];
            grid[a, i + omlaag] = grid[a, i];
            gridBezet[a, i] = Color.Transparent;
            grid[a, i] = false;
        }
    }

    public int GridLegen()
    {
        int leeg = 0;
            for (int rij = Height - 1; rij >= 0; rij--)
            {
                if (IsRijVol(rij) == true) ;
                {
                    RijLeegmaken(rij);
                    leeg++;
                }
                if (leeg > 0)
                {
                    RijOmlaag(rij, leeg);
                }
            }
        return leeg;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // tekenen van de lege grid, voor de achtergrond
        for (int hoogte = 0; hoogte < Height; hoogte++) // voor de volledige hoogte een achtergrond-blokje op de grid tekenen
        {
            for (int breedte = 0; breedte < Width; breedte++) // voor de volledige breedte een achtergrond-blokje op de grid tekenen
            {
                spriteBatch.Draw(emptyCell, new Rectangle(breedte * emptyCell.Width, hoogte * emptyCell.Height, emptyCell.Width, emptyCell.Height), Color.White); // tekenen van lege grid
                spriteBatch.Draw(emptyCell, new Rectangle(breedte * emptyCell.Width, hoogte * emptyCell.Height, emptyCell.Width, emptyCell.Height), gridBezet[breedte, hoogte]); // tekenen van de geplaatste blokjes
            }
        }

        spriteBatch.DrawString(font, "Next block:", new Vector2(390, 20), Color.Black);
    }
}

