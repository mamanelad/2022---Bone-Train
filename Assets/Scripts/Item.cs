using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData data;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI description;

    [SerializeField] private GameObject textBox;

    private void Start()
    {
        icon.sprite = data.icon;
        description.text = data.description;
        textBox.SetActive(false);
    }

    public void ShowItemDescription()
    {
        textBox.SetActive(true);
    }

    public void HideItemDescription()
    {
        textBox.SetActive(false);
    }
}
