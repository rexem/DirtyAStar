DirtyAStar
==========

A* search algorithm implementation in C#

Usage:
```c#
DirtyAStarSearch search = new DirtyAStarSearch();


bool[,] grid = new bool[5, 5]; // create and fill the grid

// grid[x, y] = true/false;

List<AStarNode> path = search.AStarSearch(grid, new AStarNode(0, 2), new AStarNode(4, 2));

if (path != null)
{
  // found the shortest path
}
```
