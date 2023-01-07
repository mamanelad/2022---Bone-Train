using System;
using System.Collections;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    #region Inspector Control

    public BezierSpline spline;

    [SerializeField] private bool lookForward;

    [SerializeField] private SplineWalkerMode mode;

    #endregion

    public float SplineLenght { get; set; }

    private bool trackTransition;

    private float trainSpeed = 0; // pre 0.5 second

    private const float timeMeasureUnit = 0.03f;

    private float curSpeed;

    private float speedFactor = 1;

    public float Progress { get; set; }

    private bool goingForward = true;

    private float baseSpeed;

    private void Start()
    {
        baseSpeed = GameManager.Shared.GetSpeed();
        curSpeed = baseSpeed * speedFactor;
        SplineLenght = spline.GetLength();
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
            Progress += Time.deltaTime / SplineLenght * curSpeed;
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
            Progress -= Time.deltaTime / SplineLenght * curSpeed;
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

    public void SetProgress()
    {
        float min = 0f;
        float max = 1f;
        float distMin;
        float distMax;
        var tempPos = spline.GetPoint(0.5f);
        var curPos = transform.position;
        var dist = Vector3.Distance(curPos, tempPos); 
        var minDist = 100f;
        float minIndex = 0;
        // float detailLevel = 20000;
        // for (float i = 0; i < detailLevel; i++)
        // {
        //     tempPos = spline.GetPoint(i / detailLevel);
        //     var dist = Vector3.Distance(curPos, tempPos); 
        //     if (dist < minDist)
        //     {
        //         minDist = dist;
        //         minIndex = i;
        //     }
        // }
        // print(minIndex / detailLevel);
        // Progress = minIndex / detailLevel;

        var i = 1;
        while (dist > 0.005f)
        {
            print(i);
            i += 1;
            var posMin = spline.GetPoint(min);
            var posMax = spline.GetPoint(max);
            
            distMin = Vector3.Distance(curPos, posMin); 
            distMax = Vector3.Distance(curPos, posMax);

            if (distMin < distMax)
            {
                dist = distMin;
                max = (max + min) / 2f;
            }
            else
            {
                dist = distMax;
                min = (max + min) / 2f;
            }
        }
        
        print("AERT:  " + (dist == minDist ? min : max));
        Progress = dist == minDist ? min : max;
    }

    private IEnumerator TrackSpeed()
    {
        //var currentPos = transform.position;
        while (true)
        {
            baseSpeed = GameManager.Shared.GetSpeed();
            curSpeed = baseSpeed * speedFactor;
            yield return new WaitForSeconds(timeMeasureUnit);
            // var newPosition = transform.position;
            // trainSpeed = Vector3.Distance(currentPos, newPosition);
            // //curSpeed *= 0.1f / trainSpeed;
            // currentPos = newPosition;
        }
    }
}