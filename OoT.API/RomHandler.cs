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

    public class Handlers
    {

        [Signature("03 20 F8 09 00 00 00 00 3C 0E 80 12 91 CE 12 12")]
        private static Ptr entrypoint = 0;

        [OnEmulatorStart]
        public static void OnEmulatorStarted(EventEmulatorStart e)
        {
            Console.WriteLine("[OoT] Emulator Started.");
        }

        [EventHandler("OnRomLoaded")]
        public static void OnRomLoaded(EventRomLoaded e)
        {
            var header = new u8[0x40];
            Array.Copy(e.rom, header, 0x40);
            GCHandle pinnedBytes = GCHandle.Alloc(header, GCHandleType.Pinned);
            N64RomHeader romHeader = (N64RomHeader)Marshal.PtrToStructure(pinnedBytes.AddrOfPinnedObject(), typeof(N64RomHeader));
            pinnedBytes.Free();
            if (romHeader.id == "CZL" && romHeader.version == 0)
            {
                Console.WriteLine("OoT 1.0 NTSC");
                // 1.0
                OoTVersionPointers.SaveContext = (Ptr)0x8011A5D0;
            }
        }
    }
}
