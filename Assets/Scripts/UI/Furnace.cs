using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnace : MonoBehaviour
{
    [Header("Speed")] private float maxSpeed;
    private float minSpeed;
    [SerializeField] private float curSpeed;

    [Space(10)] [Header("Change Speed Amounts")]
    private int addToSpeedFuel;

    private int addToSpeedGoodSoul;
    private int addToSpeedBadSoul;

    [SerializeField] private int decreesSpeed;

    [Space(10)] [Header("Times")] 
    [SerializeField] private float decreesTime = .5f;
    private float _decreesTimer;

    [Space(10)] [Header("Sprites")] 
    [SerializeField] private Sprite maxFurnaceSprite;
    [SerializeField] private Sprite midFurnaceSprite;
    [SerializeField] private Sprite lowFurnaceSprite;
    [SerializeField] private Sprite stopFurnaceSprite;

    public enum BurnObject
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

        addToSpeedFuel = GameManager.Shared.addToSpeedFuel;
        addToSpeedGoodSoul = GameManager.Shared.addToSpeedGoodSoul;
        addToSpeedBadSoul = GameManager.Shared.addToSpeedBadSoul;

        _decreesTimer = decreesTime;
    }

    private void Update()
    {
        DecreesSpeedHandler();
        ChangeSprite();
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

    public void AddSpeed(BurnObject objectToBurn)
    {
        switch (objectToBurn)
        {
            case BurnObject.Fuel:
                AddSpeedHelper(addToSpeedFuel);
                break;

            case BurnObject.GoodSoul:
                AddSpeedHelper(addToSpeedGoodSoul);
                break;

            case BurnObject.BadSoul:
                AddSpeedHelper(addToSpeedBadSoul);
                break;
        }
    }

    private void AddSpeedHelper(float addSpeed)
    {
        var newSpeed = curSpeed + addSpeed;
        if (newSpeed >= maxSpeed)
            return;

        curSpeed = newSpeed;
        GameManager.Shared.SetSpeed(curSpeed);
    }

    private void ChangeSprite()
    {
        var image = GetComponent<Image>();
        var speedState = GameManager.Shared.GetSpeedState();
        
        switch (speedState)
        {
            case GameManager.SpeedState.Max:
                image.sprite = maxFurnaceSprite;
                break;
            
            case GameManager.SpeedState.Mid:
                image.sprite = midFurnaceSprite;
                break;
            
            case GameManager.SpeedState.Low:
                image.sprite = lowFurnaceSprite;
                break;
            
            case GameManager.SpeedState.Stop:
                image.sprite = stopFurnaceSprite;
                break;
        }
    }
}