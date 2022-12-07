using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    #region Inspector Control

    public EventData data;

    [Space(20)]
    [Header("Artwork")]
    
    [SerializeField] private Image background;

    [SerializeField] private Image foreground;

    [SerializeField] private Image character;

    [SerializeField] private GameObject buttonRight;
    
    [SerializeField] private GameObject buttonLeft;
    
    [SerializeField] private GameObject buttonSingle;
    
    [Space(20)]
    [Header("Text")]
    
    [SerializeField] private TextMeshProUGUI textTitle;
    
    [SerializeField] private TextMeshProUGUI textBody;

    [Space(20)] 
    [Header("Others")] 
    [SerializeField] private float clickDelay = 1f;

    #endregion

    #region Class Variables

    private Interaction curInteraction;
    private bool isEventOver;

    #endregion

    private void Start()
    {
        curInteraction = GetInteraction();
        buttonSingle.SetActive(false);
        buttonRight.SetActive(false);
        buttonLeft.SetActive(false);
        
        //ConfigureEvent();
    }
    
    public void ConfigureEvent()
    {
        if (background != null)
            background.sprite = data.background;

        if (foreground != null)
            foreground.sprite = data.foreground;
        
        if (character != null)
            character.sprite = data.character;

        if (data.buttonSingle)
        {
            buttonSingle.SetActive(true);
            buttonSingle.GetComponent<Image>().sprite = data.buttonSingle;
        }
        else
        {
            buttonRight.SetActive(true);
            buttonLeft.SetActive(true);
            if (buttonRight != null)
                buttonRight.GetComponent<Image>().sprite = data.buttonRight;
        
            if (buttonLeft != null)
                buttonLeft.GetComponent<Image>().sprite = data.buttonLeft;
        }
        
        if (textBody != null)
            textBody.text = curInteraction.textBody;

        if (textTitle != null)
            textTitle.text = data.textTitle;
    }
    
    private Interaction GetInteraction()
    {
        var morale = GameManager.Shared.morale;
        var bestDistance = Math.Abs(morale - data.interactions[0].morale);
        var curInter = data.interactions[0];
        
        foreach (var interaction in data.interactions)
        {
            var curDistance = Math.Abs(morale - interaction.morale);
            if (bestDistance > curDistance)
            {
                bestDistance = curDistance;
                curInter = interaction;
            }
        }
        return curInter;
    }

    public void InteractionAction(bool accept)
    {
        if (isEventOver)
            return;
        
        var action = accept ? curInteraction.interactionAccept : curInteraction.interactionDeny;
        GameManager.Shared.SoulStones += action.soulStones;
        GameManager.Shared.GoodSouls += action.goodSouls;
        GameManager.Shared.BadSouls += action.badSouls;
        StartCoroutine(FinishEvent());
    }

    private IEnumerator FinishEvent()
    {
        isEventOver = true;
        yield return new WaitForSecondsRealtime(clickDelay);
        LevelManager.Shared.FinishEvent();
    }

}
