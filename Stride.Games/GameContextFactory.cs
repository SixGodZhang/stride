using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core;

namespace Stride.Games
{
    public static class GameContextFactory
    {
        internal static GameContext NewDefaultGameContext(int requestedWidth = 0, int requestedHeight = 0, bool isUserManagingRun = false)
        {
            // Default context is Desktop
            AppContextType type = AppContextType.Desktop;

            // TODO 此处根据不同的平台来决定创建何种AppContextType

            return NewGameContext(type, requestedWidth, requestedWidth, isUserManagingRun);
        }

        private static GameContext NewGameContext(AppContextType type, int requestedWidth, int requestedHeight, bool isUserManagingRun)
        {
            GameContext res = null;
            switch(type)
            {
                case AppContextType.Desktop:
                    res = NewGameContextDesktop(requestedWidth, requestedHeight, isUserManagingRun);
                    break;
                case AppContextType.DesktopWpf:
                    // TODO 创建WPF的上下文
                    // res = NewGameContextWpf(requestedWidth, requestedHeight, isUserManagingRun);
                    break;
            }

            if (res == null)
            {
                throw new InvalidOperationException("Requested type and current platform are incompatible.");
            }

            return res;
        }

        public static GameContext NewGameContextDesktop(int requestedWidth = 0, int requestedHeight = 0, bool isUserManagingRun = false)
        {
            if (Platform.Type != PlatformType.Windows)
                return null;

            return new GameContextWinforms(null, requestedWidth, requestedHeight, isUserManagingRun);
        }
    }
}
