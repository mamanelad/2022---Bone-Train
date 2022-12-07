using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Map;

public class Node : MonoBehaviour
{
    [SerializeField] NodeData _nodeData;

    private int id;
    private TMP_Text _titleText;
    private TMP_Text _descriptionText;

    private string _name;
    private Node[] _connectedNodes;

    private bool canTravelTo;
    private bool playerTravelAlready;

    Color cantTravelColor;
    Color canTravelColor;
    Color travelAlreadyColor;

    private Image _image;
    private float dangerPercentage;
    private float amountOfFuelNeeded;

    private void Awake()
    {
        InitiateNode();
        if (!canTravelTo)
            _image.color = cantTravelColor;
        else
            _image.color = canTravelColor;
    }


    private void InitiateNode()
    {
        id = _nodeData.id;
        _name = _nodeData.name;
        _image = _nodeData._image;
        _titleText = _nodeData.titleText;
        _descriptionText = _nodeData.descriptionText;
        cantTravelColor = _nodeData.cantTravelColor;
        canTravelColor = _nodeData.canTravelColor;
        travelAlreadyColor = _nodeData.travelAlreadyColor;
        dangerPercentage = _nodeData.dangerPercentage;
        _connectedNodes = _nodeData.connectedNodes;
        canTravelTo = _nodeData.canTravelTo;
        playerTravelAlready = _nodeData.playerTravelAlready;
        amountOfFuelNeeded = _nodeData.amountOfFuelNeeded;
    }


    public void TravelTo()
    {
        if (!playerTravelAlready && canTravelTo)
        {
            playerTravelAlready = true;
            _image.color = travelAlreadyColor;

            foreach (var node in _connectedNodes)
            {
                if (!node.playerTravelAlready && !node.canTravelTo)
                {
                    node.canTravelTo = true;
                    node._image.color = node.canTravelColor;
                }
            }
        }
    }
}