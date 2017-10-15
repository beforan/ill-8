using Moq;
using Xunit;

namespace ill8.Cpu.Test.InstructionsTests
{
    public class ExecTests
    {
        private readonly Mock<Instructions> _mock;
        public ExecTests()
        {
            var cpu = new Mock<ICpu>().Object;
            _mock = new Mock<Instructions>(cpu)
            {
                CallBase = true
            };
        }

        [Fact] //This is drastically quicker / less demanding than a Theory with ~0xfff cases
        public void x0000Calls0NNN()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xfff; i++)
            {
                if (i == 0x0e0 || i == 0x0ee) continue; //skip the special cases

                ushort opcode = (ushort)(0x0000 | i);
                _mock.Setup(x => x._0NNN(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._0NNN(opcode), Times.Once);
            }
        }

        [Fact]
        public void x00E0Calls00E0()
        {
            ushort opcode = 0x00E0;
            _mock.Setup(x => x._00E0(opcode));
            var sut = _mock.Object;

            sut.Exec(opcode);

            _mock.Verify(x => x._00E0(opcode), Times.Once);
        }

        [Fact]
        public void x00EECalls00EE()
        {
            ushort opcode = 0x00EE;
            _mock.Setup(x => x._00EE(opcode));
            var sut = _mock.Object;

            sut.Exec(opcode);

            _mock.Verify(x => x._00EE(opcode), Times.Once);
        }

        [Fact]
        public void x1000Calls1NNN()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xfff; i++)
            {
                ushort opcode = (ushort)(0x1000 | i);
                _mock.Setup(x => x._1NNN(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._1NNN(opcode), Times.Once);
            }
        }

        [Fact]
        public void x2000Calls2NNN()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xfff; i++)
            {
                ushort opcode = (ushort)(0x2000 | i);
                _mock.Setup(x => x._2NNN(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._2NNN(opcode), Times.Once);
            }
        }

        [Fact]
        public void x3000Calls3XNN()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xfff; i++)
            {
                ushort opcode = (ushort)(0x3000 | i);
                _mock.Setup(x => x._3XNN(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._3XNN(opcode), Times.Once);
            }
        }

        [Fact]
        public void x4000Calls4XNN()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xfff; i++)
            {
                ushort opcode = (ushort)(0x4000 | i);
                _mock.Setup(x => x._4XNN(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._4XNN(opcode), Times.Once);
            }
        }

        [Fact]
        public void x5XY0Calls5XY0()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xff; i++)
            {
                ushort opcode = (ushort)(0x5000 | i << 4);
                _mock.Setup(x => x._5XY0(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._5XY0(opcode), Times.Once);
            }
        }

        [Fact]
        public void x6000Calls6XNN()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xfff; i++)
            {
                ushort opcode = (ushort)(0x6000 | i);
                _mock.Setup(x => x._6XNN(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._6XNN(opcode), Times.Once);
            }
        }

        [Fact]
        public void x7000Calls7XNN()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xfff; i++)
            {
                ushort opcode = (ushort)(0x7000 | i);
                _mock.Setup(x => x._7XNN(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._7XNN(opcode), Times.Once);
            }
        }

        [Fact]
        public void x9XY0Calls9XY0()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xff; i++)
            {
                ushort opcode = (ushort)(0x9000 | i << 4);
                _mock.Setup(x => x._9XY0(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._9XY0(opcode), Times.Once);
            }
        }

        [Fact]
        public void xA000CallsANNN()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xfff; i++)
            {
                ushort opcode = (ushort)(0xA000 | i);
                _mock.Setup(x => x.ANNN(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x.ANNN(opcode), Times.Once);
            }
        }

        [Fact]
        public void xB000CallsBNNN()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xfff; i++)
            {
                ushort opcode = (ushort)(0xB000 | i);
                _mock.Setup(x => x.BNNN(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x.BNNN(opcode), Times.Once);
            }
        }

        [Fact]
        public void xCX00CallsCXNN()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xfff; i++)
            {
                ushort opcode = (ushort)(0xC000 | i);
                _mock.Setup(x => x.CXNN(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x.CXNN(opcode), Times.Once);
            }
        }

        [Fact]
        public void xDXY0CallsDXYN()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xfff; i++)
            {
                ushort opcode = (ushort)(0xD000 | i);
                _mock.Setup(x => x.DXYN(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x.DXYN(opcode), Times.Once);
            }
        }

        [Fact]
        public void xEX9ECallsEX9E()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xf; i++)
            {
                ushort opcode = (ushort)(0xE09E | i << 8 );
                _mock.Setup(x => x.EX9E(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x.EX9E(opcode), Times.Once);
            }
        }

        [Fact]
        public void xEXA1CallsEXA1()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xf; i++)
            {
                ushort opcode = (ushort)(0xE0A1 | i << 8);
                _mock.Setup(x => x.EXA1(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x.EXA1(opcode), Times.Once);
            }
        }

        [Fact]
        public void x8XY0Calls8XY0()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xff; i++)
            {
                ushort opcode = (ushort)(0x8000 | i << 4);
                _mock.Setup(x => x._8XY0(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._8XY0(opcode), Times.Once);
            }
        }

        [Fact]
        public void x8XY1Calls8XY1()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xff; i++)
            {
                ushort opcode = (ushort)(0x8001 | i << 4);
                _mock.Setup(x => x._8XY1(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._8XY1(opcode), Times.Once);
            }
        }

        [Fact]
        public void x8XY2Calls8XY2()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xff; i++)
            {
                ushort opcode = (ushort)(0x8002 | i << 4);
                _mock.Setup(x => x._8XY2(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._8XY2(opcode), Times.Once);
            }
        }

        [Fact]
        public void x8XY3Calls8XY3()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xff; i++)
            {
                ushort opcode = (ushort)(0x8003 | i << 4);
                _mock.Setup(x => x._8XY3(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._8XY3(opcode), Times.Once);
            }
        }

        [Fact]
        public void x8XY4Calls8XY4()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xff; i++)
            {
                ushort opcode = (ushort)(0x8004 | i << 4);
                _mock.Setup(x => x._8XY4(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._8XY4(opcode), Times.Once);
            }
        }

        [Fact]
        public void x8XY5Calls8XY5()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xff; i++)
            {
                ushort opcode = (ushort)(0x8005 | i << 4);
                _mock.Setup(x => x._8XY5(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._8XY5(opcode), Times.Once);
            }
        }

        [Fact]
        public void x8XY6Calls8XY6()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xff; i++)
            {
                ushort opcode = (ushort)(0x8006 | i << 4);
                _mock.Setup(x => x._8XY6(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._8XY6(opcode), Times.Once);
            }
        }

        [Fact]
        public void x8XY7Calls8XY7()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xff; i++)
            {
                ushort opcode = (ushort)(0x8007 | i << 4);
                _mock.Setup(x => x._8XY7(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._8XY7(opcode), Times.Once);
            }
        }

        [Fact]
        public void x8XYECalls8XYE()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xff; i++)
            {
                ushort opcode = (ushort)(0x800E | i << 4);
                _mock.Setup(x => x._8XYE(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x._8XYE(opcode), Times.Once);
            }
        }

        [Fact]
        public void xFX0ACallsFX0A()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xf; i++)
            {
                ushort opcode = (ushort)(0xF00A | i << 8);
                _mock.Setup(x => x.FX0A(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x.FX0A(opcode), Times.Once);
            }
        }

        [Fact]
        public void xFX15CallsFX15()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xf; i++)
            {
                ushort opcode = (ushort)(0xF015 | i << 8);
                _mock.Setup(x => x.FX15(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x.FX15(opcode), Times.Once);
            }
        }

        [Fact]
        public void xFX18CallsFX18()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xf; i++)
            {
                ushort opcode = (ushort)(0xF018 | i << 8);
                _mock.Setup(x => x.FX18(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x.FX18(opcode), Times.Once);
            }
        }

        [Fact]
        public void xFX1ECallsFX1E()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xf; i++)
            {
                ushort opcode = (ushort)(0xF01E | i << 8);
                _mock.Setup(x => x.FX1E(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x.FX1E(opcode), Times.Once);
            }
        }

        [Fact]
        public void xFX29CallsFX29()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xf; i++)
            {
                ushort opcode = (ushort)(0xF029 | i << 8);
                _mock.Setup(x => x.FX29(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x.FX29(opcode), Times.Once);
            }
        }

        [Fact]
        public void xFX33CallsFX33()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xf; i++)
            {
                ushort opcode = (ushort)(0xF033 | i << 8);
                _mock.Setup(x => x.FX33(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x.FX33(opcode), Times.Once);
            }
        }

        [Fact]
        public void xFX55CallsFX55()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xf; i++)
            {
                ushort opcode = (ushort)(0xF055 | i << 8);
                _mock.Setup(x => x.FX55(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x.FX55(opcode), Times.Once);
            }
        }

        [Fact]
        public void xFX65CallsFX65()
        {
            var sut = _mock.Object;

            for (var i = 0; i <= 0xf; i++)
            {
                ushort opcode = (ushort)(0xF065 | i << 8);
                _mock.Setup(x => x.FX65(opcode));

                sut.Exec(opcode);

                _mock.Verify(x => x.FX65(opcode), Times.Once);
            }
        }
    }
}
