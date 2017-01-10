using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An A* Grid
/// </summary>
public class Grid
{
    /// <summary>
    /// The nodes for this grid.
    /// </summary>
    private readonly Node[,] _grid;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="width">Width of the grid (amount of columns)</param>
    /// <param name="height">Height of the grid (amount ofrows)</param>
    public Grid(int width, int height)
    {
        if (width < 0 || height < 0)
        {
            throw new System.InvalidOperationException("width and height cannot be smaller than zero");
        }

        Width = width;
        Height = height;

        _grid = new Node[Width, Height];

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                var currentPosition = new Vector2(x, y);
                _grid[x, y] = new Node(currentPosition);
            }
        }
    }

    /// <summary>
    /// The width of the grid.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// The height of the grid.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Gets a node from the grid.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Node GetNode(Vector2 position)
    {
        return GetNode((int) position.x, (int) position.y);
    }

    /// <summary>
    /// Gets a node from the grid.
    /// </summary>
    /// <param name="x">x position of the node.</param>
    /// <param name="y">y position of the node.</param>
    /// <returns></returns>
    public Node GetNode(int x, int y)
    {
        if (!IsOnGrid(x, y))
        {
            throw new System.InvalidOperationException("Requested position is out of bounds");
        }
        return _grid[x, y];
    }

    /// <summary>
    /// Gets the neighbours for a specific position.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public List<Node> GetNeighbours(Vector2 position)
    {
        var neighbours = new List<Node>();
        var x = (int) position.x;
        var y = (int) position.y;

        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                if (IsOnGrid(x + i, y + j))
                {
                    neighbours.Add(GetNode(x + i, y + j));
                }
            }
        }

        return neighbours;
    }

    /// <summary>
    /// Resets the A Star values for this grid.
    /// </summary>
    public void Reset()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                _grid[x, y].Reset();
            }
        }
    }

    /// <summary>
    /// Determines if a x & y position is within the grid
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsOnGrid(int x, int y)
    {
        return x >= 0 && y >= 0 && x < Width && y < Height;
    }

    /// <summary>
    /// Determines if a position is within the grid
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool IsOnGrid(Vector2 position)
    {
        return IsOnGrid((int) position.x, (int) position.y);
    }
}