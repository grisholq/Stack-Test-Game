using UnityEngine;

public struct StickmanInput
{
    public Vector3 Movement;
    public Vector2 Movement2D => new Vector2(Movement.x, Movement.z);
    public Vector3 Direction => Movement.normalized;
    public bool NoMovement => Movement == Vector3.zero;
    public bool HasMovement => NoMovement == false;
}