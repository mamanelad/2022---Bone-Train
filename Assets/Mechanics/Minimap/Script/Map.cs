using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map GameMap;
    
    public string[] Names;
    
    public List<Node> _nodeList;
    [SerializeField] private GameObject _nodeHolder;
    
    public List<Edge> _edgesList;
    [SerializeField] private GameObject _edgeHolder;
    



    

    private void Awake()
    {
        GameMap = this;
    }

    void Start()
    {
        foreach (var node in _nodeHolder.GetComponentsInChildren<Node>())
            _nodeList.Add(node);
            
        foreach (var edge in _edgeHolder.GetComponentsInChildren<Edge>())
            _edgesList.Add(edge);
    }

    
    
}
