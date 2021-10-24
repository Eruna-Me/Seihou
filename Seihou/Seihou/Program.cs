using System;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Seihou.LevelTypeGenerator")] //TODO: assemblyinfo.cs

namespace Seihou
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Game())
                game.Run();
        }
    }
#endif
}
