using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    #region Variables

    [Header("Detection")]
    [field: SerializeField] public SensorDetection ShipSensor { get; protected set; }
    [field: SerializeField] public Radar ShipRadar { get; protected set; }

    [Header("Weapons")]
    [field: SerializeField] public SensorDetection WeaponSensor { get; protected set; }
    [field: SerializeField] public Radar WeaponRadar { get; protected set; }
    [SerializeField] private List<Weapon> weapons;

    public Transform AttackTarget { get; protected set; }

    //Components
    public ShipID ShipID { get; protected set; }
    public HealthController ShipHealth { get; protected set; }
    public PathfindSeek PathfindSeek { get; protected set; }
    public PathfindPatrol PathfindPatrol { get; protected set; }
    protected ShipMovement movement;


    //StateMachine
    public StateMachine stateMachine { get; protected set; }

    //States
    public ShipST_Idle IdleState { get; protected set; }
    public ShipST_Flee FleeState { get; protected set; }
    public ShipST_FindTarget FindTargetState { get; protected set; }
    public ShipST_Patrol PatrolState { get; protected set; }
    public ShipST_Chase ChaseState { get; protected set; }
    public ShipST_Attack AttackState { get; protected set; }

    #endregion


    #region Unity Methods
    protected virtual void Awake()
    {
        movement = GetComponent<ShipMovement>();
        ShipHealth = GetComponent<HealthController>();
        PathfindSeek = GetComponent<PathfindSeek>();
        PathfindPatrol = GetComponent<PathfindPatrol>();
    }

    protected virtual void Start()
    {
        SetUpStateMachine();
        SetShipID();
    }

    protected virtual void Update()
    {
        stateMachine.CurrentState.OnUpdate();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.CurrentState.OnFixedUpdate();
    }

    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }

    #endregion

    protected virtual void SetUpStateMachine()
    {
        stateMachine = new StateMachine();
    }

    protected virtual void SetShipID() { }

    public virtual void SelectAttackTarget(Transform target) => AttackTarget = target;

    #region Ship Weapons
    public virtual void ToggleWeaponManualControl(bool toggle)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].ToggleFreeFireMode(!toggle);
        }
    }

    public virtual void AimWeaponsAtTarget()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetTarget(AttackTarget);
        }
    }

    public virtual void FireWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].FireWeapon();
        }
    }
    #endregion
}

public enum ShipTypes { Fighter, Corvette, Destroyer, Cruiser }
