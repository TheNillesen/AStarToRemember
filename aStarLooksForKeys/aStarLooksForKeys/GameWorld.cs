using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aStarLooksForKeys
{
    class GameWorld
    {
        private Wizard wizard;
        private bool run;

        public Map map;
        public bool gameRun;
        

        public GameWorld()
        {
            Console.SetWindowSize(80, 21);

            Console.Clear();
            Console.WriteLine("Welcome to the Algorithm Simulator 5000");
            Console.WriteLine("How will you proceed?");
            Console.WriteLine("Press 1 for: A*. Press 2 for something else");
            Console.ReadKey();
            Console.Clear();

            map = new Map(10, 10);
            wizard = new Wizard(map.nodes[8, 1]);
            map.Render(false);
            gameRun = true;
            run = true;
            GameLoop();
        }

        private void GameLoop()
        {
            while (run)
            {
                while (gameRun)
                {
                    wizard.Move(this);
                }

                Console.WriteLine("\nYour run of the Algorithm Simulator 5000 have concluded");
                Console.WriteLine("How will you proceed?");
                Console.WriteLine("Press 1 for: A*. \nPress 2 for something else. \nPress 3 to exit game.");

                ConsoleKeyInfo result = Console.ReadKey();
                if (result.Key == ConsoleKey.D1)
                    gameRun = true;
                if (result.Key == ConsoleKey.D3)
                    return;
                Console.Clear();

                //New game
                map = new Map(10, 10);
                wizard = new Wizard(map.nodes[8, 1]);
                map.Render(false);
            }
        }
    }
}
