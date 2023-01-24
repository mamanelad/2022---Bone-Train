using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JunctionChange : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectNewBranchLeft;
    [SerializeField] private GameObject gameObjectNewBranchRight;
    private SplineWalker train;
    private BezierSpline splineMaster;
    private BezierSpline splineNewBranch;
    private BezierSpline splineNewBranchLeft;
    private BezierSpline splineNewBranchRight;
    [SerializeField] private Collider colliderSphere;
    [SerializeField] private Collider colliderBox;

    private Vector3 offset;

    // [SerializeField] private MiniMapJunction miniMapJunction;
    [SerializeField] private JunctionChange nextJunction;
    [SerializeField] private int id;

    private bool _randomDecisionWasMade;

    private void Start()
    {
        colliderSphere.enabled = true;
        colliderBox.enabled = false;
        train = FindObjectOfType<SplineWalker>();
        splineMaster = splineMaster = train.spline;
        splineNewBranchLeft = gameObjectNewBranchLeft.GetComponent<BezierSpline>();
        splineNewBranchRight = gameObjectNewBranchRight.GetComponent<BezierSpline>();
        if (gameObjectNewBranchLeft)
        {
            splineNewBranch = gameObjectNewBranchLeft.GetComponent<BezierSpline>();
            offset = -transform.position;
        }
    }

    private void Update()
    {
        if (GameManager.Shared)
        {
            if (GameManager.Shared.GetIsArrowsAreOn())
                DecideTrack(GameManager.Shared.GetArrowSide());    
        }
        
    }


    private void ConnectTracks()
    {
        float prevSplineLen = splineMaster.GetLength();
        float newSplineLen = 0;
        var endPoint = splineMaster.points[^1];
        var newPointStart = splineNewBranch.points[0] + offset;

        if (Vector3.Distance(endPoint, newPointStart) > 0.005)
        {
            var point = new Vector3[]
            {
                Vector3.Lerp(endPoint, newPointStart, 0.33f),
                Vector3.Lerp(endPoint, newPointStart, 0.66f),
                newPointStart
            };
            splineMaster.AddPoint(point);
        }

        for (int i = 1; i < splineNewBranch.points.Length; i += 3)
        {
            var point = new Vector3[]
            {
                splineNewBranch.points[i] + offset,
                splineNewBranch.points[i + 1] + offset,
                splineNewBranch.points[i + 2] + offset,
            };
            splineMaster.AddPoint(point);
        }

        Destroy(splineNewBranch);
        newSplineLen = splineMaster.GetLength();
        print("old len: " + prevSplineLen + "  new len: " + newSplineLen);
        print("old progress: " + train.Progress + "  new progress: " +
              (train.Progress * (prevSplineLen / newSplineLen)));
        //train.Progress *= prevSplineLen / newSplineLen;
        train.SplineLenght = newSplineLen;
        train.SetProgress();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            if (colliderSphere.enabled)
            {
                GameManager.Shared.ArrowsTurnOnAndOff(true);
                colliderSphere.enabled = false;
                colliderBox.enabled = true;
            }

            else if (colliderBox.enabled)
            {
                if (GameManager.Shared.GetArrowSide() == Arrow.ArrowSide.None)
                    DecideTrack(Arrow.ArrowSide.None);

                GameManager.Shared.ArrowsTurnOnAndOff(false);
                GameManager.Shared.SetArrowSide(Arrow.ArrowSide.None);
                ConnectTracks();
                colliderBox.enabled = false;
            }
        }
    }


    private void DecideTrack(Arrow.ArrowSide sideChosen)
    {
        if (sideChosen == Arrow.ArrowSide.Left)
        {
            splineNewBranch = splineNewBranchLeft;
            GameManager.Shared.ArrowSpriteHandler(Arrow.ArrowSide.Left);
        }

        if (sideChosen == Arrow.ArrowSide.Right)
        {
            splineNewBranch = splineNewBranchRight;
            GameManager.Shared.ArrowSpriteHandler(Arrow.ArrowSide.Right);
        }

        if (sideChosen == Arrow.ArrowSide.None)
        {
            if (_randomDecisionWasMade) return;
            _randomDecisionWasMade = true;
            splineNewBranch = Random.Range(1, 3) == 1 ? splineNewBranchLeft : splineNewBranchRight;
            Arrow.ArrowSide side = splineNewBranch == splineNewBranchLeft
                ? Arrow.ArrowSide.Left
                : Arrow.ArrowSide.Right;
            // GameManager.Shared.ArrowSpriteHandler(side);
        }
    }

    public JunctionChange GetNextJunction()
    {
        if (nextJunction == null)
        {
            print("No next Junction to junction id: " + id);
        }

        return nextJunction;
    }

    // public MiniMapJunction GetMiniMapJunction()
    // {
    //     if (miniMapJunction == null)
    //     {
    //         print("Add the miniMap jUNCTION to his junction that is id is: " + id);
    //     }
    //
    //     return miniMapJunction;
    // }
}