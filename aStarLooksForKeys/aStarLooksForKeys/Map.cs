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
        public List<Node> walkables; 

        public Map(int mapWidth, int mapHeight)
        {
            walkables = new List<Node>();
            nodes = new Node[mapWidth, mapHeight];
            GenerateMap(mapWidth, mapHeight);
        }

        private void GenerateMap(int mapWidth, int mapHeight)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for(int j = 0; j < mapHeight; j++)
                {
                    nodes[i, j] = new Node(MyType.walkable, "o");
                   
                    if (i == 4 && j == 2)
                    {
                        nodes[i, j] = new Node(MyType.tower, "t");
                    }
                    if (i == 8 && j == 0)
                    {
                        nodes[i, j] = new Node(MyType.portal, "p");
                    }
                    if (j > 1 && j < 7 && i == 9)
                    {
                        nodes[i, j] = new Node(MyType.notWalkable, "T");
                    }
                    if (j > 1 && j < 7 && i == 7)
                    {
                        nodes[i, j] = new Node(MyType.notWalkable, "T");
                    }
                    if (i > 0 && i < 7 && j > 3 && j < 7)
                    {
                        nodes[i, j] = new Node(MyType.notWalkable, "v");
                    }
                    if (i == 7 && j == 8)
                    {
                        nodes[i, j] = new Node(MyType.tower, "i");
                    }
                    if (i == 8 && j == 1)
                    {
                        nodes[i, j] = new Node(MyType.wizard, "w");
                    }
                    if (nodes[i, j].myType == MyType.walkable)
                    {
                        walkables.Add(nodes[i,j]);
                        
                    }
                }
            }
            PlaceKey();
            PlaceKey();
        }
        private void PlaceKey()
        {
            Random rnd = new Random();
            int index = rnd.Next(walkables.Count);
            walkables[index].myType = MyType.key;
            walkables[index].symbole = "k";
            walkables.RemoveAt(index);
        }
        public void Render()
        {
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    Console.Write(nodes[i, j].symbole + " ");
                }
                Console.Write("\n");
            }
        }
    }
}
