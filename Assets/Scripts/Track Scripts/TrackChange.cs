using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackChange : MonoBehaviour
{
    private SplineWalker train;
    private BezierSpline track;
    private Collider collider;
    
    void Start()
    {
        track = GetComponent<BezierSpline>();
        collider = GetComponent<Collider>();

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            train = other.GetComponent<SplineWalker>();
            var midPos = (track.GetControlPoint(2) + track.GetControlPoint(1)) / 2;
            var trackPosition = transform.transform.position - midPos; 
            train.StartCoroutine(train.SwitchTrack(track, trackPosition));
            collider.enabled = false;
        }
            
    }
}
