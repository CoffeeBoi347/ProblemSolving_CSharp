using System.Collections.Generic;

namespace Tantya.Utils
{
    public class WeightedGraph
    {
        public class Edge
        {
            public string Destination { get; }
            public float Cost { get; }

            public Edge(string destination, float cost)
            {
                Destination = destination;
                Cost = cost;
            }
        }

        private readonly Dictionary<string, List<Edge>> adjacencyList = new();

        public void AddNode(string node)
        {
            if (!adjacencyList.ContainsKey(node))
            {
                adjacencyList[node] = new List<Edge>();
            }
        }

        public void AddEdge(string from, string to, float cost)
        {
            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
                throw new System.ArgumentException("Node names cannot be null or whitespace.");

            if (cost < 0f)
                throw new System.ArgumentOutOfRangeException(nameof(cost), "Cost cannot be negative.");

            AddNode(from);
            AddNode(to);

            if (adjacencyList[from].Exists(edge => edge.Destination == to))
                return;

            adjacencyList[from].Add(new Edge(to, cost));
            adjacencyList[to].Add(new Edge(from, cost));
        }

        public List<Edge> GetEdges(string node)
        {
            if (adjacencyList.TryGetValue(node, out var edges))
            {
                return edges;
            }

            return new List<Edge>();
        }

        public IEnumerable<string> GetNodes()
        {
            return adjacencyList.Keys;
        }

        public bool ContainsNode(string node)
        {
            return adjacencyList.ContainsKey(node);
        }
    }
}