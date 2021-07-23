using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Games
{
    public interface IGame
    {
        /// <summary>
        /// Occurs when [activated].
        /// </summary>
        event EventHandler<EventArgs> Activated;

        /// <summary>
        /// Occurs when [deactivated]
        /// </summary>
        event EventHandler<EventArgs> Deactivated;

        /// <summary>
        /// Occurs when [exiting]
        /// </summary>
        event EventHandler<EventArgs> Exiting;

        event EventHandler<EventArgs> WindowCreated;

        /// <summary>
        /// Gets the current game time
        /// </summary>
        GameTime UpdateTime { get; }

        /// <summary>
        /// Gets the current draw time.
        /// </summary>
        GameTime DrawTime { get; }


        float DrawInterpolationFactor { get; }


        GameContext Context {get;}

        bool IsMouseVisible { get; set; }

        bool IsRunning { get; }

        GameWindow Window { get; }
    }
}

