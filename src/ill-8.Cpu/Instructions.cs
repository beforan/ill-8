using System;
using System.Collections.Generic;

namespace ill8.Cpu
{
    public class Instructions
    {
        private List<Action<ushort>> _instructions;

        public Cpu Cpu { get; private set; }

        public Instructions(Cpu cpu)
        {
            Cpu = cpu;

            _instructions = new List<Action<ushort>>
            {
                [0x0000] = null,
                [0x00E0] = null,
                [0x00EE] = null,

                [0x1000] = null,
                [0x2000] = null,
                [0x3000] = null,
                [0x4000] = null,
                [0x5000] = null,
                [0x6000] = null,
                [0x7000] = null,
                [0x9000] = null,
                [0xA000] = ANNN,
                [0xB000] = null,
                [0xC000] = null,
                [0xD000] = null,

                [0xE09E] = null,
                [0xE0A1] = null,

                [0x8000] = null,
                [0x8001] = null,
                [0x8002] = null,
                [0x8003] = null,
                [0x8004] = null,
                [0x8005] = null,
                [0x8006] = null,
                [0x8007] = null,
                [0x800E] = null,

                [0xF007] = null,
                [0xF00A] = null,
                [0xF015] = null,
                [0xF018] = null,
                [0xF01E] = null,
                [0xF029] = null,
                [0xF033] = null,
                [0xF055] = null,
                [0xF065] = null,
            };
        }

        public void Exec(ushort opcode)
        {
            var lookup = opcode & 0xf000;

            switch (lookup)
            {
                //0 has 2 special cases to catch
                case 0:
                    if (opcode == 0x00E0) lookup = opcode;
                    if (opcode == 0x00EE) lookup = opcode;
                    break;

                //bitmask for 8
                case 0x8000:
                    lookup = opcode & 0xf00f;
                    break;

                //bitmask for e and f
                case 0xe000:
                case 0xf000:
                    lookup = opcode & 0xf0ff;
                    break;
            }

            _instructions[lookup](opcode);
        }

        public void ANNN(ushort opcode)
        {

        }
    }
}
