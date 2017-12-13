using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aStarLooksForKeys
{
    class GameWorld
    {
        private Map map;
        private Wizard wizard;

        public GameWorld()
        {
            map = new Map(10, 10);
            wizard = new Wizard(new Position(1, 1));
            map.Render();
            GameLoop();
        }

        private void GameLoop()
        {
            bool run = true;
            while (run)
            {

            }
        }
    }
}
