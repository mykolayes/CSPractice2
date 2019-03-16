using System;

namespace NaUKMA.CS.Practice02
{
    internal class BirthDateException : Exception
    {
        internal BirthDateException()
        {
        }

        internal BirthDateException(string message)
            : base(message)
        {
        }

        internal BirthDateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
