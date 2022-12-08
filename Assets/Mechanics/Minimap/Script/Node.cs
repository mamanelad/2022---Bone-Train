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
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private List<Node> _connectedNodes;

    [SerializeField] private GameObject popUpNode;
    private TMP_Text popUpNodeText;
    private bool popUpNodeIsOn;
    private string _name;
    public bool canTravelTo;
    private bool playerTravelAlready;

    Color cantTravelColor;
    Color canTravelColor;
    Color travelAlreadyColor;
    Color InNodeColor;

    private Sprite backGroundSprite;
    private float dangerPercentage;
    private float amountOfFuelNeeded;

    private Image _image;
    private Vector3 mousePosition;
    private Vector3 mousePositionOld;

    private float popUpNodeYChange;
    private float popUpNodeXChange;
    private Map _map;
    private bool canClickMap;
    private NodeHolder _nodeHolder;

    private Canvas _canvas;

    private void Awake()
    {
        _image = GetComponent<Image>();
        popUpNodeText = popUpNode.GetComponentInChildren<TMP_Text>();

        InitiateNode();
        _image.color = !canTravelTo ? cantTravelColor : canTravelColor;
    }

    private void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        _map = GetComponentInParent<Map>();
        _nodeHolder = GetComponentInParent<NodeHolder>();
    }

    private void Update()
    {
        canClickMap = _map.CanClickOnMap();
        if (!canClickMap) return;
        PopUpController();
    }

    private void PopUpController()
    {
        if (!canTravelTo) return;
        var newXChange = popUpNodeXChange * _canvas.scaleFactor;
        var newYChange = popUpNodeYChange * _canvas.scaleFactor;

        if (popUpNodeIsOn)
        {
            var popUpRect = popUpNode.GetComponent<RectTransform>();
            var nodeRect = GetComponent<RectTransform>();
            var newPos = new Vector3(nodeRect.position.x + newXChange, nodeRect.position.y - newYChange,
                nodeRect.position.z);
            popUpRect.position = newPos;
        }
    }

    private void InitiateNode()
    {
        id = _nodeData.id;
        _name = _nodeData.name;
        backGroundSprite = _nodeData.backGroundSprite;
        _titleText.text = _nodeData.titleText;
        _descriptionText.text = _nodeData.descriptionText;
        cantTravelColor = _nodeData.cantTravelColor;
        canTravelColor = _nodeData.canTravelColor;
        travelAlreadyColor = _nodeData.travelAlreadyColor;
        dangerPercentage = _nodeData.dangerPercentage;
        canTravelTo = _nodeData.canTravelTo;
        playerTravelAlready = _nodeData.playerTravelAlready;
        amountOfFuelNeeded = _nodeData.amountOfFuelNeeded;
        popUpNodeText.text = _nodeData.popUpNodeText;
        popUpNodeYChange = _nodeData.popUpNodeYChange;
        popUpNodeXChange = _nodeData.popUpNodeXChange;
        InNodeColor = _nodeData.InNodeColor;
    }


    public void TravelTo()
    {
        if (!canClickMap) return;
        if (!playerTravelAlready && canTravelTo)
        {
            _nodeHolder.ChangeCurrNode(this);
            playerTravelAlready = true;
            foreach (var node in _connectedNodes)
            {
                if (node._image.color == InNodeColor)
                {
                    node._image.color = travelAlreadyColor;
                }
            }

            _image.color = InNodeColor;
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

    public void MouseIsOn()
    {
        if (playerTravelAlready) return;
        if (!canTravelTo) return;
        if (!canClickMap) return;
        if (popUpNodeIsOn) return;
        popUpNode.SetActive(true);
        popUpNodeIsOn = true;
    }

    public void MouseIsOff()
    {
        if (!canClickMap) return;
        popUpNode.SetActive(false);
        popUpNodeIsOn = false;
    }

    public bool GetAlreadyTravelTo()
    {
        return playerTravelAlready;
    }

    public List<Node> GetConnectedNodes()
    {
        return _connectedNodes;
    }

    public void MackCantCanVisit()
    {
        canTravelTo = false;
        if (!playerTravelAlready)
        {
            _image.color = cantTravelColor;
        }
    }

    public Color GetInNodeColor()
    {
        return InNodeColor;
    }

    public void ChangeColorNode(Color newColor)
    {
        _image.color = newColor;
    }
}