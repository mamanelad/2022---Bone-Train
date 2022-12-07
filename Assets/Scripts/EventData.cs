using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Interaction")]
public class EventData : ScriptableObject
{
    [Header("Artwork")]
    public Sprite background;
    public Sprite foreground;
    public Sprite character;

    [Header("Button Artwork")]
    public Sprite buttonLeft;
    public Sprite buttonRight;
    public Sprite buttonSingle;

    [Space(20)] [Header("Text")] 
    public string textTitle;
    public List<Interaction> interactions;
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
}
