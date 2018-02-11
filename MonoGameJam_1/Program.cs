using System;

namespace MonoGameJam_1
{
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
            using (var game = new MonoGameJam_1())
                game.Run();
        }
    }
}
