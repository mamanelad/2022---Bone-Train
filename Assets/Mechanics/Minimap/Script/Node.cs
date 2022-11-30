using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Map;
public class Node : MonoBehaviour
{
    public int id;

    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _descriptionText;

    [SerializeField] private string _name = "Node";
    [SerializeField] private Node[] _connectedNodes;

    [SerializeField] bool canTravelTo = false;
    [SerializeField] private bool playerTravelAlready = false;
    
    [SerializeField] Color cantTravelColor = Color.black;
    [SerializeField] Color canTravelColor = Color.white;
    [SerializeField] Color travelAlreadyColor = Color.red;
    
    private Image _image;

    private void Awake()
    {
        _name += id;
        _image = GetComponent<Image>();
        if (!canTravelTo) 
            _image.color = cantTravelColor;
        else
            _image.color = canTravelColor;
    }
    

    //This function is called when you click the node
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
