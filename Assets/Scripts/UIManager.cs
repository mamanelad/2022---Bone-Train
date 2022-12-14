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
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGoodSouls()
    {
        goodSoulsNumberText.text = Convert.ToString(GameManager.shared.GoodSouls); 
    }
    
    public void ChangeBadSouls()
    {
        badSoulsNumberText.text = Convert.ToString(GameManager.shared.BadSouls); 
    }
    
    public void ChangeSoulStones()
    {
        soulStonesAmountNumberText.text = Convert.ToString(GameManager.shared.SoulStones); 
    }
}
