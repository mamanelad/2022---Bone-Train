using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGate : MonoBehaviour
{
    [SerializeField] private EventObject eventData;
    [SerializeField] private EventManager eventManager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            eventManager.StartEvent(eventData);
        }
    }
}