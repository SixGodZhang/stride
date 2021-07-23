using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Games
{
    /// <summary>
    /// Current timing used for variable-step (real time) or fixed-step (game time) games.
    /// </summary>
    public class GameTime
    {
        private TimeSpan accumulateElapsedTime;
        private int accumulatedFrameCountPerSecond;
        private double factor;

        /// <summary>
        /// Gets the elapsed game time since the last update
        /// </summary>
        public TimeSpan Elapsed { get; private set; }


        /// <summary>
        /// Get the amount of game time since the start of the game.
        /// </summary>
        public TimeSpan Total { get; private set; }

        /// <summary>
        /// Gets the current frame count since the start of the game
        /// </summary>
        public int FrameCount { get; private set; }

        /// <summary>
        /// Gets the number of frame per second (FPS) for the current running game.
        /// </summary>
        public float FramePerSecond { get; private set; }

        public TimeSpan TimePerFrame { get; private set; }

        public bool FramePerSecondUpdated { get; private set; }

        public TimeSpan WarpElapsed { get; private set; }

        public double Factor
        {
            get => factor;
            set => factor = value > 0 ? value : 0;
        }

        public GameTime()
        {
            accumulateElapsedTime = TimeSpan.Zero;
            factor = 1;
        }

        public GameTime(TimeSpan totalTime, TimeSpan elapsedTime)
        {
            Total = totalTime;
            Elapsed = elapsedTime;
            accumulateElapsedTime = TimeSpan.Zero;
            factor = 1;
        }

        internal void Update(TimeSpan totalGameTime, TimeSpan elapsedGameTIme, bool incrementFrameCount)
        {
            Total = totalGameTime;
            Elapsed = elapsedGameTIme;
            WarpElapsed = TimeSpan.FromTicks((long)(Elapsed.Ticks * Factor));

            FramePerSecondUpdated = false;

            if (incrementFrameCount)
            {
                accumulateElapsedTime += elapsedGameTIme;
                var accumulateElapsedGameTimeInSecond = accumulateElapsedTime.TotalSeconds;
                if (accumulatedFrameCountPerSecond > 0 & accumulateElapsedGameTimeInSecond > 1.0)
                {
                    TimePerFrame = TimeSpan.FromTicks(accumulateElapsedTime.Ticks / accumulatedFrameCountPerSecond);
                    FramePerSecond = (float)(accumulatedFrameCountPerSecond / accumulateElapsedGameTimeInSecond);
                    accumulatedFrameCountPerSecond = 0;
                    accumulateElapsedTime = TimeSpan.Zero;
                    FramePerSecondUpdated = true;
                }

                accumulatedFrameCountPerSecond++;
                FrameCount++;
            }
        }

        internal void Reset(TimeSpan totalGameTime)
        {
            Update(totalGameTime, TimeSpan.Zero, false);
            accumulateElapsedTime = TimeSpan.Zero;
            accumulatedFrameCountPerSecond = 0;
            FrameCount = 0;
        }

        public void ResetTimeFactor()
        {
            factor = 1;
        }
    }
}
