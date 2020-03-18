using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        static void Main(string[] args)
        {
        }
    }
}
