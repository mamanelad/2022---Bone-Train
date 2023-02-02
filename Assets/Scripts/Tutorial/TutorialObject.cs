using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TutorialObject : MonoBehaviour
{
   
    
    public enum TutorialKind
    {
        StartHandle,  //N
        
        Fuel,  //Y
        
        Furnace,  //Y
        
        SpeedMeter,     //Y
        
        GoodSouls, //Y
        
        BadSouls, //Y
        
        Deals, // Y
        
        MiniMap, //N

        Arrows, //N
        
        Junction,  //N          
        
        SpecialItem, //Y
        
        None

    }

    
    public bool stopTime;

    [Range(0, 1)] public float slowMotionTime = 1f; 
    public bool closeTheObjectWithTimer;
    public bool openWithTime = true;
    
    public TutorialKind myKind;
    private bool _startClose;
    [SerializeField] private float closeTimer = 3f;
    public TutorialKind nextObject = TutorialKind.None;

    private void Update()
    {
        if (_startClose)
        {
            closeTimer -= Time.deltaTime;
            if (closeTimer <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void StartCloseRoutine()
    {
        _startClose = true;
    }
    
}
