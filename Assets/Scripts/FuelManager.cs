using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelManager : MonoBehaviour
{
    [Header("Time")] [SerializeField] private float tackDownFuelTime = 1f;
    [SerializeField] private float tackDownFuelTimer;

    [Space(10)] [Header("Speed")] private float _speed;
    private float _maxSpeed = 0;
    [SerializeField] private float speedMult = 1.3f;

    [Space(10)] [Header("Extra")] [SerializeField] private bool _driving;
    [SerializeField] private int tackDownFuelAmount = 1;


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
        
        var speedDiff = _speed / _maxSpeed;
        tackDownFuelTimer -= (Time.deltaTime * speedDiff * speedMult);
        if (tackDownFuelTimer <= 0)
        {
            tackDownFuelTimer = tackDownFuelTime;
            GameManager.Shared.ChangeBySoulStones(-tackDownFuelAmount);
        }
    }

    public void SetDriving(bool mode)
    {
        _driving = mode;
    }
}