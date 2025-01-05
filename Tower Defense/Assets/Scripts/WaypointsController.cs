using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsController : MonoBehaviour
{
    [SerializeField] private List<GameObject> wayPointsEnemy;

    private void Start()
    {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        Array.Sort(waypoints, (a, b) => { return a.name.CompareTo(b.name);});
        for (int i = 0; i < waypoints.Length; i++)
        {
            wayPointsEnemy.Add(waypoints[i]);
        }
    }

    public Vector3 GetTheWayPoint(int index)
    {
        return wayPointsEnemy[index].transform.position;
    }
}