using System;
using System.Runtime.Serialization;

namespace Accolades.Maije.Infrastructure.Exceptions
{
    public class InfrastructureException : Exception
    {
        /// <summary>
        /// Initialize a new <see cref="InfrastructureException"/>
        /// </summary>
        public InfrastructureException()
        {
        }

        /// <summary>
        /// Initialize a new <see cref="InfrastructureException"/>
        /// </summary>
        /// <param name="message">The exception message</param>
        public InfrastructureException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initialize a new <see cref="InfrastructureException"/>
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public InfrastructureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
