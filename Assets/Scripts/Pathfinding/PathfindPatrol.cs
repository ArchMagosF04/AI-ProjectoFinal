using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindPatrol : MonoBehaviour
{
    private AStarComponent aStar;
    [SerializeField] private float arrivalDistance = 55f;
    [SerializeField] private float nodeDetectionRadius = 55f;
    [SerializeField] private LayerMask nodeLayer;

    [SerializeField] private List<PathNode> nodeCircuit;

    public Transform TargetLocation { get; private set; }

    [SerializeField] private List<PathNode> currentPath = new List<PathNode>();
    private int currentNode;
    private int nextNode;
    private int currentPathIndex;

    private void Awake()
    {
        aStar = GetComponent<AStarComponent>();
    }

    private void Start()
    {
        currentNode = 0;
        nextNode = 1;
        currentPath = aStar.FindPath(nodeCircuit[currentNode], nodeCircuit[nextNode]);
        TargetLocation = currentPath[0].transform;
    }

    public void StartPatrolFromLocation(Transform start)
    {
        currentPathIndex = 0;
        currentNode = nextNode - 1;
        if (nextNode == 0) currentNode = nodeCircuit.Count - 1;

        currentPath = aStar.FindPath(FindClosestNode(start), nodeCircuit[nextNode]);
        TargetLocation = currentPath[0].transform;
    }

    public bool ProgressPatrol()
    {
        //Vector3 myPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        //Vector3 targetPosition = new Vector3(currentPath[currentPathIndex].transform.position.x, 0f, currentPath[currentPathIndex].transform.position.z);

        if (Vector3.Distance(transform.position.NoY(), currentPath[currentPathIndex].transform.position.NoY()) <= arrivalDistance) //Compares the distance between the character and the destination waypoint.
        {
            if (Vector3.Distance(transform.position.NoY(), nodeCircuit[nextNode].transform.position.NoY()) <= arrivalDistance)
            {
                GoToNextPath();
                return true;
            }
            else
            {
                GoToNextInCurrentPath(); //If it arrived at the destination then change the target.
                return true;
            }
        }

        return false;
    }

    private void GoToNextInCurrentPath()
    {
        if (currentPathIndex < currentPath.Count - 1) currentPathIndex++;

        TargetLocation = currentPath[currentPathIndex].transform;
    }

    private void GoToNextPath()
    {
        currentNode++;
        if (currentNode >= nodeCircuit.Count) currentNode = 0;

        nextNode++;
        if (nextNode >= nodeCircuit.Count) nextNode = 0;

        currentPathIndex = 0;

        currentPath = aStar.FindPath(nodeCircuit[currentNode], nodeCircuit[nextNode]);
        TargetLocation = currentPath[currentPathIndex].transform;
    }

    private PathNode FindClosestNode(Transform point)
    {
        Collider[] nodes = Physics.OverlapSphere(point.position, nodeDetectionRadius, nodeLayer);
        List<PathNode> potentialNodes = new List<PathNode>();


        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].TryGetComponent<PathNode>(out PathNode pathNode))
            {
                if (!pathNode.IsBlocked)
                {
                    potentialNodes.Add(pathNode);
                }
            }
        }

        PathNode closest = null;
        float smallestDistance = 1000f;

        foreach (PathNode node in potentialNodes)
        {
            float nodeDist = Vector3.Distance(point.position, node.transform.position);
            if (closest == null || nodeDist < smallestDistance)
            {
                smallestDistance = nodeDist;
                closest = node;
            }
        }

        return closest;
    }
}
