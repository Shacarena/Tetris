using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


class TetrisBlock
    {
        bool[,] RotateClockwise(bool[,] shape) // draait het blok
        {
        for (int row = 0; row < shape.Length; row++) // buitenste loop voor roteren
        {
            for(int col = row; col < shape.Length; col++) // binnenste loop voor roteren
            {
                bool tijdelijk = shape[row,col]; // een tijdelijke 2d array maken om de nieuwe waardes op te slaap
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
        int[,] t_block = new int[3, 3] { { 1, 1, 1 }, { 0, 1, 0 }, { 0, 0, 0 } };
    }

    class lshape : TetrisBlock
    {
        int[,] l_block = new int[3, 3] { { 2, 0, 0}, { 2, 0, 0 }, { 2, 2, 0 } };
    }

    class sshape : TetrisBlock
    {
        int[,] s_block = new int[3, 3] { { 0, 0, 0 }, { 0, 3, 3 }, { 3, 3, 0 } };
    }

    class jshape : TetrisBlock
    {
        int[,] j_shape = new int[3, 3] { { 0, 0, 4 }, { 0, 0, 4}, { 0, 4, 4 } };
    }

    class ishape : TetrisBlock
    {
        int[,] i_shape = new int[3, 3] { { 5, 0, 0 }, { 5, 0, 0, }, { 5, 0, 0 } };
    }

    class zshape : TetrisBlock
    {
        int[,] z_shape = new int[3, 3] { { 0, 0, 0 }, { 6, 6, 0 }, { 0, 6, 6} };
    }

    class oshape : TetrisBlock
    {
        int[,] o_shape = new int[2, 2] { { 7, 7 }, { 7, 7} };
    }

