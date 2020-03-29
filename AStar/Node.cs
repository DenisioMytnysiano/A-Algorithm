namespace FindWay
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
}
