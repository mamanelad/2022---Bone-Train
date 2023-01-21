using FMODUnity;
using UnityEngine;

public class EventGate : MonoBehaviour
{
    [SerializeField] private InteractionData interactionData;
    [SerializeField] private InteractionManager interactionManager;
    [SerializeField] private SpriteRenderer minimapIcon;
    
    [SerializeField] private Collider sphereTrigger;
    [SerializeField] private Collider eventTrigger;
    
    private void Start()
    {
        if (interactionData == null)
        {
            Debug.Log("Event gate is missing interaction data");
            return;
        }
        
        interactionManager = FindObjectOfType<InteractionManager>();
        
        SetUpMinimapIcon();
        
        sphereTrigger.enabled = true;
        eventTrigger.enabled = false;
    }

    private void SetUpMinimapIcon()
    {
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            if (sphereTrigger.enabled)
            {
                sphereTrigger.enabled = false;
                eventTrigger.enabled = true;
                RuntimeManager.PlayOneShot(interactionData.enteredDistanceAudio);
                
            }

            else if (eventTrigger.enabled)
            {
                interactionManager.StartInteraction(interactionData);
            }
        }
    }
}