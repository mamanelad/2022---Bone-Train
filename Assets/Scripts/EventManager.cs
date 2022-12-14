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

    [SerializeField] private GameObject buttonAccept;
    
    [SerializeField] private GameObject buttonDeny;
    
    [SerializeField] private GameObject buttonSingle;
    
    [Space(20)]
    [Header("Text")]
    
    [SerializeField] private TextMeshProUGUI textTitle;
    
    [SerializeField] private TextMeshProUGUI textBody;

    [Space(20)] 
    [Header("Others")] 
    [SerializeField] private float clickDelay = 0.1f;

    #endregion

    #region Class Variables

    private Interaction curInteraction;
    private bool isEventOver;

    #endregion

    private void Start()
    {
        CloseButtons();
        ConfigureEvent();
    }
    
    public void ConfigureEvent()
    {
        curInteraction = GetInteraction();
        
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
            buttonAccept.SetActive(true);
            buttonDeny.SetActive(true);
            if (buttonAccept != null)
                buttonAccept.GetComponent<Image>().sprite = data.buttonAccept;
        
            if (buttonDeny != null)
                buttonDeny.GetComponent<Image>().sprite = data.buttonDeny;
        }
        
        if (textBody != null)
            textBody.text = curInteraction.textBody;

        if (textTitle != null)
            textTitle.text = data.textTitle;
    }
    
    private Interaction GetInteraction()
    {
        var morale = GameManager.shared.morale;
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
        GameManager.shared.AddToSoulStones(action.soulStones);
        GameManager.shared.AddToGoodSouls(action.goodSouls);
        GameManager.shared.AddToBadSouls(action.badSouls);
        StartCoroutine(FinishEvent());
    }

    private IEnumerator FinishEvent()
    {
        isEventOver = true;
        yield return new WaitForSecondsRealtime(clickDelay);
        CloseButtons();
        isEventOver = false;
        LevelManager.Shared.FinishEvent();
    }

    private void CloseButtons()
    {
        buttonSingle.SetActive(false);
        buttonAccept.SetActive(false);
        buttonDeny.SetActive(false);
    }
    
}
