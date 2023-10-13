using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

class TetrisBlock : Game
{


    protected Vector2 Start;

    public TetrisBlock()
    {
        Start = new Vector2(Width/2, Height/2);
        
    }

    public float Width { get; }
    public float Height { get; }

    bool[,] RotateClockwise(bool[,] shape) // draait het blok
    {
        for (int row = 0; row < shape.Length; row++) // buitenste loop voor roteren
        {
            for (int col = row; col < shape.Length; col++) // binnenste loop voor roteren
            {
                bool tijdelijk = shape[row, col]; // een tijdelijke 2d array maken om de nieuwe waardes op te slaap
                shape[row, col] = shape[shape.Length - 1 - col, row]; // 'swappen' volgens de regels voor matrices
                shape[shape.Length - 1 - col, row] = shape[shape.Length - 1 - row, shape.Length - 1 - col];
                shape[shape.Length - 1 - row, shape.Length - 1 - col] = shape[col, shape.Length - 1 - row];
                shape[col, shape.Length - 1 - row] = tijdelijk;
            }
        }
        return shape;
    }
   

    // ??????????

    }
class tshape : TetrisBlock
    {
        public bool[,] shape = new bool[3, 3] { { true, true, true }, { false, true, false }, { false, false, false } };
        public Color color = Color.Purple;
    }

    class lshape : TetrisBlock
    {
        public bool[,] shape = new bool[3, 3] { { true, false, false }, { true, false, false }, { true, true, false } };
        public Color color = Color.Orange;
    }

    class sshape : TetrisBlock
    {
        public bool[,] shape = new bool[3, 3] { { false, false, false }, { false, true, true }, { true, true, false } };
        public Color color = Color.Green;
    }

    class jshape : TetrisBlock
    {
        public bool[,] shape = new bool[3, 3] { { false, false, true }, { false, false, true }, { false, true, true } };
        public Color color = Color.DarkBlue;
    }

    class ishape : TetrisBlock
    {
        public bool[,] shape = new bool[4, 4] { { true, false, false, false }, { true, false, false, false}, { true, false, false, false } , { true, false, false, false } };
        public Color color = Color.LightBlue;
    }

    class zshape : TetrisBlock
    {
        public bool[,] shape = new bool[3, 3] { { false, false, false }, { true, true, false }, { false, true, true } };
        public Color color = Color.Red;
    }

    class oshape : TetrisBlock
    { 
        public bool[,] shape = new bool[2, 2] { { true, true }, { true, true } };
        public Color color = Color.Yellow;
    }

