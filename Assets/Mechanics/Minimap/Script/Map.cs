using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map GameMap;
    private List<Node> _nodeList = new List<Node>();
    private NodeHolder _nodeHolder;
    
    public bool changeScale;
    public float curScale;
    [SerializeField] private float smallScale = 5f;
    [SerializeField] private float bigScale = 15f;
    [SerializeField] private GameObject bigMapPosition;
    [SerializeField] private GameObject smallMapPosition;
    private RectTransform curPosition;
    public bool canClickMap;
    private void Awake()
    {
        GameMap = this;
    }

    void Start()
    {
        InitiateNodes();
        curPosition = transform.GetComponent<RectTransform>();
    }
    
    
    private void InitiateNodes()
    {
        _nodeHolder = GetComponentInChildren<NodeHolder>();
        foreach (var node in _nodeHolder.GetComponentsInChildren<Node>())
            _nodeList.Add(node);
    }

    private void Update()
    {
        if (changeScale)
            ChangeScale();
    }

    private void ChangeScale()
    {
        changeScale = false;
        var nowBigMap = (curScale >= bigScale);
        
        if (nowBigMap)
        {
            ChangePosition(smallMapPosition);
            ChangeCanClick(false);
            transform.localScale = new Vector3(smallScale, smallScale, smallScale);
            curScale = smallScale;
        }
        else
        {
            ChangePosition(bigMapPosition);
            ChangeCanClick(true);
            transform.localScale = new Vector3(bigScale, bigScale, bigScale);
            curScale = bigScale;
        }
        
        
    }

    private void ChangeCanClick(bool canClick)
    {
        canClickMap = canClick;
    }

    private void ChangePosition(GameObject newPos)
    {
        
        curPosition = newPos.GetComponent<RectTransform>();
        transform.position = curPosition.position;
    }

    public bool CanClickOnMap()
    {
        return canClickMap;
    }
}