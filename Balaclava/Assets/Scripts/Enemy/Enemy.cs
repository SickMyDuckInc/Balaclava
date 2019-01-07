using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    //state
    protected enum states {Patrol, Arrest};

    //Constants animations
    protected int lookAround = Animator.StringToHash("LookAround");
    protected int leftTurn = Animator.StringToHash("LeftTurn");
    protected int rightTurn = Animator.StringToHash("RightTurn");
    protected int totalTurn = Animator.StringToHash("TotalTurn");
    protected int walk = Animator.StringToHash("Walk");

    protected Transform target;
    protected int waypointIndex = 0;

    protected List<Transform> waypoints;

    public NavMeshAgent agent;

    protected Animator anim;
}
