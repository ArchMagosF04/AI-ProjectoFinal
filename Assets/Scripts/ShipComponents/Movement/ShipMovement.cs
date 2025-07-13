using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObstacleAvoidance))]
public class ShipMovement : MonoBehaviour
{
    public enum SteeringMode { Seek, Pursuit, Flee, Arrival, None}

    [SerializeField] private SteeringMode currentMode = SteeringMode.Seek;
    [field: SerializeField] public Transform TargetLocation { get; private set; }

    [Header("Stats")]

    [SerializeField, Range(10, 500)] private float rotationSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField, Range(-1, 1)] private float turnThreshold;


    [Header("Pursuit")]

    [SerializeField] private float timePrediction;
    [SerializeField] private float closeEnoughDist;
    [SerializeField] private ShipMovement targetsMovement;

    [Header("Debug")]

    [SerializeField] private bool debugTargetFuturePos;
    [SerializeField] protected bool measureDistance;
    [SerializeField] private float distanceToMeasure;


    public Vector3 DesiredDirection { get; private set; }
    public Vector3 MoveVector => moveSpeed * DesiredDirection.normalized;

    //Components
    private ObstacleAvoidance obstacleAvoidance;

    //Steerings
    private Steer_Pursuit pursuit;

    private void Awake()
    {
        obstacleAvoidance = GetComponent<ObstacleAvoidance>();
    }

    private void Start()
    {
        pursuit = new Steer_Pursuit(transform, targetsMovement, timePrediction, closeEnoughDist);
    }

    public void CalculateDesiredDirection()
    {
        Vector3 dir = (TargetLocation.position.NoY() - transform.position.NoY()).normalized;

        //SteeringBehaviourModifier <--- TODO
        switch (currentMode)
        {
            case SteeringMode.Seek:
                break;
            case SteeringMode.Pursuit:
                dir = pursuit.MoveDirection();
                break;
            case SteeringMode.Flee:
                break;
            case SteeringMode.Arrival:
            default:
                break;
        }

        DesiredDirection = dir;
    }

    public void RotateTowardsDirection(Vector3 direction)
    {
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

    public void MoveShip()
    {
        Vector3 moveVector = transform.forward * moveSpeed * Time.deltaTime;
        Vector3 avoidance = obstacleAvoidance.Avoid();

        transform.position += avoidance;

        if (Vector3.Dot(transform.forward, DesiredDirection) > turnThreshold)
        {
            transform.position += moveVector;
        }
    }

    public void ChangeSteering(SteeringMode mode)
    {
        currentMode = mode;
    }

    private void OnDrawGizmos()
    {
        if (debugTargetFuturePos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(pursuit.FuturePos, 15f);
        }

        if (measureDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, distanceToMeasure);
        }
    }
}
