using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private Transform _train;
    private Camera _miniMapCamara;
    [SerializeField] private float addToPositionX = -5f;
    private Vector3 _originCamPosition;

    private void Awake()
    {
        _miniMapCamara = FindObjectOfType<MiniMapCamara>().GetComponent<Camera>();
        _originCamPosition = _miniMapCamara.transform.position;
    }

    private void LateUpdate()
    {
        if (!_train)
            _train = GameManager.Shared.GetTrain().transform;


        var newPos = _train.transform.position;
        newPos.y = _originCamPosition.y;
        newPos.z = _originCamPosition.z;
        newPos.x += addToPositionX;
        _miniMapCamara.transform.position = newPos;
    }
    
}