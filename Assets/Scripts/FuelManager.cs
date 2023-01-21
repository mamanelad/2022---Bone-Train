using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FuelManager : MonoBehaviour
{
    [Header("Time")] [SerializeField] private float tackDownFuelTime = 1f;
    [SerializeField] private float tackDownFuelTimer;

    [Space(10)] [Header("Speed")] private float _speed;
    private float _maxSpeed = 0;
    [SerializeField] private float speedMult = 1.3f;

    [Space(10)] [Header("Extra")] [SerializeField] private bool _driving;
    [FormerlySerializedAs("tackDownFuelAmount")] [SerializeField] private int tackDownFuelAmountInMaxSpeed = 1;


    private void Awake()
    {
        tackDownFuelTimer = tackDownFuelTime;
    }

    private void Update()
    {
        if (_maxSpeed == 0)
            _maxSpeed = GameManager.Shared.GetMaxSpeed();

        _speed = GameManager.Shared.GetSpeed();

        if (!_driving) return;
        
        
        // tackDownFuelTimer -= Time.deltaTime * speedDiff * speedMult);
        tackDownFuelTimer -= Time.deltaTime;
        
        // if (tackDownFuelTimer <= 0)
        // {
        //     var speedTakeDown = Mathf.FloorToInt((_speed / _maxSpeed) * tackDownFuelAmountInMaxSpeed);
        //     tackDownFuelTimer = tackDownFuelTime;
        //     GameManager.Shared.ChangeBySoulStones(-speedTakeDown);
        // }
    }

    public void SetDriving(bool mode)
    {
        _driving = mode;
    }
}