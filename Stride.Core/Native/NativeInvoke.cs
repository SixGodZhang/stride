using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Core
{
    public static class NativeInvoke
    {
        internal const string Library = "libcore";

        static NativeInvoke()
        {
            NativeLibraryHelper.PreloadLibrary("libcore", typeof(NativeInvoke));
        }

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "cnSleep", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Sleep(int ms);
    }
}
