using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 NoY(this Vector3 input)
    {
        return new Vector3(input.x, 0f, input.z);
    }
}
