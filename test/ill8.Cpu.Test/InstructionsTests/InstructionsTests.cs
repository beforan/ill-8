using Moq;
using Xunit;

namespace ill8.Cpu.Test.InstructionsTests
{
    public class InstructionsTests
    {
        [Fact]
        public void ConstructorInitialisesCpuProperty()
        {
            var cpu = new Mock<ICpu>().Object;

            var i = new Instructions(cpu);

            Assert.Same(cpu, i.Cpu);
        }
    }
}
