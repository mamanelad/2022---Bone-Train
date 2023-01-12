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

    private void Awake()
    {
        Instance = this;
    }
}
