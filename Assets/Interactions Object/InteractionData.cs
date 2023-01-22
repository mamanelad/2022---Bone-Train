using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

[CreateAssetMenu(menuName = "New Interaction")]
public class InteractionData : ScriptableObject
{
    [Header("Artwork")]
    public Sprite character;
    public EventReference audio;
    public EventReference enteredDistanceAudio;
    
    [Header("Interaction Type")]
    public LoadIcon.IconIndex iconIndex;

    [Header("Text")] 
    public string title;
    [TextArea] 
    public string textBox;

    [Header("Options")] 
    public List<LoadOption.Option> options;
}
