using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Event")]
public class EventData : ScriptableObject
{
    public Sprite background;
    public Sprite foreground;
    public Sprite character;

    public string textTitle;
    public string textBody;

    public Sprite buttonLeft;
    public Sprite buttonRight;
}
