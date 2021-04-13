using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    class LoggerException : Exception
    {
        public LoggerException(string message) : base(message) { }
    }

    class TransmissionTypeException : ArgumentException
    {
        public string Value { get; }
        public TransmissionTypeException() : base() { }
        public TransmissionTypeException(string val, string message) : base(message)
        {
            Value = val;
        }

        public override string ToString()
        {
            return Message;
        }
    }
    class IntTypeException : ArgumentException
    {
        public int Value { get; }
        public IntTypeException(int val,string message) : base(message)
        {
            Value = val;
        }
        public override string ToString()
        {
            return Message;
        }
    }

    class BrandTypeException : ArgumentException
    {
        public BrandTypeException(string message) : base(message)
        {
          
        }
        public override string ToString()
        {
            return Message;
        }
    }

}
