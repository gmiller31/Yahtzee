using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    class Die
    {
        int Face = 1;
        bool Keep = false;
        
        
        public void Roll(int rnd)
        {
            Face = rnd;

        }

        public int getFace()
        {
            return Face;
        }

        public void setKeep()
        {
            Keep = !Keep;
        }

        public bool getKeep()
        {
            return Keep;
        }
    }

}
