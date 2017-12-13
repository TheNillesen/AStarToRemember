using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aStarLooksForKeys
{
    class GameWorld
    {
        public Map map;
        private Wizard wizard;

        public GameWorld()
        {
            Console.WriteLine("Welcome to the Algorithm Simulator 5000");
            Console.WriteLine("How will you proceed?");
            Console.WriteLine("Press 1 for: A*. Press 2 for something else");
            Console.ReadKey();
            Console.Clear();

            map = new Map(10, 10);
            wizard = new Wizard(map.nodes[8,1 ]);
            map.Render();
            GameLoop();
        }

        private void GameLoop()
        {
            bool run = true;

            while (run)
            {
                wizard.Move(this);
            }
        }
    }
}
