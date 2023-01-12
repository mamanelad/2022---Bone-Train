using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] public TextMeshProUGUI goodSoulsNumberText;
    [SerializeField] public TextMeshProUGUI badSoulsNumberText;
    [SerializeField] public TextMeshProUGUI soulStonesAmountNumberText;

    

    public void SetGoodSouls()
    {
        goodSoulsNumberText.text = Convert.ToString(GameManager.Shared.GoodSouls); 
        print(goodSoulsNumberText.text);
    }
    
    
    
    public void SetBadSouls()
    {
        badSoulsNumberText.text = Convert.ToString(GameManager.Shared.BadSouls); 
    }
    
    public void SetSoulStones()
    {
        soulStonesAmountNumberText.text = Convert.ToString(GameManager.Shared.SoulStones); 
    }
}
