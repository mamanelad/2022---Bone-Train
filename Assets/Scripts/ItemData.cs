using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Item")]
public class ItemData : ScriptableObject
{
    public string name;
    public Sprite icon;
    [TextArea]
    public string description;
}
