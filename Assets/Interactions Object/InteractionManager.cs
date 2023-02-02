using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private InteractionData interactionData;
    private List<LoadOption.Option> interactionOptions;

    [SerializeField] private Image character;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI textBox;

    [SerializeField] private LoadIcon icon;

    [SerializeField] private List<GameObject> optionsGameObjects;
    [SerializeField] private Animator itemAnimator;
    private bool isItemPoleUp = true;

    private EventInstance interactionAudio;

    private bool goodSoulsAdded;
    private bool badSoulsAdded;
    private static readonly int Up = Animator.StringToHash("Up");
    private static readonly int Down = Animator.StringToHash("Down");

    private const int SWORD = 0;
    private const int SHIELD = 1;

    public void StartInteraction(InteractionData newInteractionData)
    {
        gameObject.SetActive(true);
        interactionData = newInteractionData;
        interactionOptions = newInteractionData.options;
        LoadInteraction();

        if (!interactionData.audio.IsNull)
            interactionAudio.start();

        itemAnimator.SetTrigger(Up);
        UIAudioManager.Instance.PlayUIEventStart();
        UIAudioManager.Instance.PauseTrainLoop();
        GameManager.Shared.StopTrain();
        Time.timeScale = 0;
    }

    public void EndInteraction()
    {
        StartCoroutine(EndInteractionRoutine());
    }

    public IEnumerator EndInteractionRoutine()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        if (isItemPoleUp)
            itemAnimator.SetTrigger(Down);

        interactionAudio.stop(STOP_MODE.ALLOWFADEOUT);
        DisableOptions();
        UIAudioManager.Instance.ResumeTrainLoop();
        GameManager.Shared.ContinueTrain();
        gameObject.SetActive(false);
        Time.timeScale = 1;

        if (goodSoulsAdded)
            GameManager.Shared.OpenGoodSoulTuturial();
        if (badSoulsAdded)
            GameManager.Shared.OpenGoodSoulTuturial();
    }

    public void LoadInteraction()
    {
        character.sprite = interactionData.character;
        title.text = interactionData.title;
        textBox.text = interactionData.textBox;

        if (!interactionData.audio.IsNull)
            interactionAudio = RuntimeManager.CreateInstance(interactionData.audio);

        icon.Load(interactionData.iconIndex);

        LoadOptions();
    }

    private void LoadOptions()
    {
        int optionCounter = 0;
        foreach (var option in interactionOptions)
        {
            var currentOption = optionsGameObjects[optionCounter];
            currentOption.SetActive(true);
            currentOption.GetComponent<LoadOption>().Load(option);
            optionCounter += 1;
        }
    }

    public void ChooseOption(LoadOption.Option option)
    {
        GameManager.Shared.ChangeByGoodSouls(option.goodSouls);
        GameManager.Shared.ChangeByBadSouls(option.badSouls);
        GameManager.Shared.ChangeBySoulStones(option.soulsStones);

        goodSoulsAdded = option.goodSouls > 0;
        badSoulsAdded = option.badSouls > 0;

        if (option.sword)
            GameManager.Shared.ChangeBySwords(1);
        if (option.shield)
            GameManager.Shared.ChangeByShields(1);
        if (!option.sound.IsNull)
            RuntimeManager.PlayOneShot(option.sound);

        UseOptionTag(option.tag);

        EndInteraction();
    }

    public void ActivateSpecialItem(int index)
    {
        if (index == SWORD && GameManager.Shared.Swords > 0)
            ActivateSword();

        if (index == SHIELD && GameManager.Shared.Shields > 0)
            ActivateShield();
    }

    private void ActivateSword()
    {
        itemAnimator.SetTrigger(Down);
        isItemPoleUp = false;
        GameManager.Shared.ChangeBySwords(-1);
        UIAudioManager.Instance.PlayClickSwordSound();
        RemoveNegativeOptions();
        LoadOptions();
    }

    private void ActivateShield()
    {
        itemAnimator.SetTrigger(Down);
        isItemPoleUp = false;
        GameManager.Shared.ChangeByShields(-1);
        UIAudioManager.Instance.PlayClickShieldSound();
        EndInteraction();
    }

    private void RemoveNegativeOptions()
    {
        List<LoadOption.Option> modifiedOptions = new List<LoadOption.Option>();
        foreach (var option in interactionOptions)
        {
            LoadOption.Option modifiedOption = option;
            modifiedOption.goodSouls = Math.Max(modifiedOption.goodSouls, -1);
            modifiedOption.badSouls = Math.Min(modifiedOption.badSouls, 1);
            modifiedOption.soulsStones = Math.Max(modifiedOption.soulsStones, -1);
            modifiedOptions.Add(modifiedOption);
        }

        interactionOptions = modifiedOptions;
    }

    private void DisableOptions()
    {
        foreach (var optionGameObject in optionsGameObjects)
            optionGameObject.SetActive(false);
    }

    public void SetTutorialObject(Tutorial tutorial)
    {
        foreach (var option in optionsGameObjects)
        {
            option.GetComponent<LoadOption>().SetTutorialObject(tutorial);
        }
    }

    private void UseOptionTag(string tag)
    {
        if (tag == "")
            return;

        SpecialInteractionHandler handler = FindObjectOfType<SpecialInteractionHandler>();
        if (tag == "GOOD DOG")
        {
            print("Changed to good dog end");
            handler.SetDogGood();
        }

        if (tag == "GOOD MOM")
        {
            print("Changed to good mom end");
            handler.SetMomGood();
        }

        if (tag == "GOOD DEMON")
        {
            print("Changed to good demon end");
            handler.SetDemonGood();
        }
    }
}