using System;

namespace Domain
{
    public class InvalidBankOperationException : ApplicationException
    {
        public InvalidBankOperationException(string message) : base(message)
        {
        }

    }
}
