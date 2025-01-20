using UnityEngine;

public class AStarTester : MonoBehaviour
{
    [SerializeField] private GameObject blockedPrefab;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private GameObject walkablePrefab;
    
    [SerializeField] private Transform prefabContainer;

    private Grid _testGrid;

    void Start()
    {
        // Onze start- en eindpositie
        var startPoint = new Vector2(0, 0);
        var endPoint = new Vector2(46, 45);

        // Maak het grid
        _testGrid = new Grid(50, 50);

        // Blokkeer bepaalde nodes
        BlockColliders();

        // Vind een pad van start naar eind
        var path = _testGrid.FindPath(startPoint, endPoint);

        // Log de knooppunten van het gevonden pad
        path.ForEach(node => Debug.Log($"Pad-node: {node.Position}"));

        // De code hieronder is alleen voor debugging: hiermee visualiseer je het grid
        for (int x = 0; x < _testGrid.Width; x++)
        {
            for (int y = 0; y < _testGrid.Height; y++)
            {
                var currentNode = _testGrid.GetNode(x, y);
                var spawnPos = new Vector3(x, y, 0f);

                // Kies het juiste prefab
                var targetPrefab = walkablePrefab;
                if (!currentNode.IsWalkable)
                {
                    targetPrefab = blockedPrefab;
                }
                else if (path.Contains(currentNode))
                {
                    targetPrefab = pathPrefab;
                }

                // Plaats de prefab in de scene, als child van 'prefabContainer'
                Instantiate(targetPrefab, spawnPos, Quaternion.identity, prefabContainer);
            }
        }
    }
    
    private void BlockColliders()
    {
        var allColliders = FindObjectsOfType<Collider>();

        foreach (var currentCollider in allColliders)
        {
            Bounds bounds = currentCollider.bounds;
            Vector3 min = bounds.min;
            Vector3 max = bounds.max;

            var minX = Mathf.FloorToInt(min.x);
            var maxX = Mathf.CeilToInt(max.x);
            var minY = Mathf.FloorToInt(min.y);
            var maxY = Mathf.CeilToInt(max.y);

            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    _testGrid.SetWalkable(x, y, false);
                }
            }
        }
    }
}