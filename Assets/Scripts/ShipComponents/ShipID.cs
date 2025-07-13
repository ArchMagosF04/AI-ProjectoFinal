using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShipID
{
    public ShipTypes Type;
    public string Name;
    public ShipMovement Movement;
    public Transform Transform;

    public ShipID(ShipTypes type, string name, ShipMovement movement, Transform transform)
    {
        Type = type;
        Name = name;
        Movement = movement;
        Transform = transform;
    }

    public ShipID(ShipTypes type, ShipMovement movement, Transform transform)
    {
        Type = type;
        Name = " ";
        Movement = movement;
        Transform = transform;
    }
}
