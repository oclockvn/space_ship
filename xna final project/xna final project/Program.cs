using System;

namespace xna_final_project
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SpaceShip game = new SpaceShip())
            {
                game.Run();
            }
        }
    }
#endif
}

