using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


class TetrisBlock
    {
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

    }
    class tshape : TetrisBlock
    {

        public bool[,] t_block = new bool[3, 3] { { true, true, true }, { false, true, false }, { false, false, false } };
    }

    class lshape : TetrisBlock
    {
        bool[,] l_block = new bool[3, 3] { { true, false, false }, { true, false, false }, { true, true, false } };
    }

    class sshape : TetrisBlock
    {
        bool[,] s_block = new bool[3, 3] { { false, false, false }, { false, true, true }, { true, true, false } };
    }

    class jshape : TetrisBlock
    {
        bool[,] j_shape = new bool[3, 3] { { false, false, true }, { false, false, true }, { false, true, true } };
    }

    class ishape : TetrisBlock
    {
        bool[,] i_shape = new bool[4, 4] { { true, false, false, false }, { true, false, false, false}, { true, false, false, false } , { true, false, false, false } };
    }

    class zshape : TetrisBlock
    {
        bool[,] z_shape = new bool[3, 3] { { false, false, false }, { true, true, false }, { false, true, true } };
    }

    class oshape : TetrisBlock
    {
        bool[,] o_shape = new bool[2, 2] { { true, true }, { true, true } };
    }

