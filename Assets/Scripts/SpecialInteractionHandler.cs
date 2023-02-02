using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInteractionHandler : MonoBehaviour
{
    [SerializeField] private InteractionData specialDogGood;
    [SerializeField] private InteractionData specialMomGood;
    [SerializeField] private InteractionData specialDemonGood;

    [SerializeField] private EventGate specialDogResult;
    [SerializeField] private EventGate specialMomResult;
    [SerializeField] private EventGate specialDemonResult;

    public void SetDogGood()
    {
        specialDogResult.SetInteractionData(specialDogGood);
    }

    public void SetMomGood()
    {
        specialMomResult.SetInteractionData(specialMomGood);
    }

    public void SetDemonGood()
    {
        specialDemonResult.SetInteractionData(specialDemonGood);
    }
}
