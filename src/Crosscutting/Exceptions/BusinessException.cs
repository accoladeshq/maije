using System;

namespace Accolades.Maije.Crosscutting.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public int Code { get; }

        public BusinessException()
        {
        }

        public BusinessException(string message, int code) : base(message)
        {
            Code = code;
        }

        public BusinessException(string message, int code, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }
    }
}
