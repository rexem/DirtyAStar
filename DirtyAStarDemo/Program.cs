using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirtyAStar;

namespace DirtyAStarDemo
{
    /// <summary>
    /// DirtyAStarSearch class usage demo
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            DirtyAStarSearch search = new DirtyAStarSearch();

            // create a test grid
            bool[,] grid = new bool[5, 5];
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 5; x++)
                    grid[x, y] = true;

            grid[2, 1] = false;
            grid[2, 2] = false;
            grid[2, 3] = false;

            // do the shortest path search
            List<AStarNode> path = search.AStarSearch(grid, new AStarNode(0, 2), new AStarNode(4, 2));

            // if we have a path, print each step
            if (path != null)
                foreach (AStarNode node in path)
                    Console.WriteLine("{0}:{1}", node.x, node.y);
            else
                Console.WriteLine("Path not found.");

            Console.ReadKey();

        }
    }
}
