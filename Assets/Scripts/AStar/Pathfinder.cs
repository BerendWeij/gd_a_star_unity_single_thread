using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A Star pathfinding algorithm.
/// </summary>
public static class PathFinder
{
    private static float defaultScore = 1f;

    private static float diagonalScore = 1.5f;

    /// <summary>
    /// Find a path from startPoint to endPoint in a grid.
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    /// <param name="grid"></param>
    /// <param name="heuristic"></param>
    /// <returns></returns>
    public static List<Node> FindPath(Vector2 startPoint, Vector2 endPoint, Grid grid,
        Func<Vector2, Vector2, float> heuristic = null)
    {
        var openList = new List<Node>();
        var closedList = new List<Node>();
        var currentNode = grid.GetNode(startPoint);

        grid.Reset();

        openList.Add(currentNode);

        while (openList.Count > 0)
        {
            openList.Sort((a, b) => a.F.CompareTo(b.F));

            currentNode = openList[0];

            if (currentNode.Position == endPoint)
                return GetTraversedPath(currentNode);

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (var neighbour in grid.GetNeighbours(currentNode.Position)
                .Where(n => n.IsWalkable && !closedList.Contains(n)))
            {
                UpdateNodeValues(neighbour, currentNode, endPoint, heuristic);

                if (!openList.Contains(neighbour))
                    openList.Add(neighbour);
            }
        }

        return new List<Node>();
    }

    /// <summary>
    /// Get the G score for a targetNode.
    /// </summary>
    /// <param name="parentNode"></param>
    /// <param name="targetNode"></param>
    /// <returns></returns>
    private static float GetGScore(Node parentNode, Node targetNode)
    {
        if (parentNode.Position.x != targetNode.Position.x && parentNode.Position.y != targetNode.Position.y)
            return parentNode.G + diagonalScore;

        return parentNode.G + defaultScore;
    }

    /// <summary>
    /// Gets the path by traversing back through parent nodes
    /// </summary>
    /// <param name="currentNode"></param>
    /// <returns></returns>
    private static List<Node> GetTraversedPath(Node currentNode)
    {
        var path = new List<Node>();
        while (currentNode.Parent != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Reverse();
        return path;
    }

    /// <summary>
    /// Update the A* values for a node when not set or when g value is smaller.
    /// </summary>
    /// <param name="targetNode"></param>
    /// <param name="currentNode"></param>
    /// <param name="endPoint"></param>
    /// <param name="heuristic"></param>
    private static void UpdateNodeValues(Node targetNode, Node currentNode, Vector2 endPoint, Func<Vector2, Vector2, float> heuristic)
    {
        if (heuristic == null)
            heuristic = Heuristic.GetManhattan;

        var gScore = GetGScore(currentNode, targetNode);

        if (targetNode.G > 0 && gScore >= targetNode.G)
            return;

        targetNode.G = gScore;
        targetNode.H = heuristic(targetNode.Position, endPoint);
        targetNode.F = targetNode.G + targetNode.H;
        targetNode.Parent = currentNode;
    }
}