using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// hallo
    class TetrisBlock
    {
        void RotateClockwise(bool[,] a)
        {

        }
    }
    class tshape : TetrisBlock
    {
        bool[,] t_block = new bool[3, 3] { { true, true, true }, { false, true, false }, { false, false, false } };
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
        bool[,] i_shape = new bool[4, 4] { { true, false, false, false }, { true, false, false, false }, { true, false, false, false }, { true, false, false, false } };
    }

    class zshape : TetrisBlock
    {
        bool[,] z_shape = new bool[3, 3] { { false, false, false }, { true, true, false }, { false, true, true } };
    }

    class oshape : TetrisBlock
    {
        bool[,] o_shape = new bool[2, 2] { { true, true }, { true, true } };
    }

