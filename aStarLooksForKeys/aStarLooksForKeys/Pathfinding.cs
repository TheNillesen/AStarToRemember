using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aStarLooksForKeys
{
    static class Pathfinding
    {
        static public List<Node> AStar(Node start, Node goal, ref Map grid)
        {
            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();

            open.Add(start);

            //Finds the cell from the open list, which has the lowest f(x) value. This cell is moved to the closed list..
            Node current = open[0];

            //Directional multiplicator.
            int directionMulti;
            //The values for moving linear or cross
            int linear = 10;
            int cross = 14;

            //colors for the different cells, coorosponding to how the pathfinder looks at them
            ConsoleColor openColor = ConsoleColor.Blue;
            ConsoleColor closedColor = ConsoleColor.Red;
            ConsoleColor pathColor = ConsoleColor.DarkBlue;

            while (open.Count() > 0 && (current = open[0]) != goal)
            {
                //Adds the current to the closed list and removes it from the open list.
                closed.Add(current);
                open.RemoveAt(0);
                //Sets color
                current.colorPathfinding = closedColor;

                //Runs through all the surrounding cells.
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        int xCord = current.position.X + x;
                        int yCord = current.position.Y + y;
                        if (xCord < 0)
                            xCord = 0;
                        if (yCord < 0)
                            yCord = 0;
                        if (xCord >= grid.nodes.GetLength(0) - 1)
                            xCord = grid.nodes.GetLength(0) - 1;
                        if (yCord >= grid.nodes.GetLength(1) - 1)
                            yCord = grid.nodes.GetLength(1) - 1;

                        Node cellTemp = grid.nodes[xCord, yCord];

                        //If the cell is not walkable or is in the closed list, then it's ignored.
                        if (cellTemp.myType != MyType.notWalkable && !closed.Contains(cellTemp) && cellTemp.monsterHere != true)
                        {
                            //If the cell isn't in the open list, then it's added.
                            if (!open.Contains(cellTemp))
                            {
                                open.Add(cellTemp);
#if DEBUG
                                //Sets the color
                                cellTemp.colorPathfinding = openColor;
#endif
                                //The current cell is made the parent cell.
                                cellTemp.parent = current;

                                //G value is calculated.
                                if ((x == -1 && y == -1) || (x == -1 && y == 1) || (x == 1 && y == -1) || (x == 1 && y == 1))
                                    directionMulti = cross;
                                else
                                    directionMulti = linear;
                                //The g value
                                int gTemp = cellTemp.g * directionMulti;

                                //The path value
                                cellTemp.pathValue = current.pathValue + gTemp;
                                //Sets a temp g value on the cell
                                cellTemp.gTemp = cellTemp.g * directionMulti;

                                //H value is calculaed.
                                cellTemp.h = CalcH(cellTemp.position, goal.position, linear, cross);
                            }
                            else
                            {
                                //G value is calculated.
                                if ((x == -1 && y == -1) || (x == -1 && y == 1) || (x == 1 && y == -1) || (x == 1 && y == 1))
                                    directionMulti = cross;
                                else
                                    directionMulti = linear;
                                //The g value
                                int gTemp = cellTemp.g * directionMulti;

                                if (gTemp < cellTemp.gTemp)
                                {
                                    //The path value
                                    cellTemp.pathValue = current.pathValue + cellTemp.g * directionMulti;
                                    //Sets a temp g value on the cell
                                    cellTemp.gTemp = cellTemp.g * directionMulti;

                                    //H value is calculaed.
                                    cellTemp.h = CalcH(cellTemp.position, goal.position, linear, cross);

#if DEBUG
                                    //Sets the color
                                    cellTemp.colorPathfinding = openColor;
#endif
                                    //The current cell is made the parent cell.
                                    cellTemp.parent = current;
                                }
                            }
                        }
                    }
                }
                //Sorts the open list so the cell with the largest F value is at index 0.
                InsertionSort(ref open);

#if DEBUG
                //Only for bug testing
                Stopwatch stopwatch = Stopwatch.StartNew();
                int millisecondsToWait = 500;
                grid.Render(true);
                while (true)
                {
                    //some other processing to do STILL POSSIBLE
                    if (stopwatch.ElapsedMilliseconds >= millisecondsToWait)
                    {
                        break;
                    }
                    Thread.Sleep(1); //so processor can rest for a while
                }
#endif
            }

            //Finds the path
            List<Node> path = new List<Node>();
            bool run = true;
            while (run)
            {
#if DEBUG
                //Sets color
                current.colorPathfinding = pathColor;
#endif
                path.Add(current);
                if (current.parent == null)
                    break;
                current = current.parent;
                if (current.wizardHere)
                    break;
            }
            path.Reverse();
            return path;
        }

        static public Queue<Node> AStarQueue(Node start, Node goal, ref Map grid)
        {
            //Finds the path
            List<Node> path = Pathfinding.AStar(start, goal, ref grid);

            //Queue
            Queue<Node> pathQ = new Queue<Node>();

            for(int i = 0; i < path.Count(); i++)
            {
                pathQ.Enqueue(path[i]);
            }

            return pathQ;
        }



        /// <summary>
        /// Sorts the list after the cells F value..
        /// </summary>
        /// <param name="L"></param>
        static private void InsertionSort(ref List<Node> L)
        {
            for (int i = 1; i < L.Count(); i++)
            {
                Node cell = L[i];
                int pointer = i;
                while (pointer > 0 && cell.F < L[pointer - 1].F)
                {
                    L[pointer] = L[pointer - 1];
                    pointer = pointer - 1;
                }
                L[pointer] = cell;
            }
        }

        /// <summary>
        /// Calculates the h value.
        /// </summary>
        /// <param name="pointPos"></param>
        /// <param name="goalPos"></param>
        /// <param name="linear"></param>
        /// <param name="cross"></param>
        /// <returns></returns>
        static private int CalcH(Position pointPos, Position goalPos, int linear, int cross)
        {
            Position pointTemp = pointPos;
            Position goalTemp = goalPos;
            Position diffTemp = new Position(Math.Abs(pointTemp.X - goalTemp.X), Math.Abs(pointTemp.Y - goalTemp.Y));
            int hTemp = 0;
            if (diffTemp.X > diffTemp.Y)
            {
                hTemp += diffTemp.Y * cross;
                hTemp += (diffTemp.X - diffTemp.Y) * linear;
            }
            else
            {
                hTemp += diffTemp.X * cross;
                hTemp += (diffTemp.Y - diffTemp.X) * linear;
            }
            return hTemp;
        }
    }
}
