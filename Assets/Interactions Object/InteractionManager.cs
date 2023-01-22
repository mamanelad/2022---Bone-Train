using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private InteractionData interactionData;
    
    [SerializeField] private Image character;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI textBox;

    [SerializeField] private LoadIcon icon;
    [SerializeField] private List<GameObject> options;

    public void StartInteraction(InteractionData newInteractionData)
    {
        gameObject.SetActive(true);
        interactionData = newInteractionData;
        LoadInteraction();
        
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
            RuntimeManager.PlayOneShot(interactionData.audio);
        
        icon.Load(interactionData.iconIndex);

        // loading the options
        int optionCounter = 0;
        foreach (var option in interactionData.options)
        {
            var currentOption = options[optionCounter];
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
        // add option.sword
        // add option.shield
        if (!option.sound.IsNull)
            RuntimeManager.PlayOneShot(option.sound);
        
        EndInteraction();
    }

    public void SetTutorialObject(Tutorial tutorial)
    {
        foreach (var option in options)
        {
            option.GetComponent<LoadOption>().SetTutorialObject(tutorial);
        }
    }
}
