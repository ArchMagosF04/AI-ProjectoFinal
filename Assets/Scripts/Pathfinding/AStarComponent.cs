using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathfindPatrol), typeof(PathfindSeek))]
public class AStarComponent : MonoBehaviour
{
    private PathNode[] copyNodes;

    private List<PathNode> unExploreNodes;
    private List<PathNode> exploredNodes;

    public List<PathNode> FindPath(PathNode startNode, PathNode endNode)
    {
        unExploreNodes = new List<PathNode>();
        exploredNodes = new List<PathNode>();
        copyNodes = AStarManager.Instance.WorldNodes.ToArray();

        unExploreNodes.Add(startNode);

        foreach (PathNode node in copyNodes)
        {
            node.GCost = int.MaxValue;
            node.cameFrom = null;
        }

        startNode.GCost = 0;
        startNode.HCost = CalculateHCost(startNode, endNode);

        PathNode currentNode = startNode;

        while (unExploreNodes.Count > 0)
        {
            currentNode = GetLowestFCostNode(unExploreNodes);

            if (currentNode == endNode) return CalculatePath(endNode);

            unExploreNodes.Remove(currentNode);
            exploredNodes.Add(currentNode);

            foreach (PathNode neighbourNode in currentNode.Connections)
            {
                if (exploredNodes.Contains(neighbourNode) || neighbourNode.IsBlocked) continue;

                int tentativeGCost = currentNode.GCost + CalculateHCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.GCost)
                {
                    neighbourNode.cameFrom = currentNode;
                    neighbourNode.GCost = tentativeGCost;
                    neighbourNode.HCost = CalculateHCost(neighbourNode, endNode);

                    if (!unExploreNodes.Contains(neighbourNode))
                    {
                        unExploreNodes.Add(neighbourNode);
                    }
                }
            }
        }

        return null;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);

        PathNode currentNode = endNode;
        while (currentNode.cameFrom != null)
        {
            path.Add(currentNode.cameFrom);
            currentNode = currentNode.cameFrom;
        }

        path.Reverse();
        return path;
    }

    private int CalculateHCost(PathNode node, PathNode endNode)
    {
        float cost = Vector3.Distance(node.transform.position.NoY(), endNode.transform.position.NoY());
        return Mathf.RoundToInt(cost);
    }

    private PathNode GetLowestFCostNode(List<PathNode> nodeList)
    {
        PathNode lowestFCostNode = nodeList[0];
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i].FCost() < lowestFCostNode.FCost())
            {
                lowestFCostNode = nodeList[i];
            }
        }

        return lowestFCostNode;
    }
}
