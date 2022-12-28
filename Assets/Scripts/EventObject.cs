using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "New Event")]
public class EventObject : ScriptableObject
{
    [Header("Artwork")]
    public Sprite background;
    public Sprite foreground;
    public Sprite character;
    public bool singleButton;

    [Header("Text")]
    public string textTitle;
    [Space(20)]
    public string textBody;
    
}

[System.Serializable]
public class Interaction
{
    public Morale morale = Morale.Neutral;
    [TextArea] public string textBody;
    public InteractionAction interactionAccept;
    public InteractionAction interactionDeny;
}

[System.Serializable]
public class InteractionAction
{
    public int soulStones;
    public int goodSouls;
    public int badSouls;
    public Morale moraleImpact;
}