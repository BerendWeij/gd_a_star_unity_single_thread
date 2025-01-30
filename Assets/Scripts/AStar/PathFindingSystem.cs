using System.Collections.Generic;
using UnityEngine;

public class PathfindingSystem : MonoBehaviour
{
    public static PathfindingSystem Instance { get; private set; }

    [SerializeField] private int width = 50;
    [SerializeField] private int height = 50;
    
    private Grid _grid;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        } 
        else 
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // Grid init
        _grid = new Grid(width, height);
        
        BlockColliders();
    }

    /// <summary>
    /// Vraag een pad op met nodes
    /// </summary>
    public List<Node> GetPath(Vector2Int startPos, Vector2Int endPos)
    {
        return _grid.FindPath(startPos, endPos);
    }
    
    /// <summary>
    /// Vraag een pad op met Vector2
    /// </summary>
    public List<Vector2> GetPathPositions(Vector2Int startPos, Vector2Int endPos)
    {
        return _grid.FindPathPositions(startPos, endPos);
    }

    public void BlockColliders()
    {
        var allColliders = FindObjectsOfType<Collider>();

        foreach (var currentCollider in allColliders)
        {
            var bounds = currentCollider.bounds;
            var min = bounds.min;
            var max = bounds.max;

            var minX = Mathf.FloorToInt(min.x);
            var maxX = Mathf.CeilToInt(max.x);
            var minY = Mathf.FloorToInt(min.y);
            var maxY = Mathf.CeilToInt(max.y);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    _grid.SetWalkable(x, y, false);
                }
            }
        }
    }
    
    public Grid Grid => _grid;

}