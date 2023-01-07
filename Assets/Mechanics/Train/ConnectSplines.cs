using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectSplines : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectMaster;
    [SerializeField] private GameObject gameObjectNewBranch;

    private BezierSpline splineMaster;
    private BezierSpline splineNewBranch;

    private Vector3 offset;
    
    private void Start()
    {
        splineMaster = gameObjectMaster.GetComponent<BezierSpline>();
        splineNewBranch = gameObjectNewBranch.GetComponent<BezierSpline>();
        offset = gameObjectNewBranch.transform.position - gameObjectMaster.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var endPoint = splineMaster.points[^1];
            var newPointStart = splineNewBranch.points[0] + offset;

            if (Vector3.Distance(endPoint, newPointStart) > 0.005)
            {
                var point = new Vector3[]
                {
                    Vector3.Lerp(endPoint, newPointStart, 0.33f),
                    Vector3.Lerp(endPoint, newPointStart, 0.66f),
                    newPointStart
                };
                splineMaster.AddPoint(point);
            }
            
            for (int i = 1; i < splineNewBranch.points.Length; i+=3)
            {
                var point = new Vector3[]
                {
                    splineNewBranch.points[i] + offset,
                    splineNewBranch.points[i+1] + offset,
                    splineNewBranch.points[i+2] + offset,
                };
                foreach (var p in splineMaster.points)
                {
                    print(p);
                }
                splineMaster.AddPoint(point);
                print("-----------------------------");
                foreach (var p in splineMaster.points)
                {
                    print(p);
                }
            }
            Destroy(gameObjectNewBranch);
        }
    }
    
}
