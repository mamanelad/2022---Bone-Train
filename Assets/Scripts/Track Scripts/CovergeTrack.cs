using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovergeTrack : MonoBehaviour
{
    [Range(0f,1f)][SerializeField] private float progress = 0.0f;
    [SerializeField] private BezierSpline leftTrack;
    [SerializeField] private BezierSpline rightTrack;
    [SerializeField] private float speed = 4f;
    [SerializeField]private Collider collider1;
    [SerializeField]private Collider collider2;

    private BezierSpline GetTrack(Vector3 trainPos)
    {
        var leftStartPos = leftTrack.GetControlPoint(0);
        var rightStartPos = rightTrack.GetControlPoint(0);

        if (Vector3.Distance(trainPos, leftStartPos) < Vector3.Distance(trainPos, rightStartPos))
            return leftTrack;
        return rightTrack;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        print("bbbb");
        if (other.CompareTag("Train"))
        {
            print("aaaa");
            var track = GetTrack(other.transform.position);
            var train = other.GetComponent<SplineWalker>();
            var midPos = track.GetControlPoint(0);
            var trackPosition = transform.transform.position - midPos; 
            train.StartCoroutine(train.SwitchTrack(track, trackPosition, progress, speed));
            collider1.enabled = false;
            collider2.enabled = false;
        }
    }
    
}
