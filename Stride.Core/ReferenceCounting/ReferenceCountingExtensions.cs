using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Core.ReferenceCounting
{
    internal static class ReferenceCountingExtensions
    {
        public static int AddReferenceInternal(this IReferencable referencable)
        {
            return referencable.AddReference();
        }

        public static int ReleaseInternal(this IReferencable referncable)
        {
            return referncable.Release();
        }
    }
}
