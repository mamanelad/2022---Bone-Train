using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class EventAudioManager : MonoBehaviour
{
    [SerializeField] private EventReference enter;
    [SerializeField] private EventReference text;
    [SerializeField] private EventReference accept;
    [SerializeField] private EventReference reject;
    
    public void PlayEnter()
    {
        RuntimeManager.PlayOneShot(enter);
    }
    
    public void PlayText()
    {
        RuntimeManager.PlayOneShot(text);
    }
    
    public void PlayAccept()
    {
        RuntimeManager.PlayOneShot(accept);
    }
    
    public void PlayReject()
    {
        RuntimeManager.PlayOneShot(reject);
    }
    
}
