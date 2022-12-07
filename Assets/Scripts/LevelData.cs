using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class LevelData : ScriptableObject
{
    public List<EventData> interactions;
    public float delayBetweenInteractions;
}
