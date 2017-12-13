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

        private static GameWorld instance;

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }

        public GameWorld()
        {
            map = new Map(10, 10);
            wizard = new Wizard(map.nodes[0, map.nodes.GetLength(1) - 1]);
            map.Render();
            GameLoop();
        }

        private void GameLoop()
        {
            bool run = true;
            while (run)
            {
                wizard.Move();
            }
        }
    }
}
