using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEngine.Manager.Core.Exceptions
{
    public class TunnelException : Exception
    {
        public TunnelException(string message)
            : base(message)
        {
        }
    }
}
