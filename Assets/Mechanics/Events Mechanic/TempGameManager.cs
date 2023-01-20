using System;
using TMPro;
using UnityEngine;

public class TempGameManager : MonoBehaviour
{

    public static TempGameManager Shared;
    
    [SerializeField] public int soulStonesInitializeValue;
    [SerializeField] public int goodSoulsInitializeValue;
    [SerializeField] public int badSoulsInitializeValue;

    [NonSerialized] public int SoulStones;
    [NonSerialized] public int GoodSouls;
    [NonSerialized] public int BadSouls;
    
    [Header("UI Items")] 
    [SerializeField] private TextMeshProUGUI goodSouls;
    [SerializeField] private TextMeshProUGUI badSouls;
    [SerializeField] private TextMeshProUGUI fuel;

    private void Awake()
    {
        Shared = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SoulStones = soulStonesInitializeValue;
        GoodSouls = goodSoulsInitializeValue;
        BadSouls = badSoulsInitializeValue;
    }

    private void Update()
    {
        goodSouls.text = "Good Souls: " + GoodSouls;
        badSouls.text = "Bad Souls: " + BadSouls;
        fuel.text = "Fuel: " + SoulStones;
    }

    public void ChangeBySoulStones(int addNum)
    {
        if (SoulStones + addNum < 0)
        {
            SoulStones = 0;
        }
        SoulStones += addNum;
    }
    
    public void ChangeByGoodSouls(int addNum)
    {
        if (GoodSouls + addNum < 0)
        {
            GoodSouls = 0;
            return;
        }
        GoodSouls += addNum;
    }
    
    public void ChangeByBadSouls(int addNum)
    {
        if (BadSouls + addNum < 0)
        {
            BadSouls = 0;
            return;
        }
        
        BadSouls += addNum;
    }
}
