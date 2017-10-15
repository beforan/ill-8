using Moq;
using System;
using Xunit;

namespace ill8.Cpu.Test.InstructionsTests
{
    public class _0NNNTests
    {
        //private readonly ICpu _cpu;
        private readonly Instructions _sut;

        public _0NNNTests()
        {
            var cpu = new Mock<ICpu>().Object;
            _sut = new Instructions(cpu);
        }

        [Fact]
        public void Test()
        {
            Assert.Throws<NotImplementedException>(() => _sut._0NNN(0x0000));
        }
    }
}
