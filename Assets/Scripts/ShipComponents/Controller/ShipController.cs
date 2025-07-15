using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    #region Variables

    [Header("Detection")]
    [SerializeField] protected SensorDetection shipSensor;
    public SensorDetection ShipSensor => shipSensor;
    [SerializeField] protected Radar shipRadar;
    public Radar ShipRadar => shipRadar;

    [Header("Weapon Systems")]
    [SerializeField] protected SensorDetection weaponSensor;
    public SensorDetection WeaponSensor => weaponSensor;
    [SerializeField] protected Radar weaponRadar;
    public Radar WeaponRadar => weaponRadar;

    [SerializeField] private List<Weapon> weapons;

    [Header("Scriptable Objects")]
    [SerializeField] private LeaderDeathRoulette leaderRoulette;

    [Header("Debug Info")]
    [SerializeField] private string currentStateName;
    [field: SerializeField] public Transform AttackTarget { get; protected set; }
    

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

        if (gameObject.tag == "RedTeam")
        {
            GameManager.Instance.RegisterAsRedTeam();
            if (GameManager.Instance.RedAdmiral != null) GameManager.Instance.RedAdmiral.OnDeath += OnLeaderDeath;
        }
        else if (gameObject.tag == "BlueTeam")
        {
            GameManager.Instance.RegisterAsBlueTeam();
            if (GameManager.Instance.BlueAdmiral != null) GameManager.Instance.BlueAdmiral.OnDeath += OnLeaderDeath;
        }
    }

    protected virtual void Update()
    {
        stateMachine.CurrentState.OnUpdate();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.CurrentState.OnFixedUpdate();
    }

    protected virtual void OnEnable() 
    {
        ShipHealth.OnLowHealth += FleeOnLowHealth;
        ShipHealth.OnDeath += RecordDeath;
    }
    protected virtual void OnDisable() 
    {
        ShipHealth.OnLowHealth -= FleeOnLowHealth;
        ShipHealth.OnDeath -= RecordDeath;
        stateMachine.OnStateChange -= GetStateName;

        if (gameObject.tag == "RedTeam")
        {
            if (GameManager.Instance.RedAdmiral != null) GameManager.Instance.RedAdmiral.OnDeath -= OnLeaderDeath;
        }
        else if (gameObject.tag == "BlueTeam")
        {
            if (GameManager.Instance.BlueAdmiral != null) GameManager.Instance.BlueAdmiral.OnDeath -= OnLeaderDeath;
        }
    }

    #endregion

    protected virtual void RecordDeath()
    {
        if (gameObject.tag == "RedTeam")
        {
            GameManager.Instance.UnregisterFromRedTeam();
        }
        else if (gameObject.tag == "BlueTeam")
        {
            GameManager.Instance.UnregisterFromBlueTeam();
        }
    }

    protected virtual void SetUpStateMachine()
    {
        stateMachine = new StateMachine();
        stateMachine.OnStateChange += GetStateName;
    }

    protected virtual void GetStateName() => currentStateName = stateMachine.CurrentState.GetType().ToString();

    protected virtual void SetShipID() { }

    public virtual void SelectAttackTarget(Transform target) => AttackTarget = target;

    public virtual void FleeOnLowHealth()
    {
        stateMachine.ChangeState(FleeState);
        ShipHealth.OnLowHealth -= FleeOnLowHealth;
    }

    public virtual void InspireBuff()
    {
        ShipHealth.OnLowHealth -= FleeOnLowHealth;
        ShipHealth.ToggleHealthRegen(true);
    }

    public virtual void OnLeaderDeath()
    {
        float inspireBuff = leaderRoulette.InspireBuff;
        float keepFighting = leaderRoulette.KeepFighting;
        float fleeInTerror = leaderRoulette.FleeInTerror;

        int currentHealth = ShipHealth.CurrentHealth;
        int maxHealth = ShipHealth.MaxHealth;

        if (currentHealth >= maxHealth * leaderRoulette.UpperHealthThreshold)
        {
            inspireBuff *= leaderRoulette.InspireHighMod;
            keepFighting *= leaderRoulette.FightHighMod;
            fleeInTerror *= leaderRoulette.InspireHighMod;
        }
        else if (currentHealth <= maxHealth * leaderRoulette.LowerHealthThreshold)
        {
            inspireBuff *= leaderRoulette.InspireLowMod;
            keepFighting *= leaderRoulette.FightLowMod;
            fleeInTerror *= leaderRoulette.FleeLowMod;
        }

        float total = inspireBuff + keepFighting + fleeInTerror;

        inspireBuff /= total;
        keepFighting /= total;
        fleeInTerror /= total;

        float[] table = { inspireBuff, keepFighting, fleeInTerror };

        float value = Random.value;
        int i = 0;

        while (i < table.Length && value > table[i])
        {
            value -= table[i];
            i++;
        }

        switch (i)
        {
            case 0: //Inspiring Buff
                InspireBuff();
                Debug.Log("We will avenge the fallen.");
                break;
            case 2: //Flee In Terror
                FleeOnLowHealth();
                Debug.Log("Game over man, game over.");
                break;
            default: //Keep Fighting
                Debug.Log("Keep shooting.");
                break;
        }
    }

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
