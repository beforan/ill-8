using ill8.Cpu.Test.ClassData;
using Moq;
using Xunit;

namespace ill8.Cpu.Test.InstructionsTests
{
    public class _4XNNTests
    {
        private readonly Instructions _sut;

        public _4XNNTests()
        {
            var cpu = new Mock<ICpu>()
                .SetupAllProperties()
                .SetupProperty(x => x.V, new byte[16])
                .Object;
            _sut = new Instructions(cpu);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(15)]
        public void IncreasePCBy2IfConditionMet(byte x)
        {
            var pc = _sut.Cpu.PC;
            _sut.Cpu.V[x] = 0xea;
            ushort opcode = (ushort)(0x40ab | x << 8);

            _sut._4XNN(opcode);

            Assert.Equal(pc+2, _sut.Cpu.PC);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(15)]
        public void LeavePCAloneIfConditionNotMet(byte x)
        {
            var pc = _sut.Cpu.PC;
            _sut.Cpu.V[x] = 0xab;
            ushort opcode = (ushort)(0x30ab | x << 8);

            _sut._4XNN(opcode);

            Assert.Equal(pc, _sut.Cpu.PC);
        }
    }
}
