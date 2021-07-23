using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Games
{
    public interface IMessageLoop : IDisposable
    {
        bool NextFrame();
    }
}
