using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aStarLooksForKeys
{
    class Wizard
    {
        private Node current;
        private Queue<Node> path;
        private Queue<Node> destinations;
        private Node currentDes;
        private string symbole;
        private bool firstTime;

        public Wizard(Node StartNode)
        {
            firstTime = true;
            symbole = "W";
            this.current = StartNode;
            destinations = new Queue<Node>();
            path = new Queue<Node>();
        }

        public void Move(GameWorld gameworld)
        {
            //Finds the destinations.
            if(firstTime || destinations.Count() <= 0 || destinations == null)
            {
                Destinations(gameworld);
                firstTime = false;
            }
            //Sets the current destination.
            if (currentDes == null)
                currentDes = destinations.Dequeue();

            //Finds the path.
            if (path.Count() <= 0 || path == null)
                path = Pathfinding.AStarQueue(gameworld.map.nodes[current.position.X, current.position.Y], currentDes, ref gameworld.map);

            //Moves to the next position.
            WriteAt(current.position.X, current.position.Y, current.symbole);
            current = path.Dequeue();
            WriteAt(current.position.X, current.position.Y, symbole);
            if (path.Count() <= 0)
                currentDes = null;

            //Only for bug testing
            Stopwatch stopwatch = Stopwatch.StartNew();
            int millisecondsToWait = 500;
            gameworld.map.Render();
            while (true)
            {
                //some other processing to do STILL POSSIBLE
                if (stopwatch.ElapsedMilliseconds >= millisecondsToWait)
                {
                    break;
                }
                Thread.Sleep(1); //so processor can rest for a while
            }
        }

        private void WriteAt(int x, int y, string s)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }

        private void Destinations(GameWorld gameworld)
        {
            for (int x = 0; x < gameworld.map.nodes.GetLength(0); x++)
            {
                for (int y = 0; y < gameworld.map.nodes.GetLength(1); y++)
                {
                    if (gameworld.map.nodes[x, y].myType == MyType.key || gameworld.map.nodes[x, y].myType == MyType.tower || gameworld.map.nodes[x, y].myType == MyType.goal)
                        destinations.Enqueue(gameworld.map.nodes[x, y]);
                }
            }
        }
    }
}
