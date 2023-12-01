using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OoT.API
{
    public unsafe struct N64RomHeader
    {
        public fixed u8 crc1[0x10];
        public fixed u8 crc2[0x10];
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1B)]
        public string goodname;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x03)]
        public string id;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x01)]
        public string lang;
        public u8 version;
    }

}
