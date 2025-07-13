using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    #region Variables



    //Components

    public ShipID ShipID { get; protected set; }
    protected ShipMovement movement;

    //StateMachine
    protected StateMachine stateMachine;

    //States


    #endregion


    #region Unity Methods
    protected virtual void Awake()
    {
        movement = GetComponent<ShipMovement>();
    }

    protected virtual void Start()
    {
        SetShipID();
        SetUpStateMachine();
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {

    }
    #endregion

    protected virtual void SetUpStateMachine()
    {
        stateMachine = new StateMachine();
    }

    protected virtual void SetShipID()
    {

    }
}

public enum ShipTypes { Fighter, Corvette, Destroyer, Cruiser }
