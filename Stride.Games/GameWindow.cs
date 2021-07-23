using Stride.Core;
using Stride.Core.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Games
{
    public abstract class GameWindow:ComponentBase
    {
        private string title;

        public bool IsActivated;

        /// <summary>
        /// Gets or sets a value indicating whether the mouse pointer is visible over this window.
        /// </summary>
        public abstract bool IsMouseVisible { get; set; }

        /// <summary>
        /// Gets the native window.
        /// </summary>
        public abstract WindowHandle NativeWindow { get; }

        /// <summary>
        /// The size the window should have when switching from fullscreen to windowed mode.
        /// to get the current actual size use ClientBounds
        /// This gets overwritten when the user resizes the window.
        /// </summary>
        public Int2 PreferredWindowedSize { get; set; } = new Int2(768, 432);
        /// <summary>
        /// The size the window should have when switching from windowed to fullscreen mode.
        /// To get the current actual size use ClientBounds
        /// </summary>
        public Int2 PreferredFullscreenSize { get; set; } = new Int2(1920, 1080);

        public event EventHandler<EventArgs> Activated;

        public event EventHandler<EventArgs> ClientSizeChanged;

        public event EventHandler<EventArgs> Deactivated;

        public event EventHandler<EventArgs> Closing;

        internal Action InitCallback;
        internal Action RunCallback;
        internal Action ExitCallback;

        internal bool Exiting;

        internal abstract void Run();

        protected internal abstract void Initialize(GameContext gameContext);
    }

    public abstract class GameWindow<TK> :GameWindow
    {
        protected internal sealed override void Initialize(GameContext gameContext)
        {
            var context = gameContext as GameContext<TK>;
            if (context != null)
            {
                GameContext = context;
                Initialize(context);
            }
            else
            {
                throw new InvalidOperationException("Invalid context for current game.");
            }
        }

        internal GameContext<TK> GameContext;

        protected abstract void Initialize(GameContext<TK> context);
    }
}
