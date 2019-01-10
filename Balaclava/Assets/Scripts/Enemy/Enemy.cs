using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    //state
    protected enum states {Patrol, Arrest};

    [HideInInspector]
    public bool playerEnabledToSearch;

    //Constants animations
    protected int lookAround = Animator.StringToHash("LookAround");
    protected int leftTurn = Animator.StringToHash("LeftTurn");
    protected int rightTurn = Animator.StringToHash("RightTurn");
    protected int totalTurn = Animator.StringToHash("TotalTurn");
    protected int walk = Animator.StringToHash("Walk");

    protected Transform target;
    protected int waypointIndex = 0;

    protected Point waypoints;

    public NavMeshAgent agent;
    [Tooltip("Assign an index for every enemy from 0 to max enemies - 1 to assign routes in waypointmanager")]
    [Header("Enemy index to assign route")]
    public int enemyIndex = 0;

    protected Animator anim;
}
