using Moq;
using Xunit;

namespace ill8.Cpu.Test.InstructionsTests
{
    public class _7XNNTests
    {
        private readonly Instructions _sut;

        public _7XNNTests()
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
        public void IncrementVxByNN(byte x)
        {
            _sut.Cpu.V[x] = 0; //ensure we're initialised to 0

            ushort opcode = (ushort)(0x70ea | x << 8);

            _sut._7XNN(opcode);

            Assert.Equal(0xea, _sut.Cpu.V[x]);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(15)]
        public void WrapAroundIfIncrementTakesBeyondFF(byte x)
        {
            _sut.Cpu.V[x] = 2; //ensure we're initialised to something greater than 0

            ushort opcode = (ushort)(0x70ff | x << 8);

            _sut._7XNN(opcode);

            Assert.Equal((2 + 0xff) & 0xff, _sut.Cpu.V[x]);
        }
    }
}
