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
        private List<Node> destinations;
        private string symbole;
        private bool firstTime;
        private Queue<MyType> orderOfBuisness;

        public Node currentDes;

        public Wizard(Node StartNode)
        {
            firstTime = true;
            symbole = "W";
            this.current = StartNode;
            current.wizardHere = true;
            destinations = new List<Node>();
            path = new Queue<Node>();

            orderOfBuisness = new Queue<MyType>();
            orderOfBuisness.Enqueue(MyType.key);
            orderOfBuisness.Enqueue(MyType.stormTower);
            orderOfBuisness.Enqueue(MyType.key);
            orderOfBuisness.Enqueue(MyType.iceTower);
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
            {
                MyType temp = orderOfBuisness.Dequeue();
                Node tempN = null;
                float dis = 10000;
                foreach(Node node in destinations)
                {
                    //If the next disstination is a key.
                    if(temp == MyType.key && node.myType == MyType.key)
                    {
                        if(tempN == null)
                        {
                            tempN = node;
                            dis = Distance(current.position, node.position);
                        }
                        else
                        {
                            if (dis > Distance(current.position, node.position))
                                tempN = node;
                        }
                    }
                    //If the next disstination is the stormtower.
                    if (temp == MyType.stormTower && node.myType == MyType.stormTower)
                        tempN = node;
                    //If the next disstination is the icetower.
                    if (temp == MyType.iceTower && node.myType == MyType.iceTower)
                        tempN = node;
                }
                currentDes = tempN;
            }

            //Finds the path.
            if (path.Count() <= 0 || path == null)
            {
                foreach (Node node in gameworld.map.nodes)
                    node.colorPathfinding = ConsoleColor.White;
                path = Pathfinding.AStarQueue(current, currentDes, ref gameworld.map);
            }

            //Moves to the next position.
            current.SetWizardHere(false, this);
            current = path.Dequeue();
            current.SetWizardHere(true, this);
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
        //private void Win()
        //{
        //    if (true)
        //    {

        //    }
        //}
        /// <summary>
        /// Finds the destinations.
        /// </summary>
        /// <param name="gameworld"></param>
        private void Destinations(GameWorld gameworld)
        {
            for (int x = 0; x < gameworld.map.nodes.GetLength(0); x++)
            {
                for (int y = 0; y < gameworld.map.nodes.GetLength(1); y++)
                {
                    if (gameworld.map.nodes[x, y].myType == MyType.key || gameworld.map.nodes[x, y].myType == MyType.stormTower || gameworld.map.nodes[x, y].myType == MyType.iceTower || gameworld.map.nodes[x, y].myType == MyType.goal)
                        destinations.Add(gameworld.map.nodes[x, y]);
                }
            }
        }

        //Finds the distance between two positions.
        private float Distance(Position a, Position b)
        {
            return (float)Math.Sqrt((a.X + b.X) * (a.X + b.X) + (a.Y + b.Y) * (a.Y + b.Y));
        }
    }
}
