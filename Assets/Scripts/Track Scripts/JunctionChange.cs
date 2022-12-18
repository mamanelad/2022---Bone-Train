using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionChange : MonoBehaviour
{

    [SerializeField]private BezierSpline leftTrack;
    [SerializeField]private BezierSpline rightTrack;
    private SplineWalker train;
    private bool didSwitch;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
            train = other.GetComponent<SplineWalker>();
    }
    
    private void Update()
    {
        if (!train)
            return;

        if (!didSwitch)
        {
            didSwitch = true;
            train.spline = rightTrack;
            train.Progress = 0;
        }
    }

}
