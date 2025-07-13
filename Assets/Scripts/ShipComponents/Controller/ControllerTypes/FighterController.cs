using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : ShipController
{
    #region Variables

    #endregion

    #region Unity Methods

    protected override void Update()
    {
        base.Update();
        //movement.CalculateDesiredDirection();
        //movement.RotateTowardsDirection(movement.DesiredDirection);
        //movement.MoveShip();
    }

    #endregion

    protected override void SetShipID()
    {
        ShipID = new ShipID(ShipTypes.Fighter, movement, transform);
    }
}
