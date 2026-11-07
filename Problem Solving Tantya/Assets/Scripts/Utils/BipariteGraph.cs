using System.Collections.Generic;

namespace Tantya.Utils
{
    public class BipariteGraph
    {
        public List<int>[] adjacencyList;

        public BipariteGraph(int vertices)
        {
            adjacencyList = new List<int>[vertices];
            for (int i = 0; i < vertices; i++)
            {
                adjacencyList[i] = new List<int>();
            }
        }

        public void AddEdge(int u, int v)
        {
            adjacencyList[u].Add(v);
        }
    }
}