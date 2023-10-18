using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Data;

class TetrisBlock : Game
{
    public Color color;
    public bool[,] shape;
    public float Width { get; }
    public float Height { get; }
    public int xoffset;
    public int yoffset;
    InputHelper InputHelper = new InputHelper();

    bool[,] RotateClockwise(bool[,] shape) // draait het blok, hiervan moeten de GetLengths nog even aangepast worden naar de juiste 0 of 1
    {
        for (int row = 0; row < shape.GetLength(0); row++) // buitenste loop voor roteren
        {
            for (int col = row; col < shape.GetLength(0); col++) // binnenste loop voor roteren
            {
                bool tijdelijk = shape[row, col]; // een tijdelijke 2d array maken om de nieuwe waardes op te slaap
                shape[row, col] = shape[shape.GetLength(0) - 1 - col, row]; // 'swappen' volgens de regels voor matrices
                shape[shape.GetLength(0) - 1 - col, row] = shape[shape.GetLength(0) - 1 - row, shape.GetLength(0) - 1 - col];
                shape[shape.GetLength(0) - 1 - row, shape.GetLength(0) - 1 - col] = shape[col, shape.GetLength(0) - 1 - row];
                shape[col, shape.GetLength(0) - 1 - row] = tijdelijk;
            }
        }
        return shape;
    }

    public void Update(GameTime gameTime)
    {
       // om de offset aan te passen bij keyboard input

       if (InputHelper.KeyDown(Keys.Down) == true)
        { 
            yoffset = yoffset + 1;
        }

       if (InputHelper.KeyDown(Keys.Up) == true)
        {
            xoffset = xoffset + 1;
        }
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Texture2D block;
        block = TetrisGame.ContentManager.Load<Texture2D>("block");
        Point position = new Point(xoffset * block.Width, yoffset * block.Height);


        for (int hoogte = 0; hoogte < shape.GetLength(0); hoogte++) // voor de volledige hoogte een achtergrond-blokje op de grid tekenen
        {
            for (int breedte = 0; breedte < shape.GetLength(1); breedte++) // voor de volledige breedte een achtergrond-blokje op de grid tekenen
            {
                if (shape[breedte, hoogte] == true)
                {
                    spriteBatch.Draw(block, new Rectangle(position.X + (breedte * block.Width), position.Y + (hoogte * block.Height), block.Width, block.Height), color); // tekenen van de shape met de juiste kleur
                }

            }
        }

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
