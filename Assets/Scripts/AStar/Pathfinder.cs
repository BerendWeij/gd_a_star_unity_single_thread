using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Star pathfinding algorithm.
/// </summary>
public class Pathfinder
{

    private static float defaultScore = 1f;

    private static float diagonalScore = 1.5f;

    /// <summary>
    /// Find a path from startPoint to endPoint in a grid.
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    /// <param name="grid"></param>
    /// <returns></returns>
    public static List<Node> FindPath(Vector2 startPoint, Vector2 endPoint, Grid grid)
    {
        var openList = new List<Node>();
        var closedList = new List<Node>();
        var currentNode = grid.GetNode(startPoint);

        openList.Add(currentNode);

        grid.Reset();

        while (openList.Count > 0)
        {
            openList.Sort((a, b) => a.F.CompareTo(b.F));

            currentNode = openList[0];

            if (currentNode.Position == endPoint)
            {
                return GetTraversedPath(currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            var neighbours = grid.GetNeighbours(currentNode.Position);

            neighbours.ForEach(neighbour =>
            {
                if (openList.Contains(neighbour) || closedList.Contains(neighbour) ||
                    !neighbour.IsWalkable)
                {
                    return;
                }

                SetNodeValues(neighbour, currentNode, endPoint);
                openList.Add(neighbour);
            });
        }

        return null;
    }

    /// <summary>
    /// Get the G score for a neighbour.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="neighbour"></param>
    /// <returns></returns>
    private static float GetGScore(Node node, Node neighbour)
    {
        if (node.Position.x != neighbour.Position.x && node.Position.y != neighbour.Position.y)
        {
            return node.G + diagonalScore;
        }

        return node.G + defaultScore;
    }

    /// <summary>
    /// Get the H score for a node.
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    private static int GetHeuristic(Vector2 p1, Vector2 p2)
    {
        var xOffset = (int) Mathf.Abs(p2.x - p1.x);
        var yOffset = (int) Mathf.Abs(p2.y - p1.y);
        return xOffset + yOffset;
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
    /// Sets the A* values for a node
    /// </summary>
    /// <param name="targetNode"></param>
    /// <param name="currentNode"></param>
    /// <param name="endPoint"></param>
    private static void SetNodeValues(Node targetNode, Node currentNode, Vector2 endPoint)
    {
        targetNode.G = GetGScore(currentNode, targetNode);
        targetNode.H = GetHeuristic(targetNode.Position, endPoint);
        targetNode.F = targetNode.G + targetNode.H;
        targetNode.Parent = currentNode;
    }
}