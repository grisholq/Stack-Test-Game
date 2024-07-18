using System;
using UnityEngine;

[Serializable]
public struct JoystickInput
{
    public Vector2 Direction;
    public Vector3 Direction3D;
    public bool Pressed => Direction != Vector2.zero;
}