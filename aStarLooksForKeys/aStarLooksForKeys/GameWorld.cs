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
            map = new Map(10, 10);
            wizard = new Wizard(map.nodes[8, 1]);
            map.Render(false);
            gameRun = true;
            run = true;

            Console.SetWindowSize(80, 21);
            Console.Clear();
            Console.WriteLine("Welcome to the Algorithm Simulator 5000");
            Console.WriteLine("How will you proceed?");
            Console.WriteLine("Press 1 for: A*. \nPress 2 for Dijkstra. \nPress 3 to exit game.");
            ConsoleKeyInfo result = Console.ReadKey();
            if (result.Key == ConsoleKey.D1)
            {
                gameRun = true;
                wizard.aStar = true;
                wizard.dijkstra = false;
            }
            if (result.Key == ConsoleKey.D2)
            {
                gameRun = true;
                wizard.aStar = false;
                wizard.dijkstra = true;
            }
            if (result.Key == ConsoleKey.D3)
                run = false;
            Console.Clear();

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
                Console.WriteLine("Press 1 for: A*. \nPress 2 for Dijkstra. \nPress 3 to exit game.");

                //New game
                map = new Map(10, 10);
                wizard = new Wizard(map.nodes[8, 1]);

                ConsoleKeyInfo result = Console.ReadKey();
                if (result.Key == ConsoleKey.D1)
                {
                    gameRun = true;
                    wizard.aStar = true;
                    wizard.dijkstra = false;
                }
                if (result.Key == ConsoleKey.D2)
                {
                    gameRun = true;
                    wizard.aStar = false;
                    wizard.dijkstra = true;
                }
                if (result.Key == ConsoleKey.D3)
                    return;
                Console.Clear();
                map.Render(false);
            }
        }
    }
}
