using System;
using System.Collections.Generic;
using System.Linq;

namespace FindWay
{
    class AStar
    {
        private List<List<char>> maze;

        public AStar(string fileName)
        {
            Parser parser = new Parser(fileName);
            maze = parser.ParseMaze();
        }

        private List<Node> GetChildren(int x, int y, List<List<char>> maze, Node end_n)
        {
            var children = new List<Node> { new Node(x, y - 1), new Node(x, y + 1), new Node(x - 1, y), new Node(x + 1, y) };
            return children.Where(l => maze[l.x][l.y] != Convert.ToChar("x") || (l.x == end_n.x && l.y == end_n.y)).ToList();
        }


        private List<List<char>> FindWay(List<int> start, List<int> end)
        {
            List<List<char>> way = this.maze;
            Node current_node = null;
            var start_node = new Node(start[0], start[1]);
            var end_node = new Node(end[0], end[1]);
            var openList = new BinaryHeap();
            var closedList = new List<Node> { };
            int g = 0;
            openList.Insert(start_node);

            while (!openList.Empty())
            {
                current_node = openList.ExtractMin();
                closedList.Add(current_node);
                if (closedList.FirstOrDefault(n => n.x == end_node.x && n.y == end_node.y) != null) 
                    break;

                List<Node> children = GetChildren(current_node.x, current_node.y, way, end_node);
                ++g;
                foreach (var child in children)
                {
                    if (closedList.FirstOrDefault(n => n.x == child.x && n.y == child.y) != null)
                    {
                        continue;
                    }
                    if (openList.items.FirstOrDefault(n => n.x == child.x && n.y == child.y) == null)
                    {
                        child.g = g;
                        child.h = Math.Abs(end_node.x - child.x) + Math.Abs(end_node.y - child.y);
                        child.f = child.g + child.h;
                        child.parent = current_node;
                        openList.Insert(child);
                    }
                    else
                    {
                        if (g + child.h < child.f)
                        {
                            child.g = g;
                            child.f = child.g + child.h;
                            child.parent = current_node;
                        }
                    }

                }
            }
            if (current_node.x == end_node.x && current_node.y == end_node.y)
            {
                List<List<int>> path = new List<List<int>> { };

                while (current_node != null)
                {
                    path.Add(new List<int> { current_node.x, current_node.y });
                    current_node = current_node.parent;
                }
                path.Reverse();

                List<char> letters = "123456789abcdefghijklmnopqrstuvwxyz".ToCharArray().ToList();
                for (int i = 0; i < path.Count; ++i)
                {
                    way[path[i][0]][path[i][1]] = letters[i];
                }

                return way;
            }
            else
            {
                return null;
            }
        }

        private bool CoordsIsValid(List<int> start, List<int> end)
        {
            return ((start[0] >= 0 && start[0] < maze.Count) &&
                   (start[1] >= 0 && start[1] < maze[0].Count) &&
                   (end[0] >= 0 && end[0] < maze.Count) &&
                   (end[1] >= 0 && end[1] < maze[0].Count) &&
                   (maze[start[0]][start[1]] != 'x' && maze[end[0]][end[1]] != 'x'));
        }

        public void PrintWay(List<int> start, List<int> end)
        {
            if (!CoordsIsValid(start, end))
            {
                Console.WriteLine("Coordinates are not correct!");
                return;
            }

            List<List<char>> way = FindWay(start, end);
            foreach (var c in way)
            {
                foreach (var elem in c)
                {
                    Console.Write(elem);
                }
                Console.WriteLine();
            }
        }
    }
}
