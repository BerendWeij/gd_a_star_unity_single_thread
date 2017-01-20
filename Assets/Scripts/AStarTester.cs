using System.Collections.Generic;
using UnityEngine;

public class AStarTester : MonoBehaviour
{
    /// <summary>
    /// Test our A* pathfinding.
    /// </summary>
    void Start()
    {
        // our start & end point for our player
        var startPoint = new Vector2(0, 0);
        var endPoint = new Vector2(60, 50);

        // Lets create our first grid
        var testGrid = new Grid(200, 200);

        // We will change some nodes to blocking state
        testGrid.GetNode(4, 4).IsWalkable = false;
        testGrid.GetNode(4, 5).IsWalkable = false;
        testGrid.GetNode(4, 6).IsWalkable = false;

        // Find a path from start to end
        var path = testGrid.FindPath(startPoint, endPoint);

        // Display the found path
        path.ForEach(node => Debug.Log(node.Position));


    }
}