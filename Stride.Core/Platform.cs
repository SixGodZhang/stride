using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Core
{
    public static class Platform
    {
        public static readonly PlatformType Type = PlatformType.Windows;

        public static readonly bool IsWindowDesktop = Type == PlatformType.Windows;
    }
}
