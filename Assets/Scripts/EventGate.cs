using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGate : MonoBehaviour
{
    [SerializeField] private EventObject eventData;
    [SerializeField] private EventManager eventManager;

    [SerializeField] private Collider sphereTrigger;
    [SerializeField] private Collider eventTrigger;

    private void Start()
    {
        sphereTrigger.enabled = true;
        eventTrigger.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            if (sphereTrigger.enabled)
            {
                sphereTrigger.enabled = false;
                eventTrigger.enabled = true;
                // if (eventData.environmentAudio)
                //     other.gameObject.GetComponentInChildren<TrainAudio>().AddClip(eventData.environmentAudio);
            }

            else if (eventTrigger.enabled)
            {
                // if (eventData.eventAudio)
                //     other.gameObject.GetComponentInChildren<TrainAudio>().AddClip(eventData.eventAudio);
                eventManager.StartEvent(eventData);
            }
        }
    }
}