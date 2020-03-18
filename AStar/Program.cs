using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AStar
{
    class Node
    {
        public int x;
        public int y;
        public int f;
        public int h;
        public int g;
        public Node parent;
        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class BinaryHeap
    {
        public List<Node> items = new List<Node> { };

        public int parent(int index) { return (index - 1) / 2; }
        public void SiftUp(int i)
        {
            while (i != 0 && items[parent(i)].f > items[i].f)
            {
                Node tmp = items[i];
                items[i] = items[parent(i)];
                items[parent(i)] = tmp;
            }
        }
        public void SiftDown(int i)
        {
            while (2 * i + 1 < items.Count)
            {
                int j = i;
                if (items[2 * i + 1].f < items[i].f)
                {
                    j = 2 * i + 1;
                }
                if (2 * i + 2 < items.Count && items[2 * i + 2].f < items[j].f)
                {
                    j = 2 * i + 2;
                }
                if (i == j) break;
                Node tmp = items[i];
                items[i] = items[j];
                items[j] = tmp;
            }
        }
        public void Insert(Node k)
        {

            items.Add(k);
            this.SiftUp(items.Count - 1);

        }
        public Node ExtractMin()
        {
            if (items.Count == 0)
            {
                return null;
            }
            else
            {
                Node tmp = items[0];
                items[0] = items[items.Count - 1];
                items.RemoveAt(0);
                this.SiftDown(0);
                return tmp;
            }
        }
        public bool IsEmpty
        {
            get
            {
                return items.Count == 0;
            }
        }
        public static void Heapify(List<Node> arr, BinaryHeap heap)
        {
            foreach (var elem in arr)
            {
                heap.Insert(elem);
            }
        }
    }

    class Parser
    {
        private readonly string file;
        public Parser(string file)
        {
            this.file = file;
        }
        public List<List<char>> ParseMaze()
        {
            List<List<char>> maze = new List<List<char>> { };
            StreamReader sr = new StreamReader(file);
            while (!sr.EndOfStream)
            {
                maze.Add(sr.ReadLine().ToCharArray().ToList());
            }
            return maze;
        }
    };

    


    class Program
    {
        static List<Node> GetChildren(int x, int y, List<List<char>> maze, Node end_n)
        {
            var children = new List<Node> { new Node(x, y - 1), new Node(x, y + 1), new Node(x - 1, y), new Node(x + 1, y) };
            return children.Where(l => maze[l.x][l.y] != Convert.ToChar("x") || (l.x == end_n.x && l.y == end_n.y)).ToList();
        }
        public static List<List<char>> AStar(List<int> start, List<int> end, List<List<char>> Maze)
        {
            List<List<char>> maze = Maze;
            Node current_node = null;
            var start_node = new Node(start[0], start[1]);
            var end_node = new Node(end[0], end[1]);
            var openList = new BinaryHeap();
            var closedList = new List<Node> { };
            int g = 0;
            openList.Insert(start_node);
            while (!openList.IsEmpty)
            {

                current_node = openList.ExtractMin();
                closedList.Add(current_node);


                if (closedList.FirstOrDefault(n => n.x == end_node.x && n.y == end_node.y) != null) break;
                List<Node> children = GetChildren(current_node.x, current_node.y, maze, end_node);
                g++;
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

                    maze[path[i][0]][path[i][1]] = letters[i];
                }

                return maze;
            }
            else
            {
                return null;
            }
        }
        static void Main(string[] args)
        {
            string file = "maze.txt";
            Parser parser = new Parser(file);
            List<List<char>> maze = parser.ParseMaze();
            List<int> start = new List<int> { 5, 1 };
            List<int> end = new List<int> { 1, 6 };
            List<List<char>> Maze = AStar(start, end, maze);
            foreach (var c in maze)
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
