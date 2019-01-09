using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    protected string DEFAULT_OPERATING_SYSTEM_ANDROID = "android";
    protected string DEFAULT_OPERATING_SYSTEM_APPLE = "ios";

    protected Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    public abstract void EnableMovement();
    public abstract void DisableMovement();
    public abstract void EnableRotation();
    public abstract void DisableRotation(GameObject handSelected);
}
