using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindSeek : MonoBehaviour
{
    private AStarComponent aStar;
    [SerializeField] private float arrivalDistance = 55f;

    [SerializeField] private float nodeDetectionRadius = 55f;
    [SerializeField] private LayerMask nodeLayer;
    [SerializeField] private LayerMask obstacleLayer;

    public Transform TargetLocation { get; private set; }
    public bool ReachedFinalDestination { get; private set; }

    [SerializeField] private List<PathNode> currentPath = new List<PathNode>();
    public int currentPathIndex;

    private void Awake()
    {
        aStar = GetComponent<AStarComponent>();
    }


    public void GetRouteToPoint(Transform target)
    {
        currentPathIndex = 0;
        currentPath = aStar.FindPath(FindClosestNode(transform), FindClosestNode(target));
        TargetLocation = currentPath[0].transform;
        ReachedFinalDestination = false;
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

            //Vector3 dir = nodes[i].transform.position - point.position;
            //if (Physics.Raycast(point.position, dir.normalized, dir.magnitude, obstacleLayer))
            //    nodes[i] = null;
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

    public bool ProgressChase()
    {

        //Vector3 myPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        //Vector3 targetPosition = new Vector3(currentPath[currentPathIndex].transform.position.x, 0f, currentPath[currentPathIndex].transform.position.z);

        if (!ReachedFinalDestination && Vector3.Distance(transform.position.NoY(), currentPath[currentPathIndex].transform.position.NoY()) <= arrivalDistance) //Compares the distance between the character and the destination waypoint.
        {
            GoToNextInCurrentPath(); //If it arrived at the destination then change the target.
            return true;
        }

        return false;
    }

    private void GoToNextInCurrentPath()
    {
        if (currentPathIndex == currentPath.Count - 1)
        {
            TargetLocation = null;
            ReachedFinalDestination = true;
            return;
        }

        if (currentPathIndex < currentPath.Count - 1) currentPathIndex++;

        TargetLocation = currentPath[currentPathIndex].transform;
    }
}
