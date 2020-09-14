using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen
{
    class TaskRetryableException : Exception
    {
        /// <summary>

        /// Default constructor

        /// </summary>

        public TaskRetryableException() : base()

        {

        }

        /// <summary>

        /// Argument constructor

        /// </summary>

        /// <param name="message">This is the description of the exception</param>

        public TaskRetryableException(String message) : base(message)

        {

        }

        /// <summary>

        /// Argument constructor with inner exception

        /// </summary>

        /// <param name="message">This is the description of the exception</param>

        /// <param name="innerException">Inner exception</param>

        public TaskRetryableException(String message, Exception innerException) : base(message, innerException)

        {

        }
    }
}
