using System;

namespace NaUKMA.CS.Practice02
{
    internal class EmailException : Exception
    {
        internal EmailException()
        {
        }

        internal EmailException(string message)
            : base(message)
        {
        }

        internal EmailException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

