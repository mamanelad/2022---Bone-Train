using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackChange : MonoBehaviour
{
    private SplineWalker train;
    private BezierSpline track;
    private bool didSwitch;
    
    void Start()
    {
        track = GetComponent<BezierSpline>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
            train = other.GetComponent<SplineWalker>();
    }

    private void Update()
    {
        if (!train)
            return;

        if (!didSwitch && train.Progress >= 0.7f)
        {
            print(train.Progress);
            didSwitch = true;
            train.spline = track;
            var offset = train.Progress - 0.7f;
            train.Progress = 0.38f + offset;
        }
    }
}
