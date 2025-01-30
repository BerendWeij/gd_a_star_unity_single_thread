using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject walkablePrefab;
    [SerializeField] private GameObject blockedPrefab;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private Transform prefabContainer;
    
    private void Start()
    {

        var grid = PathfindingSystem.Instance.Grid;


        for (var x = 0; x < grid.Width; x++)
        {
            for (var y = 0; y < grid.Height; y++)
            {
                var node = grid.GetNode(x, y);
                var spawnPos = new Vector3(x, y, 0f);

                GameObject toSpawn = node.IsWalkable ? walkablePrefab : blockedPrefab;
                
                Instantiate(toSpawn, spawnPos, Quaternion.identity, prefabContainer);
            }
        }
    }
}