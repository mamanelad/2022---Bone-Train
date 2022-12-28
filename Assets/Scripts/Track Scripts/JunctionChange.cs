using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JunctionChange : MonoBehaviour
{
    [SerializeField] private BezierSpline leftTrack;
    [SerializeField] private BezierSpline rightTrack;
    [SerializeField] private float speed = 4f;
    private SplineWalker train;
    [SerializeField] private Collider colliderSphere;
    [SerializeField] private Collider colliderBox;


    [SerializeField] private BezierSpline start;

    // [SerializeField] private BezierSpline leftTrack;
    [SerializeField] private BezierSpline right;
    [SerializeField] private MiniMapJunction miniMapJunction;
    [SerializeField] private JunctionChange nextJunction;
    [SerializeField] private int id;
    private SplineWalker _train;
    private BezierSpline nextTrack;


    private void Start()
    {
        colliderSphere.enabled = true;
        colliderBox.enabled = false;
    }

    private void Update()
    {
        if (!_train)
        {
            train = FindObjectOfType<SplineWalker>();
        }

        if (GameManager.Shared.GetIsArrowsAreOn())
        {
            DecideTrack(GameManager.Shared.GetArrowSide());
        }

        // DecideTrack();
    }

    private void SwitchColliders()
    {
        colliderSphere.enabled = false;
        colliderBox.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            if (colliderSphere.enabled)
            {
                GameManager.Shared.ArrowsTurnOnAndOff(true);
                SwitchColliders();
            }

            else if (colliderBox.enabled)
            {
                if (GameManager.Shared.GetArrowSide() == Arrow.ArrowSide.None)
                {
                    DecideTrack(Arrow.ArrowSide.None);
                }

                GameManager.Shared.ArrowsTurnOnAndOff(false);
                GameManager.Shared.SetArrowSide(Arrow.ArrowSide.None);
                train = other.GetComponent<SplineWalker>();
                var trackPosition = transform.transform.position - nextTrack.GetControlPoint(0);
                train.StartCoroutine(train.SwitchTrack(nextTrack, trackPosition, 0.0f, speed));
                colliderBox.enabled = false;
            }
        }
    }

    private void DecideTrack(Arrow.ArrowSide sideChosen)
    {
        if (sideChosen == Arrow.ArrowSide.Left)
        {
            nextTrack = leftTrack;
            GameManager.Shared.ArrowSpriteHandler(Arrow.ArrowSide.Left);
        }

        if (sideChosen == Arrow.ArrowSide.Right)
        {
            nextTrack = rightTrack;
            GameManager.Shared.ArrowSpriteHandler(Arrow.ArrowSide.Right);
        }

        if (sideChosen == Arrow.ArrowSide.None)
        {
            nextTrack = Random.Range(1, 3) == 1 ? leftTrack : rightTrack;
            Arrow.ArrowSide side = nextTrack == leftTrack ? Arrow.ArrowSide.Left : Arrow.ArrowSide.Right;
        }
    }
    
    public JunctionChange GetNextJunction()
    {
        if (nextJunction == null)
        {
            print("No next Junction to junction id: "+id);
        }
        return nextJunction;
    }
    
    public MiniMapJunction GetMiniMapJunction()
    {
        if (miniMapJunction == null)
        {
            print("Add the miniMap jUNCTION to his junction that is id is: " + id);
        }
        
        return miniMapJunction;
    }
}