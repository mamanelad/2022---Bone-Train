using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackChange : MonoBehaviour
{
    private SplineWalker _train;
    private BezierSpline track;
    private bool didSwitch;
    
    void Start()
    {
        track = GetComponent<BezierSpline>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
            _train = other.GetComponent<SplineWalker>();
    }

    private void Update()
    {
        if (!_train)
            return;

        if (!didSwitch && _train.Progress >= 0.7f)
        {
            print(_train.Progress);
            didSwitch = true;
            _train.spline = track;
            var offset = _train.Progress - 0.7f;
            _train.Progress = 0.38f + offset;
        }
    }
}
