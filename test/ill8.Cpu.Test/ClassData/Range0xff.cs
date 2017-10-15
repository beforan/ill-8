using System.Collections;
using System.Collections.Generic;

namespace ill8.Cpu.Test.ClassData
{
    public class Range0xff : IEnumerable<object[]>
    {

        public IEnumerator<object[]> GetEnumerator()
        {
            for (int i = 0; i < 256; i++)
                yield return new object[] { i };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
