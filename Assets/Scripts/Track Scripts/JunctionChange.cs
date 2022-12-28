using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JunctionChange : MonoBehaviour
{

    [SerializeField]private BezierSpline leftTrack;
    [SerializeField]private BezierSpline rightTrack;
    [SerializeField] private float speed = 4f;
    private SplineWalker train;
    [SerializeField] private Collider colliderSphere;
    [SerializeField] private Collider colliderBox;

    private void Start()
    {
        colliderSphere.enabled = true;
        colliderBox.enabled = false;
    }

    private void SwitchColliders()
    {
        colliderSphere.enabled = true;
        colliderBox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            if (colliderSphere.enabled)
            {
                print("aaaa");
                GameManager.Shared.ArrowsTurnOnAndOff(true);
                SwitchColliders();
            }

            else if (colliderBox.enabled)
            {
                var track = Random.Range(1, 3) == 1 ? leftTrack : rightTrack;
                train = other.GetComponent<SplineWalker>();
                var trackPosition = transform.transform.position - track.GetControlPoint(0); 
                train.StartCoroutine(train.SwitchTrack(track, trackPosition, 0.0f, speed));
                colliderBox.enabled = false;
            }
        }
            
    }

}
