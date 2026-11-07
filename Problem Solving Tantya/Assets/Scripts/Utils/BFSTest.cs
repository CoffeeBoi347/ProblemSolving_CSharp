using UnityEngine;

namespace Tantya.Utils
{
    public class BFSTest : MonoBehaviour
    {
        void Start()
        {
            UnweightedGraph graph = new UnweightedGraph();
            graph.AddEdge("A", "B");
            graph.AddEdge("A", "C");
            graph.AddEdge("B", "D");
            graph.AddEdge("C", "D");
            graph.AddEdge("D", "E");
            graph.AddEdge("E", "F");

            BFSPathfinder pathfinder = new BFSPathfinder();
            var path = pathfinder.FindShortestUnweightedPath(graph, "A", "F");
            Debug.Log($"BFS Path: {string.Join(" -> ", path)}"); 
        }
    }
}