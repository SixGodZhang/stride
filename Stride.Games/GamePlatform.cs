using Stride.Core.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Games
{
    internal abstract class GamePlatform
    {
        protected readonly GameBase game;
        protected GameWindow gameWindow;


        /// <summary>
        /// Is true, Game.Run() is blocking until the game is exited, i.e. internal main loop is used.
        /// Is false, Game.Run() returns immediately and the caller has to manager the main loop by invoking the GameWindow.RunCallback.
        /// </summary>
        public bool IsBlockingRun { get; protected set; }


        public GameWindow MainWindow
        {
            get
            {
                return gameWindow;
            }
        }

        public event EventHandler<EventArgs> Activated;
        public event EventHandler<EventArgs> Deactivated;
        public event EventHandler<EventArgs> Exiting;
        public event EventHandler<EventArgs> Idle;
        public event EventHandler<EventArgs> Resume;
        public event EventHandler<EventArgs> Suspend;
        public event EventHandler<EventArgs> WindowCreated;

        protected GamePlatform(GameBase game)
        {
            this.game = game;
        }

        internal static GamePlatform Create(GameBase game)
        {
            return new GamePlatformDesktop(game);
        }

        public void Run(GameContext gameContext)
        {
            IsBlockingRun = !gameContext.IsUserManagingRun;

            gameWindow = CreateWindow(gameContext);

            // Register on Acvivated
            gameWindow.Activated += OnAcivated;
            gameWindow.Deactivated += OnDeactivated;
            gameWindow.InitCallback = OnInitCallback;
            gameWindow.RunCallback = OnRunCallback;

            WindowCreated?.Invoke(this, EventArgs.Empty);
            gameWindow.Run();
        }

        private void OnRunCallback()
        {
            var unhandledException = game.UnhandleExceptionInternal;
            if (unhandledException != null)
            {
                try
                {
                    Tick();
                }catch (Exception e)
                {
                    unhandledException(this, new GameUnhandledExceptionEventArgs(e, false));
                    game.Exit();
                }
            }
            else
            {
                Tick();
            }
        }

        private void Tick()
        {
            game.Tick();
        }

        private void OnInitCallback()
        {
            var unhandledException = game.UnhandleExceptionInternal;
            if (unhandledException != null)
            {
                try
                {
                    game.InitializeBeforeRun();
                }catch (Exception e)
                {
                    unhandledException(this, new GameUnhandledExceptionEventArgs(e, false));
                    game.Exit();
                }
            }
            else
            {
                game.InitializeBeforeRun();
            }
        }

        private void OnDeactivated(object sender, EventArgs e)
        {
            Deactivated?.Invoke(this, e);
        }

        private void OnAcivated(object sender, EventArgs e)
        {
            Activated?.Invoke(this, e);
        }

        internal void Exit()
        {
            // Nofifies that the GameWindow should exit.
            gameWindow.Exiting = true;
        }

        public virtual GameWindow CreateWindow(GameContext gameContext)
        {
            var window = GetSupportedGameWindow(gameContext.ContextType);
            if (window != null)
            {
                // TODO services
                var requestedSize = new Int2(gameContext.RequestedWidth, gameContext.RequestedHeight);
                window.PreferredWindowedSize = requestedSize;
                window.PreferredWindowedSize = requestedSize;

                window.Initialize(gameContext);
                return window;
            }

            throw new ArgumentException("Game Window context not supported on this platform");
        }

        internal abstract GameWindow GetSupportedGameWindow(AppContextType contextType);
    }
}
