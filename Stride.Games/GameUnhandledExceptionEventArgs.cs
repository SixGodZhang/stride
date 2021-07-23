using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Games
{
    public class GameUnhandledExceptionEventArgs
    {

        /// <summary>
        /// Gets the unhandled exception object.
        /// </summary>
        public object ExceptionObject { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the CLR is terminating.
        /// </summary>
        public bool IsTerminating { get; private set; }
        public GameUnhandledExceptionEventArgs(object exceptionObject, bool isTerminating)
        {
            ExceptionObject = exceptionObject;
            IsTerminating = isTerminating;
        }
    }
}
