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
            current.wizardHere = true;
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
                path = Pathfinding.AStarQueue(current, currentDes, ref gameworld.map);

            //Moves to the next position.
            current.wizardHere = false;
            current = path.Dequeue();
            current.wizardHere = true;
            if (path.Count() <= 0)
                currentDes = null;

            //Does so we wait before moving on, so the wizard moves at a slower pace
            Stopwatch stopwatch = Stopwatch.StartNew();
            int millisecondsToWait = 500;
            gameworld.map.Render(false);
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
