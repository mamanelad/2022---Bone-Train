using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHolder : MonoBehaviour
{
    private List<Node> _nodes = new List<Node>();
    private Node _curNode;
    
    void Start()
    {
        InitNodes();
    }

    private void InitNodes()
    {   var nodesArr = GetComponentsInChildren<Node>();
        foreach (var node in nodesArr)
        {
            if (node.GetAlreadyTravelTo())
            {
                _curNode = node;
                var inNodeColor = _curNode.GetInNodeColor();
                _curNode.ChangeColorNode(inNodeColor);
            }
            
            _nodes.Add(node);
        }
    }

    public void ChangeCurrNode(Node newNode)
    {
        _curNode = newNode;
        var connectedNodes = _curNode.GetConnectedNodes();
        List<Node> notConnectedNodes = new List<Node>();
        foreach (var node  in _nodes)
        {
            if (!(connectedNodes.Contains(node)))
            {
                notConnectedNodes.Add(node);
            }
        }

        foreach (var notConnectedNode in notConnectedNodes)
        {
            notConnectedNode.MackCantCanVisit();
        }
    }
    
}
