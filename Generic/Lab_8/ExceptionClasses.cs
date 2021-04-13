using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    class IntTypeException : ArgumentException
    {
        public IntTypeException(string message) : base(message)
        {

        }
        public override string ToString()
        {
            return Message;
        }
    }
}
