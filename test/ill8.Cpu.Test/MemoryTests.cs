using System;
using Xunit;

namespace ill8.Cpu.Test
{
    public class MemoryTests
    {
        private readonly Memory _m;
        public MemoryTests()
        {
            _m = new Memory();
        }

        [Fact]
        public void ConstructorInitialises4KRam()
        {
            _m.Read(0xfff);

            //No formal assertion required, since the test will throw if there's an issue.
        }

        [Fact]
        public void WriteSetsValueAtAddress()
        {
            const ushort address = 17;
            const byte val = 5;

            _m.Write(address, val);

            Assert.Equal(val, _m.Ram[address]);
        }

        [Fact]
        public void ReadGetsByteValueAtAddress()
        {
            const ushort address = 73;
            const byte val = 32;

            _m.Ram[address] = val;

            Assert.Equal(val, _m.Read(address));
        }

        [Fact]
        public void ReadWordGetsCorrectWordValueFromConsecutiveAddresses()
        {
            const ushort address = 210;
            const byte msb = 0xff;
            const byte lsb = 0xee;
            const ushort word = 0xffee;

            //this is the correct behaviour for CHIP-8 (big-endian)
            _m.Ram[address] = msb;
            _m.Ram[address + 1] = lsb;

            Assert.Equal(word, _m.ReadWord(address));
        }
    }
}
