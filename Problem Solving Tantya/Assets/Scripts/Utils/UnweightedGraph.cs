using System.Collections.Generic;

namespace Tantya.Utils
{
    public class UnweightedGraph
    {
        private readonly Dictionary<string, List<string>> adjacencyList = new();

        public void AddNode(string node)
        {
            if (string.IsNullOrWhiteSpace(node))
                throw new System.ArgumentException("Node name cannot be null or whitespace.");

            if (!adjacencyList.ContainsKey(node))
            {
                adjacencyList[node] = new List<string>();
            }
        }

        public void AddEdge(string from, string to)
        {
            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
                throw new System.ArgumentException("Node names cannot be null or whitespace.");

            AddNode(from);
            AddNode(to);

            if (!adjacencyList[from].Contains(to))
            {
                adjacencyList[from].Add(to);
            }
            if (!adjacencyList[to].Contains(from))
            {
                adjacencyList[to].Add(from);
            }
        }

        public List<string> GetNeighbors(string node)
        {
            if (!adjacencyList.TryGetValue(node, out var neighbors))
            {
                return new List<string>();
            }

            return neighbors;
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