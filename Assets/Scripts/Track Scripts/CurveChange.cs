using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveChange : MonoBehaviour
{
    [Range(0f,1f)][SerializeField] private float progress = 0.0f;
    [SerializeField] private float speed = 4f;
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
            var midPos = track.GetControlPoint(0);
            var trackPosition = transform.transform.position - midPos; 
            //train.StartCoroutine(train.SwitchTrack(track, trackPosition, progress, speed));
            collider.enabled = false;
        }
    }
}