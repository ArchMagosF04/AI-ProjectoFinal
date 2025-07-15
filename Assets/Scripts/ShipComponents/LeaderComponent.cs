using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderComponent : MonoBehaviour
{
    private ShipController shipController;

    public Action OnDeath;

    private void Awake()
    {
        shipController = GetComponent<ShipController>();
    }

    private void Start()
    {
        shipController.InspireBuff();
    }

    private void OnDisable()
    {
        OnDeath?.Invoke();
    }
}
