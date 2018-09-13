using UnityEngine;

public class Heuristic
{

    /// <summary>
    /// Get the H score for a node while using the Manhattan method.
    /// </summary>
    /// <param name="currentPosition"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static float GetManhattan(Vector2 currentPosition, Vector2 target)
    {
        var xOffset = (int) Mathf.Abs(target.x - currentPosition.x);
        var yOffset = (int) Mathf.Abs(target.y - currentPosition.y);
        return xOffset + yOffset;
    }

    /// <summary>
    /// Get the H score for a node while using the Euclidean method.
    /// </summary>
    /// <param name="currentPosition"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static float GetEuclidean(Vector2 currentPosition, Vector2 target)
    {
        return 0;
    }
}