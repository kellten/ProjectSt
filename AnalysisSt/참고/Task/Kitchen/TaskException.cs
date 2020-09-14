using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen
{
    class TaskException : Exception
    {
        /// <summary>

        /// Argument constructor

        /// </summary>

        /// <param name="message">This is the description of the exception</param>

        public TaskException(String message) : base(message)

        {

        }
    }
}
