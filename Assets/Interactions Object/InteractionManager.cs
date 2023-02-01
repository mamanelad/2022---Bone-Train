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
    // [SerializeField] private LoadSpecialItem specialItem;
    [SerializeField] private List<GameObject> optionsGameObjects;

    private EventInstance interactionAudio;
    
    public void StartInteraction(InteractionData newInteractionData)
    {
        gameObject.SetActive(true);
        interactionData = newInteractionData;
        interactionOptions = newInteractionData.options;
        LoadInteraction();

        interactionAudio.start();
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
        interactionAudio.stop(STOP_MODE.ALLOWFADEOUT);
        DisableOptions();
        UIAudioManager.Instance.ResumeTrainLoop();
        GameManager.Shared.ContinueTrain();
        gameObject.SetActive(false);
        Time.timeScale = 1;
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
        // LoadSpecial();
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

    // private void LoadSpecial()
    // {
    //     if (interactionData.iconIndex == LoadIcon.IconIndex.ENEMY)
    //         specialItem.Load(LoadSpecialItem.SpecialItemIndex.SHIELD);
    //
    //     if (interactionData.iconIndex == LoadIcon.IconIndex.REGULAR)
    //         specialItem.Load(LoadSpecialItem.SpecialItemIndex.SWORD);
    //
    //     if (interactionData.iconIndex == LoadIcon.IconIndex.CHANCE)
    //         specialItem.Load(LoadSpecialItem.SpecialItemIndex.SWORD);
    //
    //     if (interactionData.iconIndex == LoadIcon.IconIndex.ITEM)
    //         specialItem.Load(LoadSpecialItem.SpecialItemIndex.SWORD);
    // }

    public void ChooseOption(LoadOption.Option option)
    {
        GameManager.Shared.ChangeByGoodSouls(option.goodSouls);
        GameManager.Shared.ChangeByBadSouls(option.badSouls);
        GameManager.Shared.ChangeBySoulStones(option.soulsStones);
        if (option.sword)
            GameManager.Shared.ChangeBySwords(1);
        if (option.shield)
            GameManager.Shared.ChangeByShields(1);
        if (!option.sound.IsNull)
            RuntimeManager.PlayOneShot(option.sound);

        EndInteraction();
    }

    // public void ActivateSpecialItem(LoadSpecialItem.SpecialItemIndex index)
    // {
    //     if (index == LoadSpecialItem.SpecialItemIndex.SWORD)
    //         ActivateSword();
    //
    //     if (index == LoadSpecialItem.SpecialItemIndex.SHIELD)
    //         ActivateShield();
    // }

    private void ActivateSword()
    {
        GameManager.Shared.ChangeBySwords(-1);
        RemoveNegativeOptions();
        LoadOptions();
    }

    private void ActivateShield()
    {
        GameManager.Shared.ChangeByShields(-1);
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
}