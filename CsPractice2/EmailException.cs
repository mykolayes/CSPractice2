using System;

namespace NaUKMA.CS.Practice02
{
    public class EmailException : Exception
    {
        public EmailException()
        {
        }

        public EmailException(string message)
            : base(message)
        {
        }

        public EmailException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

