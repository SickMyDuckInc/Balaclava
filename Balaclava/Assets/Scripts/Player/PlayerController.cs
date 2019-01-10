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

    public virtual void EnableMovement() {
        GameObject[] guardians = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Enable guards vision");
        for (int i = 0; i < guardians.Length; ++i)
        {
            guardians[i].GetComponent<EnemyController>().enableSearch();
        }
    }
    public virtual void DisableMovement()
    {
        GameObject[] guardians = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Disable guards vision");

        for (int i = 0; i < guardians.Length; ++i)
        {
            guardians[i].GetComponent<EnemyController>().disableSearch();
        }
    }
    public abstract void EnableRotation();
    public abstract void DisableRotation(GameObject handSelected);
}
