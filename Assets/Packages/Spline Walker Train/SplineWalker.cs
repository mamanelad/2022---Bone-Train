using System;
using System.Collections;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    #region Inspector Control

    public BezierSpline spline;

    [SerializeField] private float speed;

    [SerializeField] private float accelaration;

    [SerializeField] private bool lookForward;

    [SerializeField] private SplineWalkerMode mode;
    
    #endregion

    private float splineLenght;

    private bool trackTransition;

    private float trainSpeed = 0; // pre 0.5 second

    private const float timeMeasureUnit = 0.5f;
    

    public float Progress { get; set; }

    private bool goingForward = true;

    private void Start()
    {
        splineLenght = spline.GetLength();
        StartCoroutine(TrackSpeed());
    }

    private void Update()
    {
         if (trackTransition)
            return;

         Drive();
    }

    private void Drive()
    {
        if (goingForward)
        {
            Progress += Time.deltaTime / splineLenght * speed;
            if (Progress > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                    Progress = 1f;
                else if (mode == SplineWalkerMode.Loop)
                    Progress -= 1f;
                else
                {
                    Progress = 2f - Progress;
                    goingForward = false;
                }
            }
        }
        else
        {
            Progress -= Time.deltaTime / splineLenght * speed;
            if (Progress < 0f)
            {
                Progress = -Progress;
                goingForward = true;
            }
        }

        Vector3 position = spline.GetPoint(Progress);
        transform.localPosition = position;
        if (lookForward)
            transform.LookAt(position + spline.GetDirection(Progress));
    }

    private void ChangeSpeed()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            speed += Time.deltaTime * accelaration;
        if (Input.GetKey(KeyCode.DownArrow))
            speed -= Time.deltaTime * accelaration;
    }

    public IEnumerator SwitchTrack(BezierSpline newTrack, Vector3 trackRealPos)
    {
        print("RTP: " + trackRealPos);
        trackTransition = true;
        spline = null;
        var currentPosition = transform.position;

        var newPosition = new Vector3(trackRealPos.x, currentPosition.y, trackRealPos.z);
        var transitionDuration = Vector3.Distance(newPosition, currentPosition) / trainSpeed;
        
        var timer = 0f;
        while (timer < transitionDuration)
        {
            var t = Mathf.Min(timer / transitionDuration, 1);
            var curPos = Vector3.Lerp(currentPosition, newPosition, t);
            transform.position = curPos;
            yield return null;
            timer += Time.deltaTime;
        }

        transform.position = newPosition;

        spline = newTrack;
        Progress = 0.50f;
        print("Train Pos: " + transform.position);
        trackTransition = false;

        yield return new WaitForSeconds(0.1f);
        print("Drive Pos: " + transform.position);
        
    }
    
    private IEnumerator TrackSpeed()
    {
        var currentPos = transform.position;

        while (true)
        {
            // print(trainSpeed);
            yield return new WaitForSeconds(timeMeasureUnit);
            if (!trackTransition)
            {
                var newPosition = transform.position;
                trainSpeed = Vector3.Distance(currentPos, newPosition) / timeMeasureUnit;
                currentPos = newPosition;
            }
        }
    }
}