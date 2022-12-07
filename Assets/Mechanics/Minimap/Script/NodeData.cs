using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Map;

[CreateAssetMenu(menuName = "NodeData")]
public class NodeData : ScriptableObject
{
    [Header("General")]
    public int id;
    public string nameOfNode = "Node";
    public Image _image;
    [Space(20)]
    
    [Header("Text")]
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    [Space(20)]
    
    [Header("Colors")] 
    public Color cantTravelColor = Color.black;
    public Color canTravelColor = Color.white;
    public Color travelAlreadyColor = Color.red;
    [Space(20)]
    
    [Header("Travel")]
    public float dangerPercentage;
    public Node[] connectedNodes;
    public bool canTravelTo = false;
    public bool playerTravelAlready = false;
    [Space(20)]
    
    [Header("Requirement")]
    public float amountOfFuelNeeded;
}