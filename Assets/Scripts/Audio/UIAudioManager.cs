using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance;

    [SerializeField] private EventReference uiClick;
    [SerializeField] private EventReference uiHover;
    [SerializeField] private EventReference uiEventStart;
    [SerializeField] private EventReference trainHorn;
    [SerializeField] private EventReference trainHalts;
    [SerializeField] private StudioEventEmitter trainLoop;


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


    private void Awake()
    {
        Instance = this;
    }
}