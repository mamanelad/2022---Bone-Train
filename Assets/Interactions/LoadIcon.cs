using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadIcon : MonoBehaviour
{
    [Serializable]
    public enum IconIndex
    {
        GOODSOULS,
        BADSOULS,
        SOULSTONES,
        MAJOR_DECISION,
        ENEMY,
        REGULAR,
        CHANCE,
        ITEM
    }
    
    [Serializable]
    public struct IconData
    {
        public Sprite icon;
        public string description;
    }

    [SerializeField] private List<IconData> data;
    
    public void Load(IconIndex iconIndex)
    {
        var image = GetComponentInChildren<Image>();
        var text = GetComponentInChildren<TextMeshProUGUI>();

        image.sprite = data[(int)iconIndex].icon;
        text.text = data[(int)iconIndex].description;
    }

}
