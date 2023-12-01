using System.Runtime.InteropServices;

namespace OoT;

public class Handlers
{

    [OnEmulatorStart]
    public static void OnEmulatorStarted(EventEmulatorStart e)
    {
        Console.WriteLine("[OoT] Emulator Started.");
    }

}
