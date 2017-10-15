using Moq;
using Xunit;

namespace ill8.Cpu.Test.InstructionsTests
{
    public class _1NNNTests
    {
        private readonly Instructions _sut;

        public _1NNNTests()
        {
            var cpu = new Mock<ICpu>()
                .SetupProperty(x => x.PC)
                .Object;
            _sut = new Instructions(cpu);
        }

        [Fact]
        public void SetPCTo12bitAddress()
        {
            _sut._1NNN(0x1400);

            Assert.Equal(0x0400, _sut.Cpu.PC);
        }
    }
}
