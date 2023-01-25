using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject itemArray;
    [SerializeField] private GameObject itemPrefab;

    private Dictionary<string, GameObject> itemDict = new Dictionary<string, GameObject>();

    [Header("Text")] [SerializeField] public TextMeshProUGUI goodSoulsNumberText;
    [SerializeField] public TextMeshProUGUI badSoulsNumberText;
    [SerializeField] public TextMeshProUGUI soulStonesAmountNumberText;
    [SerializeField] public TextMeshProUGUI swordsAmountNumberText;
    [SerializeField] public TextMeshProUGUI shieldsAmountNumberText;
    private BreakChain _breakChainSlider;


    

    public enum UiOption
    {
        Handle,
        Fuel,
        Interaction,
        Tutorial,
        Arrows
    }

    [SerializeField] private UiOption[] handleWorkWith;
    [SerializeField] private UiOption[] fuelWorkWith;
    [SerializeField] private UiOption[] InteractionWorkWith;
    [SerializeField] private UiOption[] arrowsWorkWith;

    private UiOption _curOption;

    [Space(10)] [Header("Speed Bar")] [SerializeField]
    private Image speedBar;
    [SerializeField][Range(0,1)] private float speedBarAdd = 0.5f;

    private void Start()
    {
        _breakChainSlider = GetComponentInChildren<BreakChain>();
        // SetGoodSouls();
        // SetBadSouls();
        // SetSoulStones();
    }

    private void FixedUpdate()
    {
        SpeedBar();
    }

    private void SpeedBar()
    {
        var wantedFill = Mathf.InverseLerp(0, GameManager.Shared.GetMaxSpeed(), GameManager.Shared.GetSpeed());
        var newFill = Mathf.Lerp(speedBar.fillAmount, wantedFill, speedBarAdd);
        speedBar.fillAmount = newFill;

    }
    public void SetGoodSouls()
    {
        goodSoulsNumberText.text = Convert.ToString(GameManager.Shared.GoodSouls);
    }


    public void SetBadSouls()
    {
        print("got to change the bed souls number sould be " + GameManager.Shared.BadSouls);
        badSoulsNumberText.text = Convert.ToString(GameManager.Shared.BadSouls);
    }

    public void SetSoulStones()
    {
        soulStonesAmountNumberText.text = Convert.ToString(GameManager.Shared.SoulStones);
    }
    
    public void SetSwords()
    {
        swordsAmountNumberText.text = $"X {Convert.ToString(GameManager.Shared.Swords)}";
    }
    
    public void SetShields()
    {
        shieldsAmountNumberText.text = $"X {Convert.ToString(GameManager.Shared.Shields)}";
    }

    private void SetCurUIOption(UiOption uiOption)
    {
        _curOption = uiOption;
    }

    public bool CanUIElementWork(UiOption uiOption)
    {
        if (uiOption == _curOption) return true;
        switch (_curOption)
        {
            case UiOption.Handle:
                return handleWorkWith.Contains(uiOption);

            case UiOption.Fuel:
                return fuelWorkWith.Contains(uiOption);

            case UiOption.Interaction:
                return InteractionWorkWith.Contains(uiOption);

            case UiOption.Tutorial:
                return true;

            case UiOption.Arrows:
                return arrowsWorkWith.Contains(uiOption);
        }

        return false;
    }

    public BreakChain GetBreakChain()
    {
        return _breakChainSlider;
    }


    public void AddItem(ItemData data)
    {
        var newItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, itemArray.transform);
        newItem.GetComponent<Item>().LoadItemData(data);
        itemDict[data.name] = newItem;
    }

    public bool CheckIfPlayerGotItem(ItemData data)
    {
        return itemDict.ContainsKey(data.name);
    }

    public bool RemoveItem(ItemData data)
    {
        if (CheckIfPlayerGotItem(data))
        {
            Destroy(itemDict[data.name]);
            itemDict.Remove(data.name);
            return true;
        }

        return false;
    }
}