using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Games
{
    internal class GamePlatformDesktop:GamePlatform
    {
        public GamePlatformDesktop(GameBase game): base(game)
        {
            IsBlockingRun = true;
        }

        internal override GameWindow GetSupportedGameWindow(AppContextType contextType)
        {
            switch(contextType)
            {
                case AppContextType.Desktop:
                    return new GameWindowWinforms();
            }

            // TODO
            return null;
        }
    }
}
