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
            textBody.text = data.textBody;
        
        if (textTitle != null)
            textTitle.text = data.textTitle;
    }
    
}
