using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Eenvoudig script om een NPC langs een reeks nodes te bewegen.
/// </summary>
public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float arrivalThreshold = 0.1f;
    private Vector2Int destination;

    private List<Node> _currentPath;
    private int _pathIndex = 0;
    private Color _gizmoColor;
    private Vector2Int _startPos;

    private void Start()
    {
        _gizmoColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        ChooseNewPath();
    }

    private void ChooseNewPath()
    {
        ChooseDestination();
        GetPath();
    }

    private void GetPath()
    {
        // Bepaal startPos
        _startPos = new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y)
        );

        // Haal pad op bij je singleton-service
        _currentPath = PathfindingSystem.Instance.GetPath(_startPos, destination);
        _pathIndex = 0;
    }

    public void ChooseDestination()
    {
        var destinationNode = PathfindingSystem.Instance.Grid.GetRandomWalkableNode();
        destination = new Vector2Int((int)destinationNode.Position.x, (int)destinationNode.Position.y);
    }

    private void Update()
    {
        if (_currentPath == null || _currentPath.Count == 0)
            return;

        if (_pathIndex >= _currentPath.Count)
        {
            ChooseNewPath();
            return; // Het eind van het pad is bereikt
        }

        // Haal de volgende node op
        var targetNode = _currentPath[_pathIndex];
        var targetPos = new Vector3(targetNode.Position.x, targetNode.Position.y, transform.position.z);

        // Bepaal richting en verplaats
        var direction = (targetPos - transform.position).normalized;
        transform.position += direction * (speed * Time.deltaTime);

        var distance = Vector3.Distance(transform.position, targetPos);
        if (distance < arrivalThreshold)
        {
            // Naar de volgende node in de lijst
            _pathIndex++;
        }
    }
    
    private void OnDrawGizmos()
    {
        if (_currentPath == null || _currentPath.Count < 2)
            return;

        Gizmos.color = _gizmoColor;

        for (var i = 0; i < _currentPath.Count - 1; i++)
        {
            var startPos = new Vector3(_currentPath[i].Position.x, _currentPath[i].Position.y, transform.position.z);
            var endPos = new Vector3(_currentPath[i + 1].Position.x, _currentPath[i + 1].Position.y, transform.position.z);

            Gizmos.DrawLine(startPos, endPos);
        }
    }
}