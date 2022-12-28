using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private Junction curJunction;
    private Junction nextJunction;

    private float _realJunctionDistance;
    private float _trainToNextJunctionDistance;
    private float _trainToCurrJunctionDistance;

    private float _MiniMapJunctionDistance;
    private MiniMapJunction _curMiniMapJunction;
    private MiniMapJunction _nextMiniMapJunction;
    
    private bool _initFinish = false;
    private GameObject _train;
    private GameObject _miniTrain;

    private float percentageCurrRoad;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!_initFinish)
        {
            InitJunctionsAndTrain();
            return;
        }
        
        CalculateTrainToNextJunctionDistance();
        CalculateTrainToCurrJunctionDistance();
        GetPercentOfCurrRoad();
        MoveMiniTrain();
        
    }

    private void MoveMiniTrain()
    {
        Vector3 newMiniTrainPosition = Vector3.Lerp(_curMiniMapJunction.GetPosition(), _nextMiniMapJunction.GetPosition(), percentageCurrRoad);
        _miniTrain.transform.position = newMiniTrainPosition;
    }
    private void GetPercentOfCurrRoad()
    {
        percentageCurrRoad = _trainToCurrJunctionDistance / _realJunctionDistance;
    }
    private void CalculateTrainToNextJunctionDistance()
    {
        _trainToCurrJunctionDistance =
            Vector3.Distance(_train.transform.position, curJunction.gameObject.transform.position);
    }
    
    private void CalculateTrainToCurrJunctionDistance()
    {
        _trainToNextJunctionDistance =
            Vector3.Distance(_train.transform.position, nextJunction.gameObject.transform.position);
    }

    private void InitJunctionsAndTrain()
    {
        if (_train == null)
        {
            _train = GameManager.Shared.GetTrain();
            return;
        }

        if (curJunction == null)
        {
            curJunction = GameManager.Shared.GetCurrJunction();
            if (nextJunction != null)
            {
                _initFinish = true;
                CalculateRealJunctionDistance();
            }
        }


        if (nextJunction == null)
        {
            nextJunction = GameManager.Shared.GetNextJunction();
            if (curJunction != null)
            {
                _initFinish = true;
                CalculateRealJunctionDistance();
            }
        }
    }

    private void CalculateRealJunctionDistance()
    {
        if (curJunction && nextJunction)
        {
            _realJunctionDistance = Vector3.Distance(curJunction.gameObject.transform.position,
                nextJunction.gameObject.transform.position);
        }
    }
    
    private void CalculateMiniMapJunctionDistance()
    {
        if (_curMiniMapJunction && _nextMiniMapJunction)
        {
            _MiniMapJunctionDistance = Vector3.Distance(_curMiniMapJunction.GetPosition(),
                _nextMiniMapJunction.GetPosition());
        }
    }

    public void SetNewJunction(Junction curr, Junction next)
    {
        curJunction = curr;
        nextJunction = next;

        _curMiniMapJunction = curJunction.GetMiniMapJunction();
        _nextMiniMapJunction = nextJunction.GetMiniMapJunction();
        
        CalculateRealJunctionDistance();
        CalculateMiniMapJunctionDistance();
    }
}