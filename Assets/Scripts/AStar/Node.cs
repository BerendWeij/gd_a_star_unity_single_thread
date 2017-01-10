using UnityEngine;

/// <summary>
/// An A* Node.
/// </summary>
public class Node
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="position"></param>
    public Node(Vector2 position)
    {
        Position = position;
        IsWalkable = true;
    }

    /// <summary>
    /// Reset the A* values for this Node.
    /// </summary>
    public void Reset()
    {
        G = 0;
        H = 0;
        F = 0;
        Parent = null;
    }

    /// <summary>
    /// The movement score.
    /// </summary>
    public float G { get; set; }

    /// <summary>
    /// The heuristic score.
    /// </summary>
    public float H { get; set; }

    /// <summary>
    /// The F score.
    /// </summary>
    public float F { get; set; }

    public bool IsWalkable { get; set; }

    /// <summary>
    /// The parent node in a path.
    /// </summary>
    public Node Parent { get; set; }

    /// <summary>
    /// The current position.
    /// </summary>
    public Vector2 Position { get; set; }
}