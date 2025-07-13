using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Variables

    public Transform target;

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
        if (target != null && freeFireMode && Time.time > timeOfLastShot + timeBetweenShots)
        {
            FireWeapon();
            timeOfLastShot = Time.time;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (freeFireMode && target == null)
        {
            radar.RadarBurst();

            targetId = radar.GetFavoriteTarget();
            
            if (targetId != null) target = targetId.Transform;
        }

        if (target != null && freeFireMode)
        {
            if (!sensor.CanDetectTarget(target)) target = null;
        }
    }

    #endregion

    public virtual void FireWeapon()
    {
        
    }
}
