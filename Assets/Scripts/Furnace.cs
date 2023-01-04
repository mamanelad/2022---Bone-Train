using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    [Header("Speed")] private float maxSpeed;
    private float minSpeed;
    private float curSpeed;

    [Space(10)] [Header("Change Speed Amounts")]
    private int addToSpeedFuel;
    private int addToSpeedGoodSoul;
    private int addToSpeedBadSoul;

    [SerializeField] private int decreesSpeed;

    [Space(10)] [Header("Times")] private float decreesTime = .5f;
    private float _decreesTimer;

    enum BurnObject
    {
        Fuel,
        GoodSoul,
        BadSoul
    }
    void Start()
    {
        maxSpeed = GameManager.Shared.maxSpeed;
        minSpeed = GameManager.Shared.minSpeed;
        curSpeed = GameManager.Shared.GetSpeed();

        _decreesTimer = decreesTime;
    }

    private void Update()
    {
        DecreesSpeedHandler();
    }

    private void DecreesSpeedHandler()
    {
        var newSpeed = curSpeed - decreesSpeed; 
        if (newSpeed <= minSpeed)
            return;

        _decreesTimer -= Time.deltaTime;
        if (_decreesTimer <= 0)
        {
            _decreesTimer = decreesTime;
            curSpeed = newSpeed;
            GameManager.Shared.SetSpeed(curSpeed);
        }
    }

    public void AddSpeed()
    {
        
    }
    
}