using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ill8.Cpu
{
    public class Instructions
    {
        private Random _rand = new Random();
        private List<Action<ushort>> _instructions;

        public ICpu Cpu { get; private set; }

        public Instructions(ICpu cpu)
        {
            Cpu = cpu;

            _instructions = new List<Action<ushort>>(new Action<ushort>[0xf066])
            {
                [0x0000] = _0NNN,
                [0x00E0] = _00E0,
                [0x00EE] = _00EE,

                [0x1000] = _1NNN,
                [0x2000] = _2NNN,
                [0x3000] = _3XNN,
                [0x4000] = _4XNN,
                [0x5000] = _5XY0,
                [0x6000] = _6XNN,
                [0x7000] = _7XNN,
                [0x9000] = _9XY0,
                [0xA000] = ANNN,
                [0xB000] = BNNN,
                [0xC000] = CXNN,
                [0xD000] = DXYN,

                [0xE09E] = EX9E,
                [0xE0A1] = EXA1,

                [0x8000] = _8XY0,
                [0x8001] = _8XY1,
                [0x8002] = _8XY2,
                [0x8003] = _8XY3,
                [0x8004] = _8XY4,
                [0x8005] = _8XY5,
                [0x8006] = _8XY6,
                [0x8007] = _8XY7,
                [0x800E] = _8XYE,

                [0xF007] = FX07,
                [0xF00A] = FX0A,
                [0xF015] = FX15,
                [0xF018] = FX18,
                [0xF01E] = FX1E,
                [0xF029] = FX29,
                [0xF033] = FX33,
                [0xF055] = FX55,
                [0xF065] = FX65,
            };
        }

        /// <summary>
        /// Load each register from V0 to VX with data from consecutive memory addresses, incrementing I on the way
        /// </summary>
        public void FX65(ushort opcode)
        {
            var x = opcode & 0x0f00 >> 8;
            for (var i = 0; i <= x; i++)
            {
                Cpu.V[i] = Cpu.M.Read(Cpu.I++);
            }
        }

        /// <summary>
        /// Dump the contents of each register from V0 to VX in consecutive memory addresses, incrementing I on the way
        /// </summary>
        public void FX55(ushort opcode)
        {
            var x = opcode & 0x0f00 >> 8;
            for(var i = 0; i <= x; i++)
            {
                Cpu.M.Write(Cpu.I++, Cpu.V[i]);
            }
        }

        /// <summary>
        /// Stores the value of VX as BCD in the three consecutive addresses starting at I
        /// </summary>
        public void FX33(ushort opcode)
        {
            var value = Cpu.V[opcode & 0x0f00 >> 8];
            var chars = value.ToString().PadLeft(3, '0').Split();
            for (var i = 0; i < chars.Length; i++)
            {
                Cpu.M.Write((ushort)(Cpu.I+i), byte.Parse(chars[i]));
            }
        }

        /// <summary>
        /// Set I to point to the font sprite address for the value in VX
        /// </summary>
        public void FX29(ushort opcode)
            => Cpu.I = (ushort)(Cpu.V[opcode & 0x0f00 >> 8] * 5);

        /// <summary>
        /// Add VX to I
        /// </summary>
        /// <param name="opcode"></param>
        public void FX1E(ushort opcode)
            => Cpu.I += Cpu.V[opcode & 0x0f00 >> 8];

        /// <summary>
        /// Set Sound to VX
        /// </summary>
        public void FX18(ushort opcode)
            => Cpu.Sound = Cpu.V[opcode & 0x0f00 >> 8];

        /// <summary>
        /// Set Delay to VX
        /// </summary>
        public void FX15(ushort opcode)
            => Cpu.Delay = Cpu.V[opcode & 0x0f00 >> 8];

        /// <summary>
        /// Block until a key is pressed... then store it in VX
        /// </summary>
        public void FX0A(ushort opcode)
        {
            //TODO Need a cpu flag and some bullshit event listening for this...
            //wait until we have input figured out
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set VX to Delay
        /// </summary>
        public void FX07(ushort opcode)
            => Cpu.V[opcode & 0x0f00 >> 8] = Cpu.Delay;

        /// <summary>
        /// Sets VX and VY to VY &lt;&lt; 1, stores the VY MSb in VF before the shift
        /// </summary>
        public void _8XYE(ushort opcode)
        {
            var x = opcode & 0x0f00 >> 8;
            var y = opcode & 0x00f0 >> 4;
            Cpu.V[0xf] = (byte)(new BitArray(Cpu.V[y]).Get(7) ? 1 : 0);
            Cpu.V[x] = Cpu.V[y] = (byte)(Cpu.V[y] << 1);
        }

        /// <summary>
        /// Sets VX to VY - VX, sets VF for no borrow
        /// </summary>
        public void _8XY7(ushort opcode)
        {
            Cpu.V[0xf] = 1;
            var x = opcode & 0x0f00 >> 8;
            var y = opcode & 0x00f0 >> 4;
            if (Cpu.V[y] - Cpu.V[x] < 0) Cpu.V[0xf] = 0;
            Cpu.V[x] = (byte)(Cpu.V[y] - Cpu.V[x]);
        }

        /// <summary>
        /// Sets VX and VY to VY >> 1, stores the VY LSb in VF before the shift
        /// </summary>
        public void _8XY6(ushort opcode)
        {
            var x = opcode & 0x0f00 >> 8;
            var y = opcode & 0x00f0 >> 4;
            Cpu.V[0xf] = (byte)(new BitArray(Cpu.V[y]).Get(0) ? 1 : 0);
            Cpu.V[x] = Cpu.V[y] = (byte)(Cpu.V[y] >> 1);
        }

        /// <summary>
        /// Subtracts VY from VX, sets VF for no borrow
        /// </summary>
        public void _8XY5(ushort opcode)
        {
            Cpu.V[0xf] = 1;
            var x = opcode & 0x0f00 >> 8;
            var y = opcode & 0x00f0 >> 4;
            if (Cpu.V[x] - Cpu.V[y] < 0) Cpu.V[0xf] = 0;
            Cpu.V[x] -= Cpu.V[y];
        }

        /// <summary>
        /// Add VY to VX, sets VF for carry
        /// </summary>
        public void _8XY4(ushort opcode)
        {
            Cpu.V[0xf] = 0;
            var x = opcode & 0x0f00 >> 8;
            var y = opcode & 0x00f0 >> 4;
            if (Cpu.V[x] + Cpu.V[y] > 0xff) Cpu.V[0xf] = 1;
            Cpu.V[x] += Cpu.V[y];
        }

        /// <summary>
        /// Set VX to VX^VY
        /// </summary>
        public void _8XY3(ushort opcode)
        {
            var x = opcode & 0x0f00 >> 8;
            Cpu.V[x] = (byte)(Cpu.V[x] ^ Cpu.V[opcode & 0x00f0 >> 4]);
        }

        /// <summary>
        /// Set VX to VX&VY
        /// </summary>
        public void _8XY2(ushort opcode)
        {
            var x = opcode & 0x0f00 >> 8;
            Cpu.V[x] = (byte)(Cpu.V[x] & Cpu.V[opcode & 0x00f0 >> 4]);
        }

        /// <summary>
        /// Set VX to VX|VY
        /// </summary>
        public void _8XY1(ushort opcode)
        {
            var x = opcode & 0x0f00 >> 8;
            Cpu.V[x] = (byte)(Cpu.V[x] | Cpu.V[opcode & 0x00f0 >> 4]);
        }

        /// <summary>
        /// Set VX to VY
        /// </summary>
        public void _8XY0(ushort opcode)
            => Cpu.V[opcode & 0x0f00 >> 8] = Cpu.V[opcode & 0x00f0 >> 4];

        /// <summary>
        /// Skip the next instruction if the key stored at VX is not pressed
        /// </summary>
        public void EXA1(ushort opcode)
        {
            if (!Cpu.Keys[Cpu.V[opcode & 0x0f00 >> 8]]) Cpu.PC += 2;
        }

        /// <summary>
        /// Skip the next instruction if the key stored at VX is pressed
        /// </summary>
        public void EX9E(ushort opcode)
        {
            if (Cpu.Keys[Cpu.V[opcode & 0x0f00 >> 8]]) Cpu.PC += 2;
        }

        /// <summary>
        /// Draw a sprite at VX, VY with height N pixels.
        /// All sprites are 9 pixels wide.
        /// The sprite is read starting from I.
        /// I is not updated.
        /// VF is set to 1 if any pixels are unset by this operation.
        /// </summary>
        public void DXYN(ushort opcode)
        {
            var x = Cpu.V[(opcode & 0x0f00) >> 8];
            var y = Cpu.V[(opcode & 0x00f0) >> 4];
            var n = opcode & 0x000f;

            Cpu.V[0xf] = 0;

            for (var dy = 0; dy < n; dy++)
            {
                var row = Cpu.M.Read((ushort)(Cpu.I + dy));
                var rowBits = new BitArray(new[] { row });

                for (var dx = 0; dx < 8; dx++)
                {
                    var pixel = rowBits[dx];
                    if (pixel)
                    {
                        var index = x + dx + ((y + dy) * 64); //TODO unhardcode display width (64)
                        if (Cpu.Vram[index]) Cpu.V[0xf] = 1;
                        Cpu.Vram[index] ^= pixel; //XOR
                    }
                }
            }

            //TODO set draw flag?
        }

        /// <summary>
        /// Set VX to rand() & NN, where rand() is a random byte value
        /// </summary>
        public void CXNN(ushort opcode)
        {
            var x = (opcode & 0x0f00) >> 8;
            var nn = opcode & 0x00ff;
            Cpu.V[x] = (byte)(_rand.Next(256) & nn);
        }

        /// <summary>
        /// Jump to an address V0 + NNN
        /// </summary>
        public void BNNN(ushort opcode)
            => Cpu.PC = (ushort)((opcode & 0x0fff) + Cpu.V[0]);

        /// <summary>
        /// Set I = NNN
        /// </summary>
        public void ANNN(ushort opcode)
            => Cpu.I = (ushort)(opcode & 0x0fff);

        /// <summary>
        /// Skip the next op (Increment PC by 2) if VX != VY
        /// </summary>
        public void _9XY0(ushort opcode)
        {
            var x = (opcode & 0x0f00) >> 8;
            var y = (opcode & 0x00f0) >> 4;
            if (Cpu.V[x] != Cpu.V[y]) Cpu.PC += 2;
        }

        /// <summary>
        /// Add NN to VX, no carry
        /// </summary>
        public void _7XNN(ushort opcode)
        {
            var x = (opcode & 0x0f00) >> 8;
            var nn = opcode & 0x00ff;
            Cpu.V[x] += (byte)nn;
        }

        /// <summary>
        /// Set VX to NN
        /// </summary>
        public void _6XNN(ushort opcode)
        {
            var x = (opcode & 0x0f00) >> 8;
            var nn = opcode & 0x00ff;
            Cpu.V[x] = (byte)nn;
        }

        /// <summary>
        /// Skip the next op (Increment PC by 2) if VX == VY
        /// </summary>
        public void _5XY0(ushort opcode)
        {
            var x = (opcode & 0x0f00) >> 8;
            var y = (opcode & 0x00f0) >> 4;
            if (Cpu.V[x] == Cpu.V[y]) Cpu.PC += 2;
        }

        /// <summary>
        /// Skip the next op (Increment PC by 2) if VX != NN
        /// </summary>
        public void _4XNN(ushort opcode)
        {
            var x = (opcode & 0x0f00) >> 8;
            var nn = opcode & 0x00ff;
            if (Cpu.V[x] != nn) Cpu.PC += 2;
        }

        /// <summary>
        /// Skip the next op (Increment PC by 2) if VX == NN
        /// </summary>
        public void _3XNN(ushort opcode)
        {
            var x = (opcode & 0x0f00) >> 8;
            var nn = opcode & 0x00ff;
            if (Cpu.V[x] == nn) Cpu.PC += 2;
        }

        /// <summary>
        /// Call subroutine at an address
        /// </summary>
        public void _2NNN(ushort opcode)
        {
            Cpu.Stack[Cpu.SP++] = Cpu.PC;
            Cpu.PC = (ushort)(opcode & 0x0fff);
        }

        /// <summary>
        /// Jump to an address
        /// </summary>
        public void _1NNN(ushort opcode)
            => Cpu.PC = (ushort)(opcode & 0x0fff);

        /// <summary>
        /// Return from a subroutine
        /// </summary>
        public void _00EE(ushort opcode)
            => Cpu.PC = Cpu.Stack[Cpu.SP--];

        /// <summary>
        /// RCA 1802 call
        /// </summary>
        public void _0NNN(ushort opcode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clear the display i.e. set all Vram pixels to off
        /// </summary>
        public void _00E0(ushort opcode)
            => Cpu.Vram.SetAll(false); //TODO set draw flag

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
    }
}
