using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [Header("Radar Detection")]
    [SerializeField] private float shortRangeRadar;
    [SerializeField] private float longRangeRadar;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Target Preferences")]
    [SerializeField, Range(0.5f, 3f)] private float fighterMultiplier;
    [SerializeField, Range(0.5f, 3f)] private float corvetteMultiplier;
    [SerializeField, Range(0.5f, 3f)] private float destroyerMultiplier;
    [SerializeField, Range(0.5f, 3f)] private float cruiserMultiplier;

    [Header("Debug")]
    [SerializeField] private bool debugGizmos;

    public List<ShipID> detectedShips { get; private set; }

    //Other
    private Collider[] radarBlips;

    private void Start()
    {
        detectedShips = new List<ShipID>();
    }

    public void RadarBurst()
    {
        detectedShips.Clear();

        radarBlips = Physics.OverlapSphere(transform.position, shortRangeRadar, enemyLayer);

        if (radarBlips.Length == 0 ) radarBlips = Physics.OverlapSphere(transform.position, longRangeRadar, enemyLayer);

        if (radarBlips.Length == 0) return;

        for (int i = 0; i < radarBlips.Length; i++)
        {
            Vector3 direction = radarBlips[i].transform.position - transform.position;

            if (Physics.Raycast(transform.position, direction.normalized, direction.magnitude, obstacleLayer))
            {
                radarBlips[i] = null;
            }
            else
            {
                if (radarBlips[i].TryGetComponent<ShipController>(out ShipController controller))
                {
                    detectedShips.Add(controller.ShipID);
                }
            }
        }
    }

    public ShipID GetFavoriteTarget()
    {
        if (detectedShips.Count == 0) return null;

        ShipID chosenTarget = null;

        float lowestScore = 9000000;

        foreach (ShipID ship in detectedShips)
        {
            float score = Vector3.Distance(transform.position, ship.Transform.position);

            switch (ship.Type)
            {
                case ShipTypes.Fighter:
                    score *= fighterMultiplier;
                    break;
                case ShipTypes.Corvette:
                    score *= corvetteMultiplier;
                    break;
                case ShipTypes.Destroyer:
                    score *= destroyerMultiplier;
                    break;
                case ShipTypes.Cruiser:
                    score *= cruiserMultiplier;
                    break;
            }

            if (score < lowestScore)
            {
                chosenTarget = ship;
                lowestScore = score;
            }
        }

        return chosenTarget;
    }

    private void OnDrawGizmos()
    {
        if (debugGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, shortRangeRadar);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, longRangeRadar);
        }
    }
}
