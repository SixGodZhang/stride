using System;
using Stride;
using Stride.Engine;

namespace Stride.Tests.Entrance
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
