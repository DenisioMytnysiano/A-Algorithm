using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FindWay
{
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
            using (StreamReader sr = new StreamReader(file))
                while (!sr.EndOfStream)
                {
                    maze.Add(sr.ReadLine().ToCharArray().ToList());
                }
            return maze;
        }
    };
}
