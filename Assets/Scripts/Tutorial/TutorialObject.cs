using System;
using System.Collections;
using System.Collections.Generic;
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
        
        SpecialItem //Y

    }
    
    public TutorialKind myKind;

}
