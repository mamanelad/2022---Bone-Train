using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGate : MonoBehaviour
{
    [SerializeField] private EventData myEventData;

    // private void OnTriggerEnter(Collider other)
    // {
    //     print("kaka");
    //     if (other.gameObject.CompareTag("Train"))
    //         GameManager.Shared.GotToEvent(myEventData);
    // }
}