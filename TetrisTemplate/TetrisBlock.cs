using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using TetrisTemplate;

abstract class TetrisBlock
{
    // alles definieren
    Texture2D block;
    public Color color;
    public bool[,] shape;
    public float Width { get; }
    public float Height { get; }
    public int xoffset;
    public int yoffset;
    InputHelper InputHelper = new InputHelper();
    public Point position = new Point(4, 0);
    TetrisGrid grid;

    protected TetrisBlock() 
    {
        block = TetrisGame.ContentManager.Load<Texture2D>("block"); // sprite inladen
    }
    
    public void RotateClockwise() // draait het blok volgens de regels voor matrices roteren, GetLength 0 of 1 maakt niet uit want alle blokken zijn vierkant
    {
        if (position.X + shape.GetLength(0) < grid.Width + 1 && position.X > -1) // kijken of blok mag draaien
        {
            for (int row = 0; row < shape.GetLength(0) / 2; row++) // buitenste loop voor roteren (breedte)
            {
                for (int col = row; col < shape.GetLength(0) - row - 1; col++) // binnenste loop voor roteren (hoogte)
                {
                    bool tijdelijk = shape[row, col]; // een tijdelijke 2d array maken om de nieuwe waardes op te slaap
                    shape[row, col] = shape[shape.GetLength(0) - 1 - col, row]; // 'swappen' volgens de regels voor matrices
                    shape[shape.GetLength(0) - 1 - col, row] = shape[shape.GetLength(0) - 1 - row, shape.GetLength(0) - 1 - col];
                    shape[shape.GetLength(0) - 1 - row, shape.GetLength(0) - 1 - col] = shape[col, shape.GetLength(0) - 1 - row];
                    shape[col, shape.GetLength(0) - 1 - row] = tijdelijk; // nieuwe vorm teruggeven
                }
            }
        }
    }
    public void RotateCounterClockwise() // zelfde ongein als hierboven maar dan andersom
    {
        if (position.X + shape.GetLength(0) < grid.Width + 1 && position.X > -1)
        {
            for (int row = 0; row < shape.GetLength(1) / 2; row++)
            {
                for (int col = row; col < shape.GetLength(0) - row - 1; col++)
                {
                    bool tijdelijk = shape[row, col];
                    shape[row, col] = shape[col, shape.GetLength(1) - 1 - row];
                    shape[col, shape.GetLength(1) - 1 - row] = shape[shape.GetLength(1) - 1 - row, shape.GetLength(1) - 1 - col];
                    shape[shape.GetLength(1) - 1 - row, shape.GetLength(1) - 1 - col] = shape[shape.GetLength(1) - 1 - col, row];
                    shape[shape.GetLength(1) - 1 - col, row] = tijdelijk;
                }
            }
        }
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int breedte = 0; breedte < shape.GetLength(0); breedte++) // over de breedte loopen
        {
            for (int hoogte = 0; hoogte < shape.GetLength(1); hoogte++) // over de hoogte loopen
            {
                if (shape[breedte, hoogte] == true) // checken of blok getekend moet worden
                {
                    spriteBatch.Draw(block, new Rectangle((position.X + breedte ) * block.Width, (position.Y + hoogte) * block.Height, block.Width, block.Height), color); // tekenen van de shape met de juiste kleur
                }
            }
        }
    }
    public void DrawNext(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int breedte = 0; breedte < shape.GetLength(0); breedte++) // over de breedte loopen
        {
            for (int hoogte = 0; hoogte < shape.GetLength(1); hoogte++) // over de hoogte loopen
            {
                if (shape[breedte, hoogte] == true) // checken of blok getekend moet worden
                {
                    spriteBatch.Draw(block, new Rectangle((position.X + breedte + 9) * block.Width, (position.Y + hoogte + 3) * block.Height, block.Width, block.Height), color); // tekenen van de shape met de juiste kleur
                }
            }
        }
    }

    private List<Zijkanten> MagBewegen()
    {
        List<Zijkanten> Bewegen = new List<Zijkanten>(); // lijst aanmaken om alles in te plaatsen
        bool[,] colorGrid = grid.grid;
        for (int col = 0; col < shape.GetLength(0); col++) // door de shape heen loopen
        {
            for (int row = 0; row < shape.GetLength(1); row++) // door de shape heen loopen
            {
                if (!shape[col, row]) continue; // kijken of de shape op dit punt getekend moet worden

                var (x, y) = (position.X + col, position.Y + row); 
                bool left = (x == 0) ? true : colorGrid[x - 1, y]; // kan niet naar links bewegen, een kleur geven zodat deze toegevoegd kan worden aan de lijst
                bool right = (x == grid.Width - 1) ? true : colorGrid[x + 1, y]; // zelfde
                bool down = (y == grid.Height - 1) ? true : colorGrid[x, y + 1]; // zelfde

                if (left != false) { Bewegen.Add(Zijkanten.Left); } // toevoegen aan de lijst wanneer het niet past
                if (right != false) { Bewegen.Add(Zijkanten.Right); }
                if (down != false) { Bewegen.Add(Zijkanten.Down); }
            }
        }
        return Bewegen; // lijst teruggeven
    }
    enum Zijkanten // om te bepalen welke opties het blok niet heen kan
    {
        Left,
        Right,
        Down
    }
    public void Reset(TetrisGrid grid) // om het grid te resetten
    {
        this.grid = grid;
    }
    public void Omlaag() // om blok in 1x omlaag te plaatsen
    {
        while (Down()) ;
    }
    
    public bool Down() // om blok omlaag te plaatsen
    {
        if (MagBewegen().Contains(Zijkanten.Down)) // kijken of blok omlaag mag bewegen
        {
            grid.AddToGrid(this); // toevoegen aan grid
            return false;
        }
        position.Y += 1; // blok omlaag bewegen wanneer het mag
        return true;
    }

    public bool Left() // blok naar links plaatsen
    {
        if (MagBewegen().Contains(Zijkanten.Left)) return false; // kijken of blok naar links mag bewegen
        position.X -= 1; // blok naar links bewegen
        return true;
    }

    public bool Right() // blok naar rechts plaatsen
    {
        if (MagBewegen().Contains(Zijkanten.Right)) return false; // kijken of blik naar rechts plaatsen
        position.X += 1; // blok naar rechts plaatsen
        return true;
    }
}

class tshape : TetrisBlock // alle subclasses voor alle blokken
{
        public tshape()
        {
            color = Color.Purple; // kleur van het blok
            shape = new bool[3, 3] { { true, true, true }, { false, true, false }, { false, false, false } }; // standaardvorm van het blok
        }
}

// voor deze allemaal hetzelfde als hierboven

class lshape : TetrisBlock
{
    public lshape()
    {
        shape = new bool[3, 3] { { true, false, false }, { true, false, false }, { true, true, false } };
        color = Color.Orange;
    }
}

class sshape : TetrisBlock
{
    public sshape()
    {
        shape = new bool[3, 3] { { false, false, false }, { false, true, true }, { true, true, false } };
        color = Color.Green;
    }
}

class jshape : TetrisBlock
{
    public jshape()
    {
        shape = new bool[3, 3] { { false, false, true }, { false, false, true }, { false, true, true } };
        color = Color.DarkBlue;
    }
}

class ishape : TetrisBlock
{
    public ishape()
    {
        shape = new bool[4, 4] { { true, false, false, false }, { true, false, false, false }, { true, false, false, false }, { true, false, false, false } };
        color = Color.LightBlue;
    }
}

class zshape : TetrisBlock
{
   public zshape()
    {
        shape = new bool[3, 3] { { false, false, false }, { true, true, false }, { false, true, true } };
        color = Color.Red;
    }
}

class oshape : TetrisBlock
{
    public oshape()
    {
        shape = new bool[2, 2] { { true, true }, { true, true } };
        color = Color.Yellow;
    }
}
