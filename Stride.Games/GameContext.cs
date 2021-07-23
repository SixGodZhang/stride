using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Games
{
    public abstract class GameContext
    {

        public AppContextType ContextType { get; protected set; }

        /// <summary>
        /// Indicating whether the user will call the main loop. E.g. Stride is used as a library.
        /// </summary>
        public bool IsUserManagingRun { get; protected set; }

        public Action RunCallback { get; internal set; }

        public Action ExitCallback { get; internal set; }

        internal int RequestedWidth;

        internal int RequestedHeight;

        internal static string ProductName
        {
            get
            {
                var assembly = Assembly.GetEntryAssembly();
                var productAttribute = assembly?.GetCustomAttribute<AssemblyProductAttribute>();
                return productAttribute?.Product ?? "Stride Game";
            }
        }

        public static string ProductLocation
        {
            get
            {
                var assembly = Assembly.GetEntryAssembly();
                return assembly?.Location;
            }
        }
    }

    public abstract class GameContext<TK>:GameContext
    {
        public TK Control { get; internal set; }

        protected GameContext(TK control, int requestedWidth = 0, int requestedHeight = 0, bool isUserManagingRun = false)
        {
            Control = control;
            RequestedWidth = requestedWidth;
            RequestedHeight = requestedHeight;
            IsUserManagingRun = isUserManagingRun;
        }
    }
}
