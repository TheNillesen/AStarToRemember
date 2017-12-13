using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aStarLooksForKeys
{
    //
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
                    nodes[i, j].position = new Position(i, j);

                    if (i == 4 && j == 2)
                    {
                        nodes[i, j] = new Node(MyType.tower, "t");
                        nodes[i, j].color = ConsoleColor.Yellow;
                    }
                    if (i == 8 && j == 0)
                    {
                        nodes[i, j] = new Node(MyType.portal, "p");
                        nodes[i, j].color = ConsoleColor.Blue;
                    }
                    if (j > 1 && j < 7 && i == 9)
                    {
                        nodes[i, j] = new Node(MyType.notWalkable, "T");
                        nodes[i, j].color = ConsoleColor.DarkGreen;
                    }
                    if (j > 1 && j < 7 && i == 7)
                    {
                        nodes[i, j] = new Node(MyType.notWalkable, "T");
                        nodes[i, j].color = ConsoleColor.DarkGreen;
                    }
                    if (i > 0 && i < 7 && j > 3 && j < 7)
                    {
                        nodes[i, j] = new Node(MyType.notWalkable, "v");
                        nodes[i, j].color = ConsoleColor.Gray;
                    }
                    if (i == 7 && j == 8)
                    {
                        nodes[i, j] = new Node(MyType.tower, "i");
                        nodes[i, j].color = ConsoleColor.Cyan;
                    }
                    if (i == 8 && j == 1)
                    {
                        nodes[i, j] = new Node(MyType.wizard, "w");
                        nodes[i, j].color = ConsoleColor.DarkBlue;
                    }
                    if (nodes[i, j].myType == MyType.walkable)
                    {
                        walkables.Add(nodes[i,j]);
                        nodes[i, j].color = ConsoleColor.Green;
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
            walkables[index].color = ConsoleColor.DarkYellow;
            walkables.RemoveAt(index);
        }

        public void Render()
        {
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    Console.ForegroundColor = nodes[i, j].color;
                    Console.Write(nodes[i, j].symbole + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("\n");
            }//cloningProblemer
        }
    }
}
