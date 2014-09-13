using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirtyAStar
{
    /// <summary>
    /// Data struct for node/tile
    /// </summary>
    class AStarNode
    {
        public int x, y, g, f;
        public AStarNode CameFromNode;

        public AStarNode(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    /// <summary>
    /// "Dirty" implementation of the A* algorithm
    /// </summary>
    class DirtyAStarSearch
    {
        /// <summary>
        /// Calculate heuristics value (Manhattan distance)
        /// </summary>
        /// <param name="from">Start Node</param>
        /// <param name="to">End Node</param>
        /// <returns></returns>
        private int Heuristic(AStarNode from, AStarNode to)
        {
            return Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y);
        }

        /// <summary>
        /// Calculates shortest path for a given grid
        /// </summary>
        /// <param name="grid">Grid with passable and not passable cells</param>
        /// <param name="start">Starting node</param>
        /// <param name="goal">Destination node</param>
        /// <returns>Returns a list of nodes of the shortest path or null if can't find any</returns>
        public List<AStarNode> AStarSearch(bool[,] grid, AStarNode start, AStarNode goal)
        {
            List<AStarNode> closedSet = new List<AStarNode>();
            List<AStarNode> openSet = new List<AStarNode>();
            openSet.Add(start);
            List<AStarNode> comeFrom = new List<AStarNode>();

            start.g = 0;
            start.f = start.g + Heuristic(start, goal);

            while (openSet.Count > 0)
            {
                AStarNode current = openSet.OrderBy(x => x.f).First();
                if (current.x == goal.x && current.y == goal.y)
                    return Reconstruct(comeFrom, current);

                openSet.Remove(current);
                closedSet.Add(current);

                // get all neighboring tiles
                List<AStarNode> neighbors = new List<AStarNode>();
                if (current.x > 0 && grid[current.x - 1, current.y])
                    neighbors.Add(new AStarNode(current.x - 1, current.y));
                if (current.x < grid.GetLength(0) - 1 && grid[current.x + 1, current.y])
                    neighbors.Add(new AStarNode(current.x + 1, current.y));
                if (current.y > 0 && grid[current.x, current.y - 1])
                    neighbors.Add(new AStarNode(current.x, current.y - 1));
                if (current.y < grid.GetLength(1) - 1 && grid[current.x, current.y + 1])
                    neighbors.Add(new AStarNode(current.x, current.y + 1));

                foreach (AStarNode neighbor in neighbors)
                {
                    bool inClosedSet = false;
                    foreach (AStarNode test in closedSet)
                        if (test.x == neighbor.x && test.y == neighbor.y)
                        {
                            inClosedSet = true;
                            break;
                        }
                    if (inClosedSet)
                        continue;

                    int tentativeGScore = current.g + 1;

                    bool inOpenSet = false;
                    foreach (AStarNode test in openSet)
                        if (test.x == neighbor.x && test.y == neighbor.y)
                        {
                            inOpenSet = true;
                            break;
                        }

                    if (inOpenSet == false || tentativeGScore < neighbor.g)
                    {
                        neighbor.CameFromNode = current;
                        comeFrom.Add(neighbor);
                        neighbor.g = tentativeGScore;
                        neighbor.f = neighbor.g + Heuristic(neighbor, goal);
                        if (inOpenSet == false)
                            openSet.Add(neighbor);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Reconstructs the path that was taken
        /// </summary>
        /// <param name="cameFrom">Parent node</param>
        /// <param name="currentNode">Current node</param>
        /// <returns>List of the path fragment</returns>
        private List<AStarNode> Reconstruct(List<AStarNode> cameFrom, AStarNode currentNode)
        {
            if (cameFrom.Contains(currentNode))
            {
                List<AStarNode> p = Reconstruct(cameFrom, currentNode.CameFromNode);
                p.Add(currentNode);
                return p;
            }
            else
            {
                List<AStarNode> p = new List<AStarNode>();
                p.Add(currentNode);
                return p;
            }
        }
    }

}
