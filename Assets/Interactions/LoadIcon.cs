using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadIcon : MonoBehaviour
{
    [Serializable]
    public struct IconData
    {
        public Sprite icon;
        public string description;
    }

    [SerializeField] private List<IconData> data;
    
    private const int GOODSOULS = 0;
    private const int BADSOULS = 1;
    private const int SOULSTONES = 2;
    private const int MAJOR_DECISION = 3;
    private const int ENEMY = 4;
    private const int REGULAR = 5;
    private const int CHANCE = 6;
    private const int ITEM = 7;

    public void Load(int iconIndex)
    {
        var image = GetComponentInChildren<Image>();
        var text = GetComponentInChildren<TextMeshProUGUI>();

        image.sprite = data[iconIndex].icon;
        text.text = data[iconIndex].description;
    }

}
