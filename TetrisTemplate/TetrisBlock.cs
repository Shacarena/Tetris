using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;

abstract class TetrisBlock
{
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
        block = TetrisGame.ContentManager.Load<Texture2D>("block");
    }
    
    public void RotateClockwise() // draait het blok, hiervan moeten de GetLengths nog even aangepast worden naar de juiste 0 of 1
    {
        for (int row = 0; row < shape.GetLength(0) / 2; row++) // buitenste loop voor roteren
        {
            for (int col = row; col < shape.GetLength(0) - row - 1; col++) // binnenste loop voor roteren
            {
                bool tijdelijk = shape[row, col]; // een tijdelijke 2d array maken om de nieuwe waardes op te slaap
                shape[row, col] = shape[shape.GetLength(0) - 1 - col, row]; // 'swappen' volgens de regels voor matrices
                shape[shape.GetLength(0) - 1 - col, row] = shape[shape.GetLength(0) - 1 - row, shape.GetLength(0) - 1 - col];
                shape[shape.GetLength(0) - 1 - row, shape.GetLength(0) - 1 - col] = shape[col, shape.GetLength(0) - 1 - row];
                shape[col, shape.GetLength(0) - 1 - row] = tijdelijk;
            }
        }
    }


    public void RotateCounterClockwise()
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
    public void Update(GameTime gameTime, TetrisGrid grid)
    {
        
    }


    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int breedte = 0; breedte < shape.GetLength(0); breedte++) // voor de volledige breedte een achtergrond-blokje op de grid tekenen
        {
            for (int hoogte = 0; hoogte < shape.GetLength(1); hoogte++) // voor de volledige hoogte een achtergrond-blokje op de grid tekenen
            {
                if (shape[breedte, hoogte] == true)
                {
                    spriteBatch.Draw(block, new Rectangle((position.X + breedte ) * block.Width, (position.Y + hoogte) * block.Height, block.Width, block.Height), color); // tekenen van de shape met de juiste kleur
                }
            }
        }
    }

    private List<TouchSide> GetTouchSides()
    {
        List<TouchSide> touchSides = new List<TouchSide>(); // we return this list in the end
        bool[,] colorGrid = grid.grid;
        for (int col = 0; col < shape.GetLength(0); col++) // loop through shape
        {
            for (int row = 0; row < shape.GetLength(1); row++)
            {
                if (!shape[col, row]) continue;

                var (x, y) = (position.X + col, position.Y + row);
                bool left = (x == 0) ? true : colorGrid[x - 1, y]; // if x is 0 then we cant move to the left so we assign a color to 'left' so that TouchSide.Left will be added to touchSides
                bool right = (x == grid.Width - 1) ? true : colorGrid[x + 1, y]; // idem 
                bool down = (y == grid.Height - 1) ? true : colorGrid[x, y + 1]; // idem

                if (left != false) { touchSides.Add(TouchSide.Left); }
                if (right != false) { touchSides.Add(TouchSide.Right); }
                if (down != false) { touchSides.Add(TouchSide.Down); }
            }
        }
        return touchSides;
    }
    enum TouchSide
    {
        Left,
        Right,
        Down
    }


    public void Reset(TetrisGrid grid)
    {
        this.grid = grid;

    }

    public bool Down()
    {
        if (GetTouchSides().Contains(TouchSide.Down))
        {
            grid.AddToGrid(this);
            return false;
        }
        position.Y += 1;
        return true;
    }

    public bool Left()
    {
        if (GetTouchSides().Contains(TouchSide.Left)) return false;
        position.X -= 1;

        return true;
    }

    public bool Right()
    {
        if (GetTouchSides().Contains(TouchSide.Right)) return false;
        position.X += 1;

        return true;
    }
}

class tshape : TetrisBlock
{
        public tshape()
        {
            color = Color.Purple;
            shape = new bool[3, 3] { { true, true, true }, { false, true, false }, { false, false, false } };
        }
}

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
