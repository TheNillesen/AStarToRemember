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
                    nodes[i, j].position = new Position(i, j);

                    if (i == 4 && j == 2)
                    {
                        nodes[i, j].myType = MyType.stormTower;
                        nodes[i, j].symbole = "t";
                        nodes[i, j].color = ConsoleColor.Yellow;
                    }
                    if (i == 8 && j == 0)
                    {
                        nodes[i, j].myType = MyType.portal;
                        nodes[i, j].symbole = "p";
                        nodes[i, j].color = ConsoleColor.Blue;
                    }
                    if (j > 1 && j < 7 && i == 9)
                    {
                        nodes[i, j].myType = MyType.notWalkable;
                        nodes[i, j].symbole = "T";
                        nodes[i, j].color = ConsoleColor.DarkGreen;
                    }
                    if (j > 1 && j < 7 && i == 7)
                    {
                        nodes[i, j].myType = MyType.notWalkable;
                        nodes[i, j].symbole = "T";
                        nodes[i, j].color = ConsoleColor.DarkGreen;
                    }
                    if (i > 0 && i < 7 && j > 3 && j < 7)
                    {
                        nodes[i, j].myType = MyType.notWalkable;
                        nodes[i, j].symbole = "v";
                        nodes[i, j].color = ConsoleColor.Gray;
                    }
                    if (i == 7 && j == 8)
                    {
                        nodes[i, j].myType = MyType.iceTower;
                        nodes[i, j].symbole = "i";
                        nodes[i, j].color = ConsoleColor.Cyan;
                    }
                    if (i == 8 && j == 5)
                    {
                        nodes[i, j].myType = MyType.monster;
                        nodes[i, j].symbole = "o";
                        nodes[i, j].color = ConsoleColor.Green;
                    }
                    if (nodes[i, j].myType == MyType.walkable)
                    {
                        walkables.Add(nodes[i,j]);
                        nodes[i, j].color = ConsoleColor.Green;
                    }
                }
            }
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

            int index2 = rnd.Next(walkables.Count);
            walkables[index2].myType = MyType.key;
            walkables[index2].symbole = "k";
            walkables[index2].color = ConsoleColor.DarkYellow;
            walkables.RemoveAt(index2);

            while (index2 == index)
            {
                index2 = rnd.Next(walkables.Count);
            }
        }

        /// <summary>
        /// Clears and prints the map.
        /// </summary>
        public void Render(bool findingPath)
        {
            Console.Clear();
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    if(findingPath && nodes[i, j].colorPathfinding != ConsoleColor.White)
                        Console.ForegroundColor = nodes[i, j].colorPathfinding;
                    else
                        Console.ForegroundColor = nodes[i, j].color;
                    if(nodes[i, j].wizardHere)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write("W ");
                    }
                    else
                        Console.Write(nodes[i, j].symbole + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("\n");
            }
        }
    }
}
