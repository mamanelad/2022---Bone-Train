using UnityEngine;

public class EventGate : MonoBehaviour
{
    [SerializeField] private EventObject eventData;
    [SerializeField] private EventManager eventManager;

    [SerializeField] private Collider sphereTrigger;
    [SerializeField] private Collider eventTrigger;

    private EventAudioManager audioManager;

    private void Start()
    {
        audioManager = GetComponent<EventAudioManager>();
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
                audioManager.PlayEnter();
                
            }

            else if (eventTrigger.enabled)
            {
                audioManager.PlayText();
                eventManager.StartEvent(eventData, audioManager);
            }
        }
    }
}