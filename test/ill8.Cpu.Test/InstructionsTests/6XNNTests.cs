using Moq;
using Xunit;

namespace ill8.Cpu.Test.InstructionsTests
{
    public class _6XNNTests
    {
        private readonly Instructions _sut;

        public _6XNNTests()
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
        public void SetVxToNN(byte x)
        {
            ushort opcode = (ushort)(0x60ea | x << 8);

            _sut._6XNN(opcode);

            Assert.Equal(0xea, _sut.Cpu.V[x]);
        }
    }
}
