using ill8.Cpu.Test.ClassData;
using Moq;
using Xunit;

namespace ill8.Cpu.Test.InstructionsTests
{
    public class _3XNNTests
    {
        private readonly Instructions _sut;

        public _3XNNTests()
        {
            var cpu = new Mock<ICpu>()
                .SetupAllProperties()
                .SetupProperty(x => x.V, new byte[16])
                .Object;
            _sut = new Instructions(cpu);
        }

        [Theory]
        [ClassData(typeof(Range0xf))]
        public void IncreasePCBy2IfConditionMet(byte x)
        {
            var pc = _sut.Cpu.PC;
            _sut.Cpu.V[x] = 0xab;
            ushort opcode = (ushort)(0x30ab | x << 8);

            _sut._3XNN(opcode);

            Assert.Equal(pc+2, _sut.Cpu.PC);
        }

        [Theory]
        [ClassData(typeof(Range0xf))]
        public void LeavePCAloneIfConditionNotMet(byte x)
        {
            var pc = _sut.Cpu.PC;
            _sut.Cpu.V[x] = 0xea;
            ushort opcode = (ushort)(0x30ab | x << 8);

            _sut._3XNN(opcode);

            Assert.Equal(pc, _sut.Cpu.PC);
        }
    }
}
