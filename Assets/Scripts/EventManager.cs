using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [SerializeField] private EventData data;

    [Space(20)]
    [Header("Artwork")]
    
    [SerializeField] private Image background;

    [SerializeField] private Image foreground;

    [SerializeField] private Image character;

    [SerializeField] private Image buttonRight;
    
    [SerializeField] private Image buttonLeft;
    
    [Space(20)]
    [Header("Text")]
    
    [SerializeField] private TextMeshProUGUI textTitle;
    
    [SerializeField] private TextMeshProUGUI textBody;

    void Start()
    {
        if (background != null)
            background.sprite = data.background;

        if (foreground != null)
            foreground.sprite = data.foreground;
        
        if (character != null)
            character.sprite = data.character;
        
        if (buttonRight != null)
            buttonRight.sprite = data.buttonRight;
        
        if (buttonLeft != null)
            buttonLeft.sprite = data.buttonLeft;

        if (textBody != null)
            textBody.text = GetTextBody();


        if (textTitle != null)
            textTitle.text = data.textTitle;
    }

    private string GetTextBody()
    {
        var morale = GameManager.Shared.morale;
        var bestDistance = Math.Abs(morale - data.interactions[0].morale);
        var curTextBody = data.interactions[0].textBody;
        
        foreach (var interaction in data.interactions)
        {
            var curDistance = Math.Abs(morale - interaction.morale);
            if (bestDistance > curDistance)
            {
                bestDistance = curDistance;
                curTextBody = interaction.textBody;
            }
        }
        return curTextBody;
    }
    
}
