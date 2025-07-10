using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform target;

    [SerializeField] private bool freeFireMode;

    [SerializeField] private SensorDetection sensor;

    private void FixedUpdate()
    {
        if (freeFireMode && target == null)
        {
            sensor.SearchForEnemiesInRange();
        }
    }
}
