using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject itemArray;
    [SerializeField] private GameObject itemPrefab;

    private Dictionary<string, GameObject> itemDict = new Dictionary<string, GameObject>();

    [Header("Text")] [SerializeField] public TextMeshProUGUI goodSoulsNumberText;
    [SerializeField] public TextMeshProUGUI badSoulsNumberText;
    [SerializeField] public TextMeshProUGUI soulStonesAmountNumberText;
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


    private void Start()
    {
        _breakChainSlider = GetComponentInChildren<BreakChain>();
        SetGoodSouls();
        SetBadSouls();
        SetSoulStones();
    }

    public void SetGoodSouls()
    {
        goodSoulsNumberText.text = Convert.ToString(GameManager.Shared.GoodSouls);
    }


    public void SetBadSouls()
    {
        badSoulsNumberText.text = Convert.ToString(GameManager.Shared.BadSouls);
    }

    public void SetSoulStones()
    {
        soulStonesAmountNumberText.text = Convert.ToString(GameManager.Shared.SoulStones);
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