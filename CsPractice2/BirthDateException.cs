using System;

namespace NaUKMA.CS.Practice02
{
    public class BirthDateException : Exception
    {
        public BirthDateException()
        {
        }

        public BirthDateException(string message)
            : base(message)
        {
        }

        public BirthDateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
