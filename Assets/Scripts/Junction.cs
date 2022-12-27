using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Junction : MonoBehaviour
{
    [SerializeField] private BezierSpline start;
    [SerializeField] private BezierSpline left;
    [SerializeField] private BezierSpline right;

    private SplineWalker _train;
    private BezierSpline nextTrack;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            _train = other.GetComponent<SplineWalker>();
            GameManager.Shared.ArrowsTurnOnAndOff(true);
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Train"))
            GameManager.Shared.ArrowsTurnOnAndOff(false);
    }


    private void Update()
    {
        if (!_train)
            return;

        DecideTrack();
        
        if (start.GetDistFromEnd(_train.Progress) == 0)
        {
            _train.spline = nextTrack;
            _train.Progress = 0;
        }
    }

    private void DecideTrack()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            nextTrack = left;
            GameManager.Shared.ArrowSpriteHandler(Arrow.ArrowSide.Left);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nextTrack = right;
            GameManager.Shared.ArrowSpriteHandler(Arrow.ArrowSide.Right);
        }
            

        if (!nextTrack)
        {
            nextTrack = Random.Range(1, 2) == 1 ? left : right;
            Arrow.ArrowSide side = nextTrack == left ? Arrow.ArrowSide.Left : Arrow.ArrowSide.Right;
        }
            
    }
}