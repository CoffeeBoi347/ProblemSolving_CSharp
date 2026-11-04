using System.Collections.Generic;

namespace Tantya.Utils
{
    public class DijkstraPathFinder
    {
        public List<string> FindShortestPath(
            WeightedGraph graph,
            string start,
            string end)
        {
            if (graph == null)
                throw new System.ArgumentNullException(nameof(graph));

            if (!graph.ContainsNode(start) || !graph.ContainsNode(end))
                return new List<string>();

            var distances = new Dictionary<string, float>();
            var previous = new Dictionary<string, string>();
            var unvisited = new HashSet<string>();

            foreach (var node in graph.GetNodes())
            {
                distances[node] = float.PositiveInfinity;
                unvisited.Add(node);
            }

            distances[start] = 0f;

            while (unvisited.Count > 0)
            {
                string current = null;
                float smallestDistance = float.PositiveInfinity;

                foreach (var node in unvisited)
                {
                    if (distances[node] < smallestDistance)
                    {
                        smallestDistance = distances[node];
                        current = node;
                    }
                }

                if (current == null)
                    break;

                if (current == end)
                    break;

                unvisited.Remove(current);

                foreach (var edge in graph.GetEdges(current))
                {
                    if (!unvisited.Contains(edge.Destination))
                        continue;

                    float newDistance = distances[current] + edge.Cost;

                    if (newDistance < distances[edge.Destination])
                    {
                        distances[edge.Destination] = newDistance;
                        previous[edge.Destination] = current;
                    }
                }
            }

            if (float.IsPositiveInfinity(distances[end]))
                return new List<string>();

            List<string> path = new List<string>();
            string currentNode = end;

            while (currentNode != start)
            {
                path.Add(currentNode);

                if (!previous.TryGetValue(
                    currentNode, out string previousNode))
                {
                    return new List<string>();
                }

                currentNode = previousNode;
            }

            path.Add(start);
            path.Reverse();

            return path;
        }
    }
}