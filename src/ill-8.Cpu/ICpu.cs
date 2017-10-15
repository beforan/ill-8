using System.Collections;

namespace ill8.Cpu
{
    public interface ICpu
    {
        byte Delay { get; set; }
        ushort I { get; set; }
        Instructions Instructions { get; }
        bool[] Keys { get; set; }
        IMemory M { get; }
        ushort PC { get; set; }
        byte Sound { get; set; }
        ushort SP { get; set; }
        ushort[] Stack { get; set; }
        byte[] V { get; set; }
        BitArray Vram { get; set; }

        void Tick();
    }
}