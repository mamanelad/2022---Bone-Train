using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [SerializeField] private EventData data;

    [SerializeField] private Image background;

    [SerializeField] private Image foreground;

    [SerializeField] private Image character;

    [SerializeField] private Image buttonRight;
    
    [SerializeField] private Image buttonLeft;

    [SerializeField] private TextMeshProUGUI textBody;

    [SerializeField] private TextMeshProUGUI textTitle;

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
