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
    public Sprite backGroundSprite;
    [Space(20)]
    
    [Header("Text")]
    public string titleText;
    public string descriptionText;
    public string popUpNodeText;
    [Space(20)]
    
    [Header("Colors")] 
    public Color InNodeColor = Color.yellow;
    public Color cantTravelColor = Color.black;
    public Color canTravelColor = Color.white;
    public Color travelAlreadyColor = Color.red;
    [Space(20)]
    
    [Header("Travel")]
    public float dangerPercentage;
    public List<Node> connectedNodes;
    public bool canTravelTo = false;
    public bool playerTravelAlready = false;
    [Space(20)]
    
    [Header("Requirement")]
    public float amountOfFuelNeeded;
    
    [Header("PopUp")]
    public float popUpNodeYChange = 50f;
    public float popUpNodeXChange = 50f;

}