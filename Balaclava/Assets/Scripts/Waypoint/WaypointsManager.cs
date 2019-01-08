using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Point
{
    public List<Transform> list;
}

public class WaypointsManager : MonoBehaviour
{
    public static WaypointsManager wp = null;

    [Header("Scene Waypoints")]
    public List<Point> waypoints;


    // Start is called before the first frame update
    void Awake()
    {
        wp = this;
    }

    public List<Transform> GetSceneWaypoints(int enemyIndex)
    {
        return waypoints[enemyIndex].list;
    }
}
