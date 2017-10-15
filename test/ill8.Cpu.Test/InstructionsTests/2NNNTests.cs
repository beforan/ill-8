using Moq;
using Xunit;

namespace ill8.Cpu.Test.InstructionsTests
{
    public class _2NNNTests
    {
        private readonly Instructions _sut;

        public _2NNNTests()
        {
            var cpu = new Mock<ICpu>()
                .SetupAllProperties()
                .SetupProperty(x => x.Stack, new ushort[16])
                .Object;
            _sut = new Instructions(cpu);
        }

        [Fact]
        public void IncrementSP()
        {
            var sp = _sut.Cpu.SP;

            _sut._2NNN(0x2800);

            Assert.Equal(sp + 1, _sut.Cpu.SP);
        }

        [Fact]
        public void StorePCInStack()
        {
            ushort value = 0x0202; //reasonable value for PC
            _sut.Cpu.PC = value;

            _sut._2NNN(0x2800);

            Assert.Equal(value, _sut.Cpu.Stack[_sut.Cpu.SP - 1]);
        }

        [Fact]
        public void SetPCTo12bitAddress()
        {
            _sut._2NNN(0x26fe);

            Assert.Equal(0x06fe, _sut.Cpu.PC);
        }
    }
}
