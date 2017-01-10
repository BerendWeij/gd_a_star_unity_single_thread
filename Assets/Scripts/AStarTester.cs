using UnityEngine;

public class AStarTester : MonoBehaviour
{
    /// <summary>
    /// Test our A* pathfinding.
    /// </summary>
    void Start()
    {
        // First create our pathfinder instance
        var pathFinder = new Pathfinder();

        // our start & end point for our player
        var startPoint = new Vector2(0, 0);
        var endPoint = new Vector2(6, 5);

        // Lets create our first grid
        var testGrid = new Grid(20, 20);

        // We will change some nodes to blocking state
        testGrid.GetNode(4, 4).IsWalkable = false;
        testGrid.GetNode(4, 5).IsWalkable = false;
        testGrid.GetNode(4, 6).IsWalkable = false;

        // Find a path from start to end
        var path = pathFinder.FindPath(startPoint, endPoint, testGrid);

        // Display the found path
        path.ForEach(node => Debug.Log(node.Position));
    }
}