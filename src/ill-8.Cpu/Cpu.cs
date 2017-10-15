using System.Collections;

namespace ill8.Cpu
{
    public class Cpu : ICpu
    {
        /// <summary>
        /// General purpose registers
        /// </summary>
        public byte[] V { get; set; } = new byte[16];

        /// <summary>
        /// 12-bit index register
        /// </summary>
        public ushort I { get; set; }

        /// <summary>
        /// 12-bit Program Counter
        /// </summary>
        public ushort PC { get; set; } = 0x200;

        /// <summary>
        /// 4k RAM
        /// </summary>
        public IMemory M { get; private set; }

        /// <summary>
        /// 64 x 32 pixel display, with on or off state only
        /// </summary>
        public BitArray Vram { get; set; } = new BitArray(64 * 32); //TODO make size configurable? maybe move to own class?
        //own class could allow palettes? might be easier for renderers?

        /// <summary>
        /// 60Hz Delay Timer register
        /// </summary>
        public byte Delay { get; set; }

        /// <summary>
        /// 60Hz Sound Timer register
        /// </summary>
        public byte Sound { get; set; }

        /// <summary>
        /// Stack for jump instructions
        /// </summary>
        public ushort[] Stack { get; set; } = new ushort[16];

        /// <summary>
        /// Stack pointer
        /// </summary>
        public ushort SP { get; set; }

        /// <summary>
        /// Key state
        /// </summary>
        public bool[] Keys { get; set; } = new bool[16];

        public Instructions Instructions { get; private set; }

        public Cpu(IMemory m)
        {
            M = m;
            Instructions = new Instructions(this);
        }

        public void Tick()
        {
            var opcode = M.ReadWord(PC);
            PC += 2; //jump forward the op length here, so it doesn't matter if an op then modifies PC

            Instructions.Exec(opcode);

            //Timers
            if (Delay > 0) Delay--;
            if (Sound > 0) Sound--;
        }
    }
}
