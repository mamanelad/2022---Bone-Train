using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Junction : MonoBehaviour
{
    [SerializeField] private BezierSpline start;
    [SerializeField] private BezierSpline left;
    [SerializeField] private BezierSpline right;
    [SerializeField] private MiniMapJunction miniMapJunction;
    [SerializeField] private Junction nextJunction;
    [SerializeField] private int id;
    private SplineWalker _train;
    private BezierSpline nextTrack;
    

    
  

    
}
