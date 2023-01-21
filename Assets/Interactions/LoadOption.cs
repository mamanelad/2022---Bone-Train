using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadOption : MonoBehaviour
{
    [Serializable]
    public struct Option
    {
        public int goodSouls;
        public int badSouls;
        public int soulsStones;
        public bool sword;
        public bool shield;
    }

    [SerializeField] private List<Sprite> icons;
    [SerializeField] private GameObject dealsTab;
    [SerializeField] private GameObject itemSlotPrefab;

    private const int GOODSOULS = 0;
    private const int BADSOULS = 1;
    private const int SOULSTONES = 2;
    private const int SWORD = 3;
    private const int SHIELD = 4;

    private List<GameObject> currentItems;

    public void Load(Option option)
    {
        DestroyPrevData();
        
        if (option.goodSouls != 0)
            LoadSingle(GOODSOULS, option.goodSouls);
        if (option.badSouls != 0)
            LoadSingle(BADSOULS, option.badSouls);
        if (option.soulsStones != 0)
            LoadSingle(SOULSTONES, option.soulsStones);
        if (option.sword)
            LoadSingle(SWORD, 1);
        if (option.shield)
            LoadSingle(SHIELD, 1);
    }

    private void DestroyPrevData()
    {
        foreach (var item in currentItems)
            Destroy(item);

        currentItems = new List<GameObject>();
    }

    private void LoadSingle(int iconIndex, int amount)
    {
        var newDeal = Instantiate(itemSlotPrefab, dealsTab.transform);
        var image = newDeal.GetComponentInChildren<Image>();
        var text = newDeal.GetComponentInChildren<TextMeshProUGUI>();

        image.sprite = icons[iconIndex];
        text.text = amount > 0 ? $"+ {amount}" : $"- {Math.Abs(amount)}";
        
        currentItems.Add(newDeal);
    }
}
