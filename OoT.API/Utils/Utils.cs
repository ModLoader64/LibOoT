using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OoT.API
{
    public class Utils
    {
        public static bool isBitSet(int value, int pos)
        {
            return (value & 1 << pos) != 0;
        }

        public static void setBit(int value, int pos)
        {

        }
    }
}
