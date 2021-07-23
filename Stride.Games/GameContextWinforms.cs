using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stride.Games
{
    /// <summary>
    /// A GameContext to use for rendering to an existing Winform Control
    /// </summary>
    public class GameContextWinforms : GameContext<Control>
    {
        public GameContextWinforms(Control control, int requestedWidth = 0, int requestedHeight = 0, bool isUserManagingRun = false )
            :base(control ?? CreateForm(), requestedWidth, requestedHeight, isUserManagingRun)
        {
            ContextType = AppContextType.Desktop;
        }

        private static Form CreateForm()
        {
            return new GameForm();
        }
    }
}
