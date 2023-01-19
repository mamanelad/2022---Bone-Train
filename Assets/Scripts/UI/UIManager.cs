using System;
using System.Collections;
using System.Collections.Generic;
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