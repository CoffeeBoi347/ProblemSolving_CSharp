using System.Collections.Generic;

namespace Tantya.Utils
{
    public class BFSPathfinder
    {
        public List<string> FindShortestPath(WeightedGraph graph, string start, string goal)
        {
            if (graph == null)
            {
                throw new System.ArgumentNullException(nameof(graph), "Graph cannot be null.");
            }

            if (!graph.ContainsNode(start) || !graph.ContainsNode(goal))
            {
                return new List<string>();
            }

            if (string.IsNullOrWhiteSpace(start) || string.IsNullOrWhiteSpace(goal))
            {
                throw new System.ArgumentException("Start and goal nodes cannot be null or whitespace.");
            }

            Queue<string> queue = new Queue<string>(); // unvisited
            HashSet<string> visited = new HashSet<string>(); // visited
            Dictionary<string, string> cameFrom = new Dictionary<string, string>(); // to reconstruct the path

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                if (current == goal)
                {
                    break; // Found the goal
                }

                foreach (var edge in graph.GetEdges(current))
                {
                    string neighbor = edge.Destination;

                    if (visited.Contains(neighbor))
                    {
                        continue; // Skip already visited nodes
                    }

                    visited.Add(neighbor);
                    cameFrom[neighbor] = current; // Track the path
                    queue.Enqueue(neighbor); // For the next iteration
                }
            }

            if (!visited.Contains(goal))
            {
                return new List<string>(); // No path found
            }

            List<string> path = new List<string>();
            string currentNode = goal;

            while (currentNode != start)
            {
                path.Add(currentNode);

                if (!cameFrom.TryGetValue(currentNode, out string previousNode))
                {
                    return new List<string>(); // No path found
                }

                currentNode = previousNode;
            }

            path.Add(start);
            path.Reverse();
            return path; // Return the reconstructed path
        }

        public List<string> FindShortestUnweightedPath(UnweightedGraph graph, string start, string goal)
        {
            if (graph == null)
                throw new System.ArgumentNullException(nameof(graph));

            if (!graph.ContainsNode(start) ||
                !graph.ContainsNode(goal))
                return new List<string>();

            Queue<string> queue = new Queue<string>();
            HashSet<string> visited = new HashSet<string>();
            Dictionary<string, string> cameFrom = new Dictionary<string, string>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();

                if (current == goal)
                    break;

                foreach (string neighbor in graph.GetNeighbors(current))
                {
                    if (visited.Contains(neighbor))
                        continue;

                    visited.Add(neighbor);
                    cameFrom[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }

            if (!visited.Contains(goal))
                return new List<string>();

            List<string> path = new List<string>();
            string currentNode = goal;

            while (currentNode != start)
            {
                path.Add(currentNode);

                if (!cameFrom.TryGetValue(currentNode, out string previousNode))
                    return new List<string>();

                currentNode = previousNode;
            }

            path.Add(start);
            path.Reverse();

            return path;
        }
    }
}