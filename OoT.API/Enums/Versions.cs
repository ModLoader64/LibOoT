using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OoT.API.Enums
{
    public enum ROM_VERSIONS
    {
        N0 = 0x00,
        GAMECUBE = 0x0f,
        REV_A = 0x01,
        REV_B = 0x02,
    }

    public static class RomRegions
    {
        public const string DEBUG_OOT = "NZL";
        public const string NTSC_OOT = "CZL";
        public const string NTSC_MM = "NZS";
    }
}
