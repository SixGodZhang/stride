using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Games
{
    /// <summary>
    /// A platform specific window handle.
    /// </summary>
    public class WindowHandle
    {
        public WindowHandle(AppContextType context, object nativeWindow, IntPtr handle)
        {
            Context = context;
            NativeWindow = nativeWindow;
            Handle = handle;
        }

        /// <summary>
        /// The context.
        /// </summary>
        public readonly AppContextType Context;

        /// <summary>
        /// The native windows as an opaque Object
        /// </summary>
        public object NativeWindow { get; }

        /// <summary>
        /// The associated platform specific handle of NativeWindow
        /// </summary>
        public IntPtr Handle { get; }
    }
}
