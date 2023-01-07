using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackChange : MonoBehaviour
{
    private Collider collider;
    
    [SerializeField] private GameObject gameObjectNewBranch;
    private SplineWalker train;
    private BezierSpline splineMaster;
    private BezierSpline splineNewBranch;
    private Vector3 offset;
    
    void Start()
    {
        collider = GetComponent<Collider>();
        train = FindObjectOfType<SplineWalker>();
        splineMaster = train.spline;
        if (gameObjectNewBranch)
        {
            splineNewBranch = gameObjectNewBranch.GetComponent<BezierSpline>();
            offset = -transform.position;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            ConnectTracks();
            collider.enabled = false;
        }
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
        Destroy(splineNewBranch);
        newSplineLen = splineMaster.GetLength();
        //print("old len: " + prevSplineLen + "  new len: " + newSplineLen);
        //print("old progress: " + train.Progress + "  new progress: " + (train.Progress * (prevSplineLen / newSplineLen)));
        //train.Progress *= (prevSplineLen / newSplineLen);
        train.SplineLenght = newSplineLen;
        train.SetProgress();
    }
}
