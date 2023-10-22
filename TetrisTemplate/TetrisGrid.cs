using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.ComponentModel.Design;
using System.Threading;
using TetrisTemplate;

class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    // andere fonts en nummers
    SpriteFont font;
    Song theme;
    Song rijleeg;
    Song levelup;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;
    public int level, points, multiplier;

    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }

    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }

    // Grid om te bepalen welke kleuren waar getekend moeten worden
    public Color[,] gridBezet;
    public bool[,] grid; // true en false grid

    public TetrisGrid()
    {
        // alles inladen
        // de soundeffects komen van deze website
        // https://www.myinstants.com/nl/search/?name=tetris
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        theme = TetrisGame.ContentManager.Load<Song>("theme");
        rijleeg = TetrisGame.ContentManager.Load<Song>("rijleeg");
        levelup = TetrisGame.ContentManager.Load<Song>("levelup");
        position = Vector2.Zero;
        gridBezet = new Color[Width, Height];
        grid = new bool[Width, Height];
        level = 0;
        points = 0;
    }

    public void AddToGrid(TetrisBlock currentblock) // blok aan grid toevoegen
    {
        for (int col = 0; col < currentblock.shape.GetLength(0); col++) // loop door de shape
        {
            for (int row = 0; row < currentblock.shape.GetLength(1); row++) // loop door de shape
            {
                if (currentblock.shape[col, row]) gridBezet[currentblock.position.X + col, currentblock.position.Y + row] = currentblock.color; // huidige vorm in de color grid zetten
                if (currentblock.shape[col, row]) grid[currentblock.position.X + col, currentblock.position.Y + row] = currentblock.shape[col, row]; // huidige vorm in de grid zetten
            }
        }
    }

    public bool gameover() // kijken of je gameover bent
    {
        for (int i = 0; i < Width; i++) // kijken naar de hele bovenste rij
        {
            if (grid[i, 0]) return true; // kijken of er blokken op de bovenste rij staan
        }
        return false; // wanneer er geen blokken op de bovenste rij staan
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
        for (int i = 0; i < Width; i++) // over de hele rij heen loopen
        {
            gridBezet[i, rij] = Color.Transparent; // kleurgrid op transparant zetten
            grid[i, rij] = false; // bezet-grid op false
            MediaPlayer.Play(rijleeg); // geluidseffectje afspelen
        }
    }

    private void RijOmlaag(int rij, int omlaag) // om rijden naar beneden te plaatsen
    {
        for (int r = rij; r < Height; r--)
        {
            for (int x = 0; x < Width; x++) // over de hele rij heen loopen
            {
                grid[x, r - omlaag] = grid[x, r]; // alle blokjes genoeg omlaag zetten
                grid[x, r] = false; // rij leeghalen in grid
                gridBezet[x, r] = Color.Transparent; // rij leeghalen in kleurengrid
            }
        }
    }
    public int GridLegen() // kijken of rijen vol zijn, omlaag moeten, etc
    {
        int omlaag = 0; // kijken hoeveel rijen naar beneden geplaatst moeten worden
        multiplier = 0;

        for (int rij = Height - 1; rij >= 0; rij--) // over de hele hoogte van het grid loopen om te kijken naar iedere rij
        {
            if (IsRijVol(rij))// checken of een rij leeg gemaakt moet worden, zo ja;
            {
                RijLeegmaken(rij); // rij leegmaken
                omlaag++; // programma laten weten dat de rijen erboven een extra rij omlaag moeten
                multiplier++;
                
            }
            else if (omlaag > 1) // checken of er iets omlaag moet
            {
                RijOmlaag(rij, omlaag); // rijen omlaag plaatsen
            }
        }
        points += 2 * multiplier; // punten erbij
        return omlaag; // opnieuw beginnen
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

        // voor de stand van het spel
        spriteBatch.DrawString(font, "Next block:", new Vector2(390, 20), Color.Black);
        spriteBatch.DrawString(font, "Level: " + level, new Vector2(390, 40), Color.Black);
        spriteBatch.DrawString(font, "Points: " + points, new Vector2(390, 60), Color.Black);
    }

    public void GridReset() // om de grid helemaal leeg te halen
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
    public void DrawGameOver(GameTime gameTime, SpriteBatch spriteBatch) // tekenen van de gameover state
    {
        spriteBatch.DrawString(font, "GAME OVER", new Vector2(330, 250), Color.Red);
        spriteBatch.DrawString(font, "Press space to start a new game", new Vector2(250, 280), Color.Black);
    }

}

