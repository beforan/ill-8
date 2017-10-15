using Moq;
using Xunit;

namespace ill8.Cpu.Test.InstructionsTests
{
    public class _5XY0Tests
    {
        private readonly Instructions _sut;

        public _5XY0Tests()
        {
            var cpu = new Mock<ICpu>()
                .SetupAllProperties()
                .SetupProperty(x => x.V, new byte[16])
                .Object;
            _sut = new Instructions(cpu);
        }

        [Theory]
        [InlineData(15, 0)]
        [InlineData(12, 7)]
        public void IncreasePCBy2IfConditionMet(byte x, byte y)
        {
            var pc = _sut.Cpu.PC;

            _sut.Cpu.V[x] = 0xea;
            _sut.Cpu.V[y] = 0xea;
            ushort opcode = (ushort)(0x5000 | x << 8 | y << 4);

            _sut._5XY0(opcode);

            Assert.Equal(pc+2, _sut.Cpu.PC);
        }

        [Theory]
        [InlineData(0, 15)]
        [InlineData(12, 7)]
        public void LeavePCAloneIfConditionNotMet(byte x, byte y)
        {
            var pc = _sut.Cpu.PC;

            _sut.Cpu.V[x] = 0xab;
            _sut.Cpu.V[y] = 0xea;
            ushort opcode = (ushort)(0x5000 | x << 8 | y << 4);

            _sut._5XY0(opcode);

            Assert.Equal(pc, _sut.Cpu.PC);
        }
    }
}
