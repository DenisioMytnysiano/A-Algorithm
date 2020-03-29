using System;
using System.Collections.Generic;
using System.Linq;

namespace FindWay
{
    class Program
    {
        static void Main(string[] args)
        {
            AStar searcher = new AStar("maze.txt");
            Console.WriteLine("Type the coordinates of the start position with a space : ");
            List<int> start = Console.ReadLine().Split().Select(int.Parse).ToList();
            Console.WriteLine("Type the coordinates of the end position with a space : ");
            List<int> end = Console.ReadLine().Split().Select(int.Parse).ToList();
            searcher.PrintWay(start, end);
        }
    }
}
