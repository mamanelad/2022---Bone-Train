using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class EventGate : MonoBehaviour
{
    [SerializeField] private InteractionData interactionData;
    [SerializeField] private InteractionManager interactionManager;
    [SerializeField] private SpriteRenderer minimapIcon;

    [SerializeField] private Collider sphereTrigger;
    [SerializeField] private Collider eventTrigger;

    [SerializeField] private List<Sprite> minimapIcons;

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
        if (minimapIcon == null)
            return;
        
        minimapIcon.sprite = minimapIcons[(int) interactionData.iconIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (interactionData == null)
            return;

        if (other.CompareTag("Train"))
        {
            if (sphereTrigger.enabled)
            {
                sphereTrigger.enabled = false;
                eventTrigger.enabled = true;
                if (!interactionData.enteredDistanceAudio.IsNull)
                    RuntimeManager.PlayOneShot(interactionData.enteredDistanceAudio);
            }

            else if (eventTrigger.enabled)
            {
                interactionManager.StartInteraction(interactionData);
            }
        }
    }

    public void SetInteractionData(InteractionData newData)
    {
        interactionData = newData;
    }
}