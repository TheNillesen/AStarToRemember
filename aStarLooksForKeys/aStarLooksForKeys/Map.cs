using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aStarLooksForKeys
{
    class Map
    {
        public Node[,] nodes;

        public Map(int mapWidth, int mapHeight)
        {
            nodes = new Node[mapWidth, mapHeight];
            GenerateMap(mapWidth, mapHeight);
        }

        private void GenerateMap(int mapWidth, int mapHeight)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for(int j = 0; j < mapHeight; i++)
                {
                    Node[i, j] = new Node(MyType.walkable);
                }
            }
        }
    }
}
