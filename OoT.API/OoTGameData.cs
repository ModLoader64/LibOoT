//using Force.Crc32;

namespace OoT.API;

public static class OoTVersionPointers
{
    public static Ptr SaveContext = 0;
}

public static class OoTGameData {

    static OoTGameData(){
        //save = new WrapperSaveContext((u32)OoTVersionPointers.SaveContext);
        //byte[] empty1 = new byte[0x100000 + 4];
        //byte[] RAM = new byte[0x800000 + 4];
        //for (int i = 0; i < RAM.Length - 4; i++)
        //{
        //    RAM[i] = Memory.RAM.ReadU8((uint)i);
        //}
        //Crc32Algorithm.ComputeAndWriteToEnd(empty1);
        //var crc = new u8[0x4];
        //Array.Copy(empty1, crc, 0x4);
        //Console.WriteLine(BitConverter.ToString(crc).Replace("-", ""));
        //Console.WriteLine("Looking for empty space...");
        //for (int i = 0; i < RAM.Length; i += empty1.Length)
        //{
            
        //}
    }

    public static readonly WrapperSaveContext save;
    public static readonly Heap heap;

}
