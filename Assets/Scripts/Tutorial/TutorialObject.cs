using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObject : MonoBehaviour
{
    public enum TutorialKind
    {
        StartHandle,
        ArrowsAndJunction,
        MiniMap,
        FuelAndFurnace,
        GoodAndBadSouls,
        SpeedMeter
        
    }
    
    public TutorialKind myKind;
    
    
    public TutorialKind GetTutorialKind()
    {
        return myKind;
    }
}
