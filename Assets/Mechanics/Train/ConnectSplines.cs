using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectSplines : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectMaster;
    [SerializeField] private GameObject gameObjectNewBranch;

    private SplineWalker train;
    private BezierSpline splineMaster;
    private BezierSpline splineNewBranch;
    private Vector3 offset;
    
    private void Start()
    {
        train = FindObjectOfType<SplineWalker>();
        splineMaster = gameObjectMaster.GetComponent<BezierSpline>();
        splineNewBranch = gameObjectNewBranch.GetComponent<BezierSpline>();
        offset = gameObjectNewBranch.transform.position - gameObjectMaster.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            ConnectTracks();
    }

    private void ConnectTracks()
    {
        float prevSplineLen = splineMaster.GetLength();
        float newSplineLen = 0;
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
            splineMaster.AddPoint(point);
        }
        Destroy(gameObjectNewBranch);
        newSplineLen = splineMaster.GetLength();
        train.Progress = (train.Progress * prevSplineLen) / newSplineLen;
        train.SplineLenght = newSplineLen;
    }
    
}
