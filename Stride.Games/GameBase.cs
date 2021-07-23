using Stride.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stride.Games
{
    public abstract class GameBase: ComponentBase
    {
        private readonly GamePlatform gamePlatform;

        private bool isMouseVisible;
        private bool beginDrawOk;
        private bool isEndRunRequired;

        internal object TickLock = new object();

        // private readonly GamePlatform;
        public GameContext Context { get; private set; }
        /// <summary>
        /// The total and delta time to be used for logic running in the update loop.
        /// </summary>
        public GameTime UpdateTime { get; }

        /// <summary>
        /// The total and delta time to be used for logic running in the draw loop.
        /// </summary>
        public GameTime DrawTime { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is exiting.
        /// </summary>
        public bool IsExiting { get; private set; }

        /// <summary>
        /// Gets or sets the time between each Tick when is false
        /// </summary>
        public TimeSpan InactiveSleepTime { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        public GameWindow Window
        {
            get
            {
                if (gamePlatform != null)
                {
                    return gamePlatform.MainWindow;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is active.
        /// </summary>
        public bool IsActive { get; private set; }

        public event EventHandler<EventArgs> Activated;
        public event EventHandler<EventArgs> Deactivated;
        public event EventHandler<EventArgs> Exiting;
        public event EventHandler<EventArgs> WindowCreated;
        public event EventHandler<GameUnhandledExceptionEventArgs> UnhandledException;

        internal EventHandler<GameUnhandledExceptionEventArgs> UnhandleExceptionInternal
        {
            get { return UnhandledException; }
        }

        protected GameBase()
        {
            UpdateTime = new GameTime();
            DrawTime = new GameTime();

            isMouseVisible = true;

            // Create Platform
            gamePlatform = GamePlatform.Create(this);
            gamePlatform.Activated += GamePlatform_Activated;
            gamePlatform.Deactivated += GamePlatform_Deactivated;
            gamePlatform.Exiting += GamePlatform_Exiting;
            gamePlatform.WindowCreated += GamePlatformOnWindowCreated;

            IsActive = true;

        }

        /// <summary>
        /// Update the game's clock and calls Update and Draw.
        /// </summary>
        internal void Tick()
        {
            lock(TickLock)
            {
                // If this instance is existing, then don't make any further update/draw
                if(IsExiting)
                {
                    CheckEndRun();
                    return;
                }

                // If this instance is not active, sleep for an inactive sleep time
                if (!IsActive)
                {
                    Utilities.Sleep(InactiveSleepTime);
                    return;
                }
            }
        }

        /// <summary>
        /// Called after all components are initialized, before the game loop starts.
        /// </summary>
        protected virtual void BeginRun()
        {

        }

        /// <summary>
        /// Called after the game loop has stopped running before exiting.
        /// </summary>
        protected virtual void EndRun()
        {

        }

        private void CheckEndRun()
        {
            if(IsExiting && IsRunning && isEndRunRequired)
            {
                EndRun();
                IsRunning = false;
            }    
        }

        private void GamePlatformOnWindowCreated(object sender, EventArgs e)
        {
            Window.IsMouseVisible = isMouseVisible;
            OnWindowCreated();
        }

        private void OnWindowCreated()
        {
            WindowCreated?.Invoke(this, EventArgs.Empty);
        }

        private void GamePlatform_Exiting(object sender, EventArgs e)
        {
            OnExiting(this, EventArgs.Empty);
        }

        private void OnExiting(object gameBase, EventArgs args)
        {
            Exiting?.Invoke(this, args);
        }

        internal void Exit()
        {
            IsExiting = true;
            gamePlatform.Exit();
        }

        private void GamePlatform_Deactivated(object sender, EventArgs e)
        {
            if (IsActive)
            {
                IsActive = false;
                OnDeactivated(this, EventArgs.Empty);
            }
        }

        private void OnDeactivated(object gameBase, EventArgs args)
        {
            Deactivated?.Invoke(this, args);
        }

        private void GamePlatform_Activated(object sender, EventArgs e)
        {
            if (!IsActive)
            {
                IsActive = true;
                OnActivated(this, EventArgs.Empty);
            }
        }

        internal void InitializeBeforeRun()
        {
            try
            {
                // Initialize this instance and all game systems before trying to create the device.
                Initialize();

                // Make sure that the device is already created
                // graphicsDeviceManager.CreateDevice();

                // Bind Graphics Context enabling initialing to use GL API eg. SetData to texture ... etc
                BeginDraw();

                // LoadContentInternal();

                IsRunning = true;

                BeginRun();

                UpdateTime.Reset(UpdateTime.Total);

                // Run the first time an update
                Update(UpdateTime);

                EndDraw(false);

            }catch(Exception ex)
            {
                Console.WriteLine("Unexpected exception", ex);
                throw;
            }
        }

        /// <summary>
        /// Ends of the drawing of a frame.
        /// </summary>
        /// <param name="present"></param>
        private void EndDraw(bool present)
        {
            if(beginDrawOk)
            {
                //TODO
                beginDrawOk = false;
            }
        }

        /// <summary>
        /// Time pass since the last call to Update
        /// </summary>
        /// <param name="updateTime"></param>
        private void Update(GameTime updateTime)
        {
            //GameSystems.Update(updateTime);
        }

        protected virtual bool BeginDraw()
        {
            beginDrawOk = false;
            //TODO
            beginDrawOk = true;

            return true;
        }

        protected virtual void Initialize()
        {
            // GameSystems.Initialize();
        }

        private void OnActivated(object sender, EventArgs args)
        {
            Activated?.Invoke(this, args);
        }

        public void Run(GameContext gameContext = null)
        {
            // Gets the GameWindow Context
            Context = gameContext ?? GameContextFactory.NewDefaultGameContext();

            // TODO
            PrepareContext();

            try
            {
                // hardcode
                Context.RequestedWidth = 640;
                Context.RequestedHeight = 1136;


                gamePlatform.Run(Context);
            }finally
            {
                if(!isEndRunRequired)
                {
                    IsRunning = false;
                }
            }
            
        }

        /// <summary>
        /// Creates or updates Context before window and device are created.
        /// </summary>
        protected virtual void PrepareContext()
        {
            //
        }
    }
}
