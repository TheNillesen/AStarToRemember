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

        public GameWorld()
        {
            map = new Map(10, 10);
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
