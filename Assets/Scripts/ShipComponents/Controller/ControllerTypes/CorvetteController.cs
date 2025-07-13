using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorvetteController : ShipController
{
    #region Variables

    //States

    #endregion

    #region Unity Methods

    protected override void Update()
    {
        
    }

    protected override void FixedUpdate()
    {
        
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    #endregion

    protected override void SetShipID()
    {
        ShipID = new ShipID(ShipTypes.Corvette, movement, transform);
    }

    protected override void SetUpStateMachine()
    {
        base.SetUpStateMachine();
    }
}
