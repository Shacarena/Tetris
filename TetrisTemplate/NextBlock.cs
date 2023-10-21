using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisTemplate
{
    internal class NextBlock
    {
        List<TetrisBlock> blocksbag;
        Random random;
        public NextBlock()
        {
            blocksbag = new List<TetrisBlock>();
            random = new Random();
            ResetBag();
        }

        void ResetBag()
        {
            for (int i = 0; i < 2; i++)
            {
                blocksbag.Add(new ishape());
                blocksbag.Add(new lshape());
                blocksbag.Add(new jshape());
                blocksbag.Add(new sshape());
                blocksbag.Add(new zshape());
                blocksbag.Add(new oshape());
                blocksbag.Add(new tshape());
            }
        }

        public TetrisBlock NewNextBlock()
        {
            if (blocksbag.Count == 0) ResetBag();

            int randomnumb = random.Next(blocksbag.Count);
            TetrisBlock nextBlock = blocksbag[randomnumb];
            blocksbag.RemoveAt(randomnumb);

            return nextBlock;
        }

    }
}
