using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager : MonoBehaviour
{
    public static AStarManager Instance { get; private set; }

    [field: SerializeField] public List<PathNode> WorldNodes { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    [ContextMenu("CalculateNodesConnections")]
    public void CalculateNodesConnections()
    {
        for (int i = 0; i < WorldNodes.Count; i++)
        {
            WorldNodes[i].Connections.Clear();
            WorldNodes[i].GetNeighbours();
        }
    }
}
