using System;
using System.Collections;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    #region Inspector Control

    public BezierSpline spline;

    [SerializeField] private float baseSpeed = 600;

    [SerializeField] private float accelaration;

    [SerializeField] private bool lookForward;

    [SerializeField] private SplineWalkerMode mode;

    #endregion

    private float splineLenght;

    private bool trackTransition;

    private float trainSpeed = 0; // pre 0.5 second

    private const float timeMeasureUnit = 0.1f;

    private float curSpeed;

    public float Progress { get; set; }

    private bool goingForward = true;

    private void Start()
    {
        curSpeed = baseSpeed;
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
            Progress += Time.deltaTime / splineLenght * curSpeed;
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
            Progress -= Time.deltaTime / splineLenght * curSpeed;
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
            curSpeed += Time.deltaTime * accelaration;
        if (Input.GetKey(KeyCode.DownArrow))
            curSpeed -= Time.deltaTime * accelaration;
    }

    public IEnumerator SwitchTrack(BezierSpline newTrack, Vector3 trackRealPos, float progress, float speedFactor)
    {
        var originalForward = transform.localPosition + spline.GetDirection(Progress);
        trackTransition = true;
        spline = null;
        var originalPosition = transform.position;

        var newPosition = new Vector3(trackRealPos.x, originalPosition.y, trackRealPos.z);
        var transitionDuration = Vector3.Distance(newPosition, originalPosition) / trainSpeed;
        var newForward = newPosition + newTrack.GetDirection(progress);

        var timer = 0f;
        while (timer < transitionDuration)
        {
            var t = Mathf.Min(timer / transitionDuration, 1);
            var curPos = Vector3.Lerp(originalPosition, newPosition, t);
            var curForward = Vector3.Lerp(originalForward, newForward, t);
            transform.position = curPos;
            transform.LookAt(curForward);
            yield return null;
            timer += Time.deltaTime;
        }

        transform.position = newPosition;
        spline = newTrack;
        Progress = progress;
        curSpeed = baseSpeed * speedFactor;
        trackTransition = false;
        transform.LookAt(transform.localPosition + spline.GetDirection(Progress));
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator TrackSpeed()
    {
        var currentPos = transform.position;

        while (true)
        {
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