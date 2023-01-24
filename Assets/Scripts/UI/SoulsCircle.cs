using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoulsCircle : MonoBehaviour
{
    [SerializeField] private Image badSoulsImage;
    [SerializeField] private Image blackMarkImage;
    [SerializeField] [Range(0f, 0.25f)] private float fillAmountOld;
    [Range(0f, 0.25f)] private float _fillAmountNew;

    [SerializeField] private float minFillAmount = 0f;
    [SerializeField] private float maxFillAmount = 0.25f;

    [SerializeField] [Range(0.01f, 0.1f)] private float addToBlack;

    private float _goodSouls;
    private float _badSouls;

    [Space(20)] [Header("Times")] [SerializeField]
    private float changeBarTime = 0.1f;
<<<<<<< HEAD
=======

    [SerializeField] [Range(0, 1)] private float addFillBy = 0.1f;

>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
    private float _changeBarTimer;

    [Space(30)] [Header("Test")] [SerializeField]
    private bool test;

    [SerializeField] private float goodSoulsTest;
    [SerializeField] private float badSoulsTest;
    private float _fillAmountTest;
    private bool _initFinish;
<<<<<<< HEAD

=======
    
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
    public enum WhenTheFunctionIsCalled
    {
        OnInit,
        OnPlay
    }


    void Start()
    {
<<<<<<< HEAD
        ChangeSoulsAmount(WhenTheFunctionIsCalled.OnInit);
=======
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
        _changeBarTimer = changeBarTime;
    }


    private void Update()
    {
<<<<<<< HEAD
        if (test)
        {
            TestFunction();
        }

        if (_initFinish)
        {
=======
        
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
            _changeBarTimer -= Time.deltaTime;
            if (_changeBarTimer <= 0)
            {
                _changeBarTimer = changeBarTime;
                UpdateSoulsBar();
            }
<<<<<<< HEAD
            
        }
=======
        
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
    }


    public void ChangeSoulsAmount(WhenTheFunctionIsCalled when = WhenTheFunctionIsCalled.OnPlay)
    {
        _goodSouls = GameManager.Shared.GetGoodSouls();
        _badSouls = GameManager.Shared.GetBadSouls();
        CalculateFillAmount(when);
        UpdateSoulsBar(when);
    }

    private void CalculateFillAmount(WhenTheFunctionIsCalled when = WhenTheFunctionIsCalled.OnPlay)
    {
        float totalSoulsAmount = _goodSouls + _badSouls;
        float badSoulsPercentageFromTotal = _badSouls / totalSoulsAmount;
<<<<<<< HEAD

        if (when == WhenTheFunctionIsCalled.OnInit)
            fillAmountOld = Mathf.Lerp(minFillAmount, maxFillAmount, badSoulsPercentageFromTotal);
        else
            _fillAmountNew = Mathf.Lerp(minFillAmount, maxFillAmount, badSoulsPercentageFromTotal);
=======
        
        _fillAmountNew = Mathf.Lerp(minFillAmount, maxFillAmount, badSoulsPercentageFromTotal);
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
    }


    private void UpdateSoulsBar(WhenTheFunctionIsCalled when = WhenTheFunctionIsCalled.OnPlay)
    {
<<<<<<< HEAD
        if (test) return;
        if (when == WhenTheFunctionIsCalled.OnPlay)
        {
            if (fillAmountOld < _fillAmountNew)
                fillAmountOld += 1;
            else
                fillAmountOld -= 1;
        }
=======
        if (when == WhenTheFunctionIsCalled.OnPlay)
            fillAmountOld = Mathf.Lerp(fillAmountOld, _fillAmountNew, addFillBy);
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
        
        badSoulsImage.fillAmount = fillAmountOld;
        blackMarkImage.fillAmount = fillAmountOld + addToBlack;
    }

    private void TestFunction()
    {
        float totalSoulsAmount = goodSoulsTest + badSoulsTest;
        float badSoulsPercentageFromTotal = badSoulsTest / totalSoulsAmount;
        _fillAmountTest = Mathf.Lerp(minFillAmount, maxFillAmount, badSoulsPercentageFromTotal);
<<<<<<< HEAD
        print(_fillAmountTest);
=======

        
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
        badSoulsImage.fillAmount = _fillAmountTest;
        blackMarkImage.fillAmount = _fillAmountTest + addToBlack;
    }
}