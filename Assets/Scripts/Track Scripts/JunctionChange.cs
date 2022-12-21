using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionChange : MonoBehaviour
{

    [SerializeField]private BezierSpline leftTrack;
    [SerializeField]private BezierSpline rightTrack;
    [SerializeField] private float speed = 4f;
    private SplineWalker train;
    private Collider collider;
    
    void Start()
    {
        collider = GetComponent<Collider>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            train = other.GetComponent<SplineWalker>();
            var trackPosition = transform.transform.position - leftTrack.GetControlPoint(0); 
            train.StartCoroutine(train.SwitchTrack(leftTrack, trackPosition, 0.0f, speed));
            collider.enabled = false;
        }
            
    }

}
