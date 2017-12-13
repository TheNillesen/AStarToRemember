using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aStarLooksForKeys
{
    class Wizard
    {
        private Node current;
        private Queue<Node> path;
        private List<Node> destinations;
        private string symbole;

        public Wizard(Node StartNode)
        {
            symbole = "W";
            this.current = StartNode;
            destinations = new List<Node>();
            path = new Queue<Node>();
        }

        public void Move()
        {
            //Finds the destinations.
            if(destinations.Count() <= 0 || destinations == null)
            {
                for(int x = 0; x < GameWorld.Instance.map.nodes.GetLength(0); x++)
                {
                    for (int y = 0; y < GameWorld.Instance.map.nodes.GetLength(1); y++)
                    {
                        if (GameWorld.Instance.map.nodes[x, y].myType == MyType.key || GameWorld.Instance.map.nodes[x, y].myType == MyType.tower || GameWorld.Instance.map.nodes[x, y].myType == MyType.goal)
                            destinations.Add(GameWorld.Instance.map.nodes[x, y]);
                    }
                }
            }

            //Finds the path.
            if (path.Count() <= 0 || path == null)
                path = Pathfinding.AStarQueue(GameWorld.Instance.map.nodes[current.position.X, current.position.Y], destinations[0], ref GameWorld.Instance.map);

            //Moves to the next position.
            WriteAt(current.position.X, current.position.Y, current.symbole);
            current = path.Dequeue();
            WriteAt(current.position.X, current.position.Y, symbole);
        }

        private void WriteAt(int x, int y, string s)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }
    }
}
