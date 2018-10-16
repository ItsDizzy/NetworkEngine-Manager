using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEngine.Manager.Core.Utils
{
    public static class BufferUtils
    {
//        public static byte[] Concat(byte[] b1, byte[] b2, int count)
//        {
//            byte[] r = new byte[b1.Length + count];
//            System.Buffer.BlockCopy(b1, 0, r, 0, b1.Length);
//            System.Buffer.BlockCopy(b2, 0, r, b1.Length, count);
//            return r;
//        }

        public static byte[] Concat(this byte[] source, byte[] other, int count)
        {
            byte[] result = new byte[source.Length + count];
            Buffer.BlockCopy(source, 0, result, 0, source.Length);
            Buffer.BlockCopy(other, 0, result, source.Length, count);

            return result;
        }
    }
}
