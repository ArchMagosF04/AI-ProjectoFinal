using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public List<PathNode> Connections;

    public Vector3 Position { get; private set; }

    public int GCost; //The Weight of the node, the cost of moving to this node.
    public int HCost; //Cost determined by its distance to the end node.

    public PathNode cameFrom;
    public bool IsBlocked;

    [Header("Materials")]
    [SerializeField] private Material openHex;
    [SerializeField] private Material closedHex;

    [Header("AdjecentDetection")]

    [SerializeField] private float nodeDetectionRadius = 5f;
    [SerializeField] private LayerMask nodeLayer;

    [Header("Debug")]

    [SerializeField] private bool debugConnections = true;
    [SerializeField] private bool debugAdjecentDetection;

    private void Awake()
    {
        Position = transform.position;
    }

    private void OnValidate()
    {
        if (openHex == null || closedHex == null) return;

        MeshRenderer mesh = GetComponent<MeshRenderer>();

        if (IsBlocked)
        {
            mesh.material = closedHex;
        }
        else
        {
            mesh.material = openHex;
        }
    }

    public int FCost()
    {
        return GCost + HCost;
    }

    public void GetNeighbours()
    {
        if (IsBlocked) return;

        Collider[] nodes = Physics.OverlapSphere(transform.position, nodeDetectionRadius, nodeLayer);

        for (int i = 0; i < nodes.Length; i++)
        {
            PathNode node = nodes[i].gameObject.GetComponent<PathNode>();
            if (node != null && !node.IsBlocked) Connections.Add(node);
        }
    }

    private void OnDrawGizmos()
    {
        if (debugConnections)
        {
            Gizmos.color = Color.green;

            if (Connections.Count > 0)
            {
                for (int i = 0; i < Connections.Count; i++)
                {
                    Gizmos.DrawLine(transform.position, Connections[i].transform.position);
                }
            }
        }
        if (debugAdjecentDetection)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, nodeDetectionRadius);
        }
        
    }
}
