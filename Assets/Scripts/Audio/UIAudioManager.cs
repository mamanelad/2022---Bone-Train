using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance;

    [Header("UI Sounds")] [SerializeField] private EventReference uiClick;
    [SerializeField] private EventReference uiHover;
    [SerializeField] private EventReference uiEventStart;

    [Header("Train Sounds")] [SerializeField]
    private EventReference trainHorn;

    [SerializeField] private EventReference trainHalts;
    [SerializeField] private StudioEventEmitter trainLoop;

    [Header("Coal Sounds")] [SerializeField]
    private EventReference grabCoal;

    [SerializeField] private EventReference burnCoal;

    public void PlayUIClickEvent()
    {
        RuntimeManager.PlayOneShot(uiClick);
    }

    public void PlayUIHoverEvent()
    {
        RuntimeManager.PlayOneShot(uiHover);
    }

    public void PlayUIEventStart()
    {
        RuntimeManager.PlayOneShot(uiEventStart);
    }

    public void PlayTrainHorn()
    {
        RuntimeManager.PlayOneShot(trainHorn);
    }

    public void PlayTrainHalt()
    {
        RuntimeManager.PlayOneShot(trainHalts);
    }

    public void PauseTrainLoop()
    {
        trainLoop.EventInstance.setPaused(true);
    }

    public void ResumeTrainLoop()
    {
        trainLoop.EventInstance.setPaused(false);
    }

    public void SetTrainLoopSpeed(float speed)
    {
        trainLoop.EventInstance.setPitch(speed);
    }

    public void PlayGrabCoal()
    {
        RuntimeManager.PlayOneShot(grabCoal);
    }

    public void PlayBurnCoal()
    {
        RuntimeManager.PlayOneShot(burnCoal);
    }


    private void Awake()
    {
        Instance = this;
    }
}