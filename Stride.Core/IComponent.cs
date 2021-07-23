using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Core
{
    public interface IComponent:IReferencable
    {
        string Name { get; }
    }
}
