using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Enemy
{
    states myState = states.Patrol;

    void Start()
    {
        anim = GetComponent<Animator>();
        //waypoints = new List<Point>(WaypointsManager.wp.waypoints.Count);
        waypoints = WaypointsManager.wp.GetSceneWaypoints(enemyIndex);
        waypointIndex = 0;
        target = waypoints.list[waypointIndex];
        NavMeshHit hit;
        NavMesh.SamplePosition(target.position, out hit, 5.0f, NavMesh.AllAreas);
        agent.SetDestination(hit.position);
        anim.SetFloat(walk, 1f);
    }


    void FixedUpdate()
    {
        //Debug.DrawRay(this.transform.position, this.transform.forward*10, Color.red);

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    if (waypoints.stopPoints[waypointIndex])
                    {
                        if (myState.Equals(states.Patrol))
                        {
                            anim.SetFloat(walk, 0f);
                            anim.SetBool(lookAround, true);
                            GetNextWaypoint();
                        }
                        else
                        {
                            //stay in idle next to the player and ends the game
                            anim.SetFloat(walk, 0f);
                        }
                    }
                    else
                    {
                        GetNextWayPointWithoutStop();
                    }
                }
            }
        }
    }

    #region StateMachineFunctions
    public void endLookAround()
    {
        //Debug.Log("Cop: " + this.name + " , endLookAround");
        anim.SetBool(lookAround, false);
        Turn();
    }

    public void Turn()
    {        
        //Debug.DrawLine(this.transform.position, target.transform.position, Color.blue, 10.0f);

        //direction of next waypoint
        Vector3 targetDir = (target.transform.position - this.transform.position);

        //where we have to turn right, left, ahead or behind
        float direction = AngleDir(transform.forward, targetDir, transform.up);

        //angle between our vector forward and direction of next waypoint
        float degrees = Vector3.Angle(this.transform.forward, targetDir);
        //Debug.Log("El ángulo en GRADOS es: " + degrees + " y dirección es = " + direction + "  ---------------------------------------------------------------------------------------");

        if(degrees > 135 || degrees < 25)
        {
            if(degrees <= 60)
            {
                endTurn();
            }
            else
            {
                anim.SetTrigger(totalTurn);
            }
        }
        //right
        else if(direction == 1)
        {
            anim.SetTrigger(rightTurn);
        }
        //left
        else
        {
            anim.SetTrigger(leftTurn);
        }
        
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, target.transform.rotation, 10);
    }

    public void endTurn()
    {
        anim.SetFloat(walk, 1f);
        agent.isStopped = false;
    }

    /// <summary>
    /// indicate where is the next position from our position if is in the right, left, front or backward 
    /// </summary>
    /// <param name="fwd"></param>
    /// <param name="targetDir"></param>
    /// <param name="up"></param>
    /// <returns></returns>
    private float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            //rigth
            return 1f;
        }
        else if (dir < 0f)
        {
            //left
            return -1f;
        }
        else
        {
            //ahead or behind
            return 0f;
        }
    }

    public void executeTotalTurn()
    {
        anim.SetTrigger(totalTurn);
    }
    #endregion

    #region AGENT POINTS
    void GetNextWaypoint()
    {
        agent.isStopped = true;
        waypointIndex++;

        if(waypointIndex >= waypoints.list.Count)
        {
            waypointIndex = 0;
        }

        target = waypoints.list[waypointIndex];
        agent.SetDestination(target.position);       
    }

    void GetNextWayPointWithoutStop()
    {
        waypointIndex++;

        if (waypointIndex >= waypoints.list.Count)
        {
            waypointIndex = 0;
        }

        target = waypoints.list[waypointIndex];
        NavMeshHit hit;
        NavMesh.SamplePosition(target.position, out hit, 5.0f, NavMesh.AllAreas);
        agent.SetDestination(hit.position);
    }
    #endregion

}
