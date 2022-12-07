using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Space(20)] [Header("Text")] 
    public string textTitle;
    public List<Interaction> interactions;
}

[System.Serializable]
public class Interaction
{
    public Morale morale;
    [TextArea] public string textBody;
}
