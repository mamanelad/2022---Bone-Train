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
    [Header("By Time")]
    [SerializeField] private int decreesSpeedLow;
    [SerializeField] private int decreesSpeedHigh;
    [Header("By Player")]
    [SerializeField] private int addToSpeedFuel;
    [SerializeField] private int addToSpeedGoodSoul;
    [SerializeField] private int addToSpeedBadSoul;
    
    
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

        // addToSpeedFuel = GameManager.Shared.addToSpeedFuel;
        // addToSpeedGoodSoul = GameManager.Shared.addToSpeedGoodSoul;
        // addToSpeedBadSoul = GameManager.Shared.addToSpeedBadSoul;

        _decreesTimer = decreesTime;
    }

    private void Update()
    {
        DecreesSpeedHandler();
        ChangeSprite();
    }

    private void DecreesSpeedHandler()
    {
        
        _decreesTimer -= Time.deltaTime;
        if (_decreesTimer <= 0)
        {
            var speedState = GameManager.Shared.GetSpeedState();

            var amountToDecrees = decreesSpeedLow;

            switch (speedState)
            {
                case GameManager.SpeedState.Stop:
                    return;

                case GameManager.SpeedState.Low:
                    amountToDecrees = decreesSpeedHigh; 
                    break;
            }

            var newSpeed = GameManager.Shared.GetSpeed() - amountToDecrees;
            
            _decreesTimer = decreesTime;
            GameManager.Shared.SetSpeed(newSpeed);
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
        var newSpeed = GameManager.Shared.GetSpeed() + addSpeed;
        if (newSpeed >= maxSpeed)
            return;

        GameManager.Shared.SetSpeed(newSpeed);
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