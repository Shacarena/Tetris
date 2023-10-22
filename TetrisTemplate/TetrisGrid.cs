using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel.Design;
using System.Threading;
using TetrisTemplate;

class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;
    SpriteFont font;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;
    public int level, points, multiplier;


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
        level = 0;
        points = 0;
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

    public bool IsRijVol(int rij) // kijken of een rij helemaal vol is
    {
        for (int i = 0; i < Width; i++) // over de hele breedte van de rij heen
        {
            if (grid[i, rij] == false) // kijken of blokken zijn gevuld
            {
                return false; // als er ook maar een blok niet is gevuld in de rij, false teruggeven
            }
        }
        return true; // alleen als alle blokken zijn gevuld
    }

    public void RijLeegmaken(int rij) // method om een rij leeg te maken
    {
        if (IsRijVol(rij)) // eerst checken of de rij daadwerkelijk helemaal vol is
        {
            for (int i = 0; i < Width; i++) // over de hele rij heen loopen
            {
                gridBezet[i, rij] = Color.Transparent; // kleurgrid op transparant zetten
                grid[i, rij] = false; // bezet-grid op false
            }
        }
    }

    private void RijOmlaag(int rij, int omlaag) // om rijden naar beneden te plaatsen
    {
        for (int x = 0; x < Width - 5; x++) // over de hele rij heen loopen
        {
            grid[x, rij + omlaag] = grid[x, rij]; // alle blokjes genoeg omlaag zetten
            gridBezet[x, rij + omlaag] = gridBezet[x, rij];
            grid[x, rij] = false; // oude rij leeghalen
            gridBezet[x, rij] = Color.Transparent;
        }
    }
    public int GridLegen() // kijken of rijen vol zijn, omlaag moeten, etc
    {
        int leeg = 0; // kijken hoeveel rijen naar beneden geplaatst moeten worden

        for (int rij = Height - 1; rij >= 0; rij--) // over de hele hoogte van het grid loopen om te kijken naar iedere rij
        {
            if (IsRijVol(rij) == true)// checken of een rij leeg gemaakt moet worden, zo ja;
            {
                RijLeegmaken(rij); // rij leegmaken
                leeg++; // GridLegen laten weten dat de rest een rij omlaag moet
                multiplier++;
                
            }
        }

        points += 2 * multiplier;

        for (int rij = Height - 1; rij >= 0; rij--) // over de hele hoogte van de grid loopen
        {
            if (leeg > 0) // kijken of er rijen omlaag moeten
            {
                RijOmlaag(rij, leeg); // rijen naar beneden plaatsen
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
        spriteBatch.DrawString(font, "Level: " + level, new Vector2(390, 40), Color.Black);
        spriteBatch.DrawString(font, "Points: " + points, new Vector2(390, 60), Color.Black);
    }

    public void GridReset()
    {
        for (int hoogte = 0; hoogte < Height; hoogte++)
        {
            for (int breedte = 0; breedte < Width; breedte++)
            {
                grid[breedte, hoogte] = false;
                gridBezet[breedte, hoogte] = Color.Transparent;
            }
        }
    }
    public void DrawGameOver(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, "GAME OVER", new Vector2(330, 250), Color.Red);
        spriteBatch.DrawString(font, "Press space to start a new game", new Vector2(250, 280), Color.Black);
    }

}

