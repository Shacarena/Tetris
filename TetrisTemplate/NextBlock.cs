using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisTemplate
{
    internal class NextBlock
    {
        List<TetrisBlock> blocksbag; // lijst maken met alle blokken erin
        Random random;
        public NextBlock() // constructor aanmaken
        {
            blocksbag = new List<TetrisBlock>(); // nieuwe lijst
            random = new Random(); // om random te kunnen kiezen
            ResetBag(); // zodat de bag wordt gevuld
        }

        void ResetBag() // om de lege bag weer te vullen wanneer nodig
        {
            for (int i = 0; i < 2; i++) // om de lijst 2x te vullen met alle mogelijke blokken
            {
                blocksbag.Add(new ishape()); // blokje toevoegen van de ishape
                blocksbag.Add(new lshape()); // rest idem met andere vormen
                blocksbag.Add(new jshape());
                blocksbag.Add(new sshape());
                blocksbag.Add(new zshape());
                blocksbag.Add(new oshape());
                blocksbag.Add(new tshape());
            }
        }

        public TetrisBlock NewNextBlock() // om volgende blok te kunnen kiezen
        {
            if (blocksbag.Count == 0) ResetBag(); // resetten als de bag leeg is

            int randomnumb = random.Next(blocksbag.Count); // willekeurig item uit de lijst kiezen
            TetrisBlock nextBlock = blocksbag[randomnumb]; // willekeurig item uit de lijst kiezen
            blocksbag.RemoveAt(randomnumb); // gekozen nummer uit de bag verwijderen
            return nextBlock;
        }

    }
}
