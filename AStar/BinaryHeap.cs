using System.Collections.Generic;

namespace FindWay
{
    class BinaryHeap
    {
        public readonly List<Node> items = new List<Node> { };

        public int parent(int index) { return (index - 1) / 2; }
        public void SiftUp(int i)
        {
            while (i != 0 && items[parent(i)].f > items[i].f)
            {
                (items[i], items[parent(i)]) = (items[parent(i)], items[i]);
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
                (items[i], items[j]) = (items[j], items[i]);
            }
        }

        public void Insert(Node k)
        {
            items.Add(k);
            SiftUp(items.Count - 1);
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
                SiftDown(0);
                return tmp;
            }
        }

        public bool Empty()
        {
            return items.Count == 0;
        }

        public static void Heapify(List<Node> arr, BinaryHeap heap)
        {
            foreach (var elem in arr)
            {
                heap.Insert(elem);
            }
        }
    }
}
