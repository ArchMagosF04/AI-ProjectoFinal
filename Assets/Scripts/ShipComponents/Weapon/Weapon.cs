using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Variables

    [field: SerializeField] public Transform Target { get; protected set; }

    [SerializeField] protected bool freeFireMode;

    [Header("Components")]
    [SerializeField] protected SensorDetection sensor;
    [SerializeField] protected Radar radar;

    [Header("Weapon Stats")]
    [SerializeField, Range(0.1f, 10f)] protected float timeBetweenShots;

    //Other
    protected float timeOfLastShot;
    protected ShipID targetId;

    #endregion

    #region Unity Methods

    protected virtual void Awake()
    {
        sensor = GetComponentInParent<SensorDetection>();
        radar = GetComponentInParent<Radar>();
    }

    protected virtual void Start()
    {
        timeOfLastShot = Time.time;
    }

    protected virtual void Update()
    {
        if (Target != null && freeFireMode && Time.time > timeOfLastShot + timeBetweenShots)
        {
            FireWeapon();
            timeOfLastShot = Time.time;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (freeFireMode && Target == null)
        {
            radar.RadarBurst();

            targetId = radar.GetFavoriteTarget();
            
            if (targetId != null) Target = targetId.Transform;
        }

        if (Target != null && freeFireMode)
        {
            if (!sensor.CanDetectTarget(Target)) Target = null;
        }
    }

    #endregion

    public virtual void FireWeapon()
    {
    }

    public virtual void ToggleFreeFireMode(bool input) 
    {
        freeFireMode = input;
        Target = null;
    }

    public virtual void SetTarget(Transform target) => this.Target = target;
}
