using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aStarLooksForKeys
{
    class Node
    {
        public MyType myType;
        public string symbole;
        public ConsoleColor color;
        public ConsoleColor colorPathfinding;

        /// <summary>
        /// The grid position of the cell
        /// </summary>
        public Position position;

        /// <summary>
        /// The h(x), the heuristik, for astar pathfinding.
        /// </summary>
        public int h;
        /// <summary>
        /// The g(x) for astar pathfinding.
        /// </summary>
        public int g;
        /// <summary>
        /// temp g value.
        /// </summary>
        public int gTemp;
        /// <summary>
        /// The g value from start to this cell.
        /// </summary>
        public int pathValue;
        /// <summary>
        /// The f(x) = g(x) + h(x) for the astar pathfinding.
        /// </summary>
        public int F { get { return (gTemp + h); } }
        /// <summary>
        /// The cell's parent.
        /// </summary>
        public Node parent;

        public bool monsterHere;
        public bool wizardHere;

        public bool WizardHere
        {
            get { return wizardHere; }
            set
            {
                if (value == false)
                {
                    wizardHere = false;
                    if (myType == MyType.monster)
                    {
                        monsterHere = true;
                        symbole = "M";
                        color = ConsoleColor.DarkRed;
                    }
                }
                if (value == true)
                    wizardHere = true;
            }
        }

        public Node(MyType myType, string symbole)
        {
            this.myType = myType;
            this.symbole = symbole;
            wizardHere = false;
            colorPathfinding = ConsoleColor.White;
        }


    }
}
