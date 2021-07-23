using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Core
{
    public static class Utilities
    {
        public static void Sleep(TimeSpan sleepTime)
        {
            var ms = (long)sleepTime.TotalMilliseconds;
            if (ms < 0 || ms > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(sleepTime), "Sleep time must be a duration less than '2^31 - 1' milliseconds.");
            }

            NativeInvoke.Sleep((int)ms);
        }
    }
}
