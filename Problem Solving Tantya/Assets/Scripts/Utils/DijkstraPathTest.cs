using System.Collections.Generic;
using UnityEngine;
using Tantya.Utils;

public class DijkstraPathTest : MonoBehaviour
{
    private void Start()
    {
        RunTest();
    }

    private void RunTest()
    {
        // Create graph
        WeightedGraph graph = CreateLargeGraph();

        // Create pathfinder
        DijkstraPathFinder pathfinder = new DijkstraPathFinder();

        // Find shortest path from A to Z
        List<string> path = pathfinder.FindShortestPath(graph, "A", "Z");

        Debug.Log("========== DIJKSTRA STRESS TEST ==========");

        if (path.Count == 0)
        {
            Debug.LogError("No path found!");
            return;
        }

        // Display path
        Debug.Log("Shortest Path: " + string.Join(" -> ", path));

        // Calculate total cost
        float totalCost = CalculatePathCost(graph, path);

        Debug.Log("Total Cost: " + totalCost);
        Debug.Log("Nodes in Path: " + path.Count);

        Debug.Log("========== TEST COMPLETED ==========");
    }

    private WeightedGraph CreateLargeGraph()
    {
        WeightedGraph graph = new WeightedGraph();

        // Create nodes
        string[] nodes =
        {
            "A", "B", "C", "D", "E",
            "F", "G", "H", "I", "J",
            "K", "L", "M", "N", "O",
            "P", "Q", "R", "S", "T",
            "U", "V", "W", "X", "Y", "Z"
        };

        foreach (string node in nodes)
        {
            graph.AddNode(node);
        }

        // Region 1
        graph.AddEdge("A", "B", 8);
        graph.AddEdge("A", "C", 3);
        graph.AddEdge("A", "D", 12);
        graph.AddEdge("B", "C", 2);
        graph.AddEdge("B", "E", 7);
        graph.AddEdge("B", "F", 15);
        graph.AddEdge("C", "D", 4);
        graph.AddEdge("C", "E", 6);
        graph.AddEdge("D", "E", 1);
        graph.AddEdge("D", "G", 10);
        graph.AddEdge("E", "F", 3);
        graph.AddEdge("E", "G", 5);
        graph.AddEdge("F", "G", 2);
        graph.AddEdge("F", "H", 9);
        graph.AddEdge("G", "H", 4);

        // Region 2
        graph.AddEdge("H", "I", 3);
        graph.AddEdge("H", "J", 8);
        graph.AddEdge("I", "J", 2);
        graph.AddEdge("I", "K", 7);
        graph.AddEdge("J", "K", 1);
        graph.AddEdge("J", "L", 6);
        graph.AddEdge("K", "L", 3);
        graph.AddEdge("K", "M", 9);
        graph.AddEdge("L", "M", 2);
        graph.AddEdge("L", "N", 5);
        graph.AddEdge("M", "N", 1);
        graph.AddEdge("M", "O", 8);
        graph.AddEdge("N", "O", 3);

        // Region 3
        graph.AddEdge("O", "P", 4);
        graph.AddEdge("O", "Q", 10);
        graph.AddEdge("P", "Q", 2);
        graph.AddEdge("P", "R", 7);
        graph.AddEdge("Q", "R", 1);
        graph.AddEdge("Q", "S", 6);
        graph.AddEdge("R", "S", 3);
        graph.AddEdge("R", "T", 9);
        graph.AddEdge("S", "T", 2);
        graph.AddEdge("S", "U", 5);
        graph.AddEdge("T", "U", 1);
        graph.AddEdge("T", "V", 8);
        graph.AddEdge("U", "V", 3);

        // Region 4
        graph.AddEdge("V", "W", 4);
        graph.AddEdge("V", "X", 9);
        graph.AddEdge("W", "X", 2);
        graph.AddEdge("W", "Y", 7);
        graph.AddEdge("X", "Y", 1);
        graph.AddEdge("X", "Z", 6);
        graph.AddEdge("Y", "Z", 2);

        // Cross-region connections
        graph.AddEdge("A", "G", 30);
        graph.AddEdge("A", "M", 50);
        graph.AddEdge("B", "J", 25);
        graph.AddEdge("C", "H", 18);
        graph.AddEdge("D", "K", 35);
        graph.AddEdge("E", "L", 22);
        graph.AddEdge("F", "N", 28);
        graph.AddEdge("G", "O", 20);
        graph.AddEdge("H", "P", 40);
        graph.AddEdge("I", "Q", 27);
        graph.AddEdge("J", "R", 33);
        graph.AddEdge("K", "S", 24);
        graph.AddEdge("L", "T", 31);
        graph.AddEdge("M", "U", 26);
        graph.AddEdge("N", "V", 29);
        graph.AddEdge("O", "W", 45);
        graph.AddEdge("P", "X", 19);
        graph.AddEdge("Q", "Y", 38);
        graph.AddEdge("R", "Z", 55);

        // Expensive direct connections
        graph.AddEdge("A", "Z", 500);
        graph.AddEdge("A", "T", 200);
        graph.AddEdge("C", "X", 150);

        // Additional cycles and connections
        graph.AddEdge("B", "D", 11);
        graph.AddEdge("E", "H", 13);
        graph.AddEdge("G", "J", 12);
        graph.AddEdge("I", "L", 8);
        graph.AddEdge("K", "O", 14);
        graph.AddEdge("M", "P", 16);
        graph.AddEdge("O", "R", 11);
        graph.AddEdge("Q", "U", 12);
        graph.AddEdge("S", "W", 10);
        graph.AddEdge("U", "X", 15);

        return graph;
    }

    private float CalculatePathCost(
        WeightedGraph graph,
        List<string> path)
    {
        float totalCost = 0f;

        for (int i = 0; i < path.Count - 1; i++)
        {
            string current = path[i];
            string next = path[i + 1];

            foreach (var edge in graph.GetEdges(current))
            {
                if (edge.Destination == next)
                {
                    totalCost += edge.Cost;
                    break;
                }
            }
        }

        return totalCost;
    }
}