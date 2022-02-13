using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // [SerializeField]
    // private List<GameObject> pathwayPoints = new List<GameObject>();

    private Pathfinding pf;
    private void Start()
    {
        pf = FindObjectOfType<Pathfinding>();
        var path = pf.GetPath();
        StartCoroutine(FindWayPoint(path));
    }

    IEnumerator FindWayPoint(List<WayPoint> pathwayPoints)
    {
        foreach (WayPoint wayPoint in pathwayPoints)
        {
            transform.position = wayPoint.transform.position + new Vector3(0, 1, 0);
            yield return new WaitForSeconds(0.5f);
        }
    }


    //IEnumerator FindWayPoint()
    //{
    //    foreach (GameObject wayPoint in pathwayPoints)
    //    {
    //        transform.position = wayPoint.transform.position + new Vector3(0, 1, 0);
    //        yield return new WaitForSeconds(0.5f);

    //    }
    //}


}
