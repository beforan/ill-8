using Moq;
using System;
using Xunit;

namespace ill8.Cpu.Test.InstructionsTests
{
    public class _00EETests
    {
        private readonly Instructions _sut;

        public _00EETests()
        {
            var cpu = new Mock<ICpu>()
                .SetupAllProperties()
                .SetupProperty(x => x.Stack, new ushort[16])
                .Object;
            _sut = new Instructions(cpu);
        }

        [Fact]
        public void DecrementSP()
        {
            var sp = ++_sut.Cpu.SP;
            _sut.Cpu.Stack[_sut.Cpu.SP] = 0x0400;

            _sut._00EE(0x00EE);

            Assert.Equal(sp - 1, _sut.Cpu.SP);
        }

        [Fact]
        public void SetPCToStackValue()
        {
            ushort value = 0x0400;
            _sut.Cpu.Stack[_sut.Cpu.SP] = value;

            _sut._00EE(0x00EE);

            Assert.Equal(value, _sut.Cpu.PC);
        }
    }
}
