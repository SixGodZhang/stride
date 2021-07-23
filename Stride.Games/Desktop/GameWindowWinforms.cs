using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stride.Games
{
    /// <summary>
    /// An abstract window.
    /// </summary>
    internal class GameWindowWinforms : GameWindow<Control>
    {
        private bool isMouseVisible;

        private bool isMouseCurrentlyHidden;

        public Control Control;

        private WindowHandle windowHandle;

        private Form form;

        private bool isFullScreenMaximized;
        private FormBorderStyle savedFormBorderStyle;
        private bool oldVisible;
        private bool deviceChangeChangedVisible;
        private bool? deviceChangeWillBeFullScreen;

        private bool allowUserResizing;
        private bool isBorderLess;

        internal GameWindowWinforms() { }

        public override WindowHandle NativeWindow { get => windowHandle; }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="gameContext"></param>
        protected override void Initialize(GameContext<Control> gameContext)
        {
            Control = gameContext.Control;

            // Setup the initial size of the window
            var width = gameContext.RequestedWidth;
            if (width == 0)
            {
                // width = Control is Form?Gra
            }

            var height = gameContext.RequestedHeight;
            if (height == 0)
            {
                //
            }

            windowHandle = new WindowHandle(AppContextType.Desktop, Control, Control.Handle);

            Control.ClientSize = new Size(width, height);

            // Control.MouseEnter += GameWindowForm_MouseEnter;
            // Control.MouseLeave += GameWindowForm_MOuseLeave;

            form = Control as Form;
            var gameForm = Control as GameForm;
            if (gameForm != null)
            {
                //gameForm.UserResized += OnClientSizeChanged;
                //gameForm.FullscreenToggle += OnFullscreenToggle;
                //gameForm.FormClosing += OnClosing;
            }
            else
            {
                // Control.Resize += OnClientSizeChanged;
            }
        }

        /// <summary>
        /// Run
        /// </summary>
        internal override void Run()
        {
            // Initialize the init callback
            InitCallback();

            var context = (GameContextWinforms)GameContext;
            if (context.IsUserManagingRun)
            {
                context.RunCallback = RunCallback;
                context.ExitCallback = ExitCallback;
            }
            else
            {
                var runCallback = new WindowsMessageLoop.RenderCallback(RunCallback);
                try
                {
                    WindowsMessageLoop.Run(Control, () => {

                        if (Exiting)
                        {
                            Destroy();
                            return;
                        }

                        runCallback();
                    });
                } finally
                {
                    ExitCallback?.Invoke();
                }
            }
        }

        public override bool IsMouseVisible
        {
            get
            {
                return isMouseVisible;
            }
            set
            { 
                if (isMouseVisible != value)
                {
                    isMouseVisible = value;
                    if (isMouseVisible)
                    {
                        if (isMouseCurrentlyHidden)
                        {
                            Cursor.Show();
                            isMouseCurrentlyHidden = false;
                        }
                    }else if (!isMouseCurrentlyHidden)
                    {
                        Cursor.Hide();
                        isMouseCurrentlyHidden = true;
                    }
                }
            } 
        }
    }
}
