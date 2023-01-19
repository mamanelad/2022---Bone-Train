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
    
    public InteractionAction action;

    [Range(0f, 1f)] public float dangerChance = 0.5f;
}

[System.Serializable]
public class InteractionAction
{
    public int soulStones;
    public int goodSouls;
    public int badSouls;
    public bool receiveItem;
    public ItemData item;
}