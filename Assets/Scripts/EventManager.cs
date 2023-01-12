using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public EventObject data;

    [SerializeField] private Image background;
    [SerializeField] private Image foreground;
    [SerializeField] private Image character;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI body;

    [SerializeField] private GameObject buttonAccept;
    [SerializeField] private GameObject buttonReject;
    [SerializeField] private GameObject buttonSingle;

    [SerializeField] private GameObject rSoulStone;
    [SerializeField] private GameObject rGoodSouls;
    [SerializeField] private GameObject rBadSouls;
    
    [SerializeField] private GameObject fSoulStone;
    [SerializeField] private GameObject fGoodSouls;
    [SerializeField] private GameObject fBadSouls;

    private Sprite buttonAcceptSprite;
    private Sprite buttonRejectSprite;
    private Sprite buttonSingleSprite;

    private void Start()
    {
        buttonAcceptSprite = buttonAccept.GetComponent<Image>().sprite;
        buttonRejectSprite = buttonReject.GetComponent<Image>().sprite;
        buttonSingleSprite = buttonSingle.GetComponent<Image>().sprite;
    }

    private void ResetButtonsSprite()
    {
        buttonAccept.GetComponent<Image>().sprite = buttonAcceptSprite;
        buttonReject.GetComponent<Image>().sprite = buttonRejectSprite;
        buttonSingle.GetComponent<Image>().sprite = buttonSingleSprite;
    }

    private void ConfigureEvent(EventObject newData)
    {
        data = newData;

        background.sprite = data.background;
        foreground.sprite = data.foreground;
        character.sprite = data.character;
        
        if (data.singleButton)
        {
            buttonSingle.SetActive(true);
            buttonAccept.SetActive(false);
            buttonReject.SetActive(false);
        }
        else
        {
            buttonSingle.SetActive(false);
            buttonAccept.SetActive(true);
            buttonReject.SetActive(true);
        }

        title.text = data.textTitle;
        body.text = data.textBody;
        
        ConfigureContent();
    }

    private void ConfigureContent()
    {
        rSoulStone.SetActive(false);
        rGoodSouls.SetActive(false);
        rBadSouls.SetActive(false);
        fSoulStone.SetActive(false);
        fGoodSouls.SetActive(false);
        fBadSouls.SetActive(false);

        if (data.action.soulStones > 0)
        {
            rSoulStone.SetActive(true);
            rSoulStone.GetComponentInChildren<TextMeshProUGUI>().text = data.action.soulStones.ToString();
        }
        
        if (data.action.soulStones < 0)
        {
            fSoulStone.SetActive(true);
            fSoulStone.GetComponentInChildren<TextMeshProUGUI>().text = data.action.soulStones.ToString();
        }
        
        if (data.action.goodSouls > 0)
        {
            rGoodSouls.SetActive(true);
            rGoodSouls.GetComponentInChildren<TextMeshProUGUI>().text = data.action.goodSouls.ToString();
        }
        
        if (data.action.goodSouls < 0)
        {
            fGoodSouls.SetActive(true);
            fGoodSouls.GetComponentInChildren<TextMeshProUGUI>().text = data.action.goodSouls.ToString();
        }
        
        if (data.action.badSouls > 0)
        {
            rBadSouls.SetActive(true);
            rBadSouls.GetComponentInChildren<TextMeshProUGUI>().text = data.action.badSouls.ToString();
        }
        
        if (data.action.badSouls < 0)
        {
            rBadSouls.SetActive(true);
            rBadSouls.GetComponentInChildren<TextMeshProUGUI>().text = data.action.badSouls.ToString();
        }
    }

    public void Accept()
    {
        GameManager.Shared.ChangeBySoulStones(data.action.soulStones);
        GameManager.Shared.ChangeByGoodSouls(data.action.goodSouls);
        GameManager.Shared.ChangeByBadSouls(data.action.badSouls);
        UIAudioManager.Instance.PlayUIClickEvent();
        StartCoroutine(EndEvent());
    }

    public void Reject()
    {
        UIAudioManager.Instance.PlayUIClickEvent();
        StartCoroutine(EndEvent());
    }

    public void StartEvent(EventObject newData)
    {
        gameObject.SetActive(true);
        UIAudioManager.Instance.PlayUIEventStart();
        GameManager.Shared.StopTrain();
        ConfigureEvent(newData);
        Time.timeScale = 0;
    }
    
    public IEnumerator EndEvent()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        ResetButtonsSprite();
        gameObject.SetActive(false);
        GameManager.Shared.ContinueTrain();
        Time.timeScale = 1;
    }

    public void OnButtonHover()
    {
        UIAudioManager.Instance.PlayUIHoverEvent();
    }
}

