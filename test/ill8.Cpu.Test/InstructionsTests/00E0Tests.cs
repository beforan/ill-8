using Moq;
using System.Collections;
using System.Linq;
using Xunit;

namespace ill8.Cpu.Test.InstructionsTests
{
    public class _00E0Tests
    {
        private readonly Instructions _sut;

        public _00E0Tests()
        {
            var cpu = new Mock<ICpu>()
                .SetupProperty(x => x.Vram, new BitArray(64*32))
                .Object;
            _sut = new Instructions(cpu);
        }

        [Fact]
        public void SetAllVramToFalse()
        {
            _sut.Cpu.Vram.SetAll(true);

            _sut._00E0(0x00E0);

            Assert.True(_sut.Cpu.Vram.Cast<bool>().All(x => x == false));
        }
    }
}
