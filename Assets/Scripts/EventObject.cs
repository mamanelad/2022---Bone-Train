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
    [Space(20)] public string textBody;

    // [Header("Audio")] 
    // public AudioClip eventAudio;
    // public AudioClip environmentAudio;

    public InteractionAction action;
}

[System.Serializable]
public class InteractionAction
{
    public int soulStones;
    public int goodSouls;
    public int badSouls;
}