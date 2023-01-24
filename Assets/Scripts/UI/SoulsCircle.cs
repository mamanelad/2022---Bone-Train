using System;
using System.Collections;
using System.Collections.Generic;
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


    [Space(30)] [Header("Test")] [SerializeField]
    private bool test;

    [SerializeField] private float goodSoulsTest;
    [SerializeField] private float badSoulsTest;
    private float _fillAmountTest;
    private bool _initFinish;

    public enum WhenTheFunctionIsCalled
    {
        OnInit,
        OnPlay
    }


    void Start()
    {
        ChangeSoulsAmount(WhenTheFunctionIsCalled.OnInit);
    }


    private void Update()
    {
        if (test)
        {
            TestFunction();
        }

        if (_initFinish)
        {
            UpdateSoulsBar(WhenTheFunctionIsCalled.OnPlay);
        }
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

        if (when == WhenTheFunctionIsCalled.OnInit)
            fillAmountOld = Mathf.Lerp(minFillAmount, maxFillAmount, badSoulsPercentageFromTotal);
        else
            _fillAmountNew = Mathf.Lerp(minFillAmount, maxFillAmount, badSoulsPercentageFromTotal);
    }


    private void UpdateSoulsBar(WhenTheFunctionIsCalled when = WhenTheFunctionIsCalled.OnPlay)
    {
        if (test) return;
        if (when == WhenTheFunctionIsCalled.OnPlay)
        {
            if (fillAmountOld < _fillAmountNew)
                fillAmountOld += 1;
            else
                fillAmountOld -= 1;
        }
        
        badSoulsImage.fillAmount = fillAmountOld;
        blackMarkImage.fillAmount = fillAmountOld + addToBlack;
    }

    private void TestFunction()
    {
        print("kaka");
        float totalSoulsAmount = goodSoulsTest + badSoulsTest;
        float badSoulsPercentageFromTotal = badSoulsTest / totalSoulsAmount;
        _fillAmountTest = Mathf.Lerp(minFillAmount, maxFillAmount, badSoulsPercentageFromTotal);
        print(_fillAmountTest);
        badSoulsImage.fillAmount = _fillAmountTest;
        blackMarkImage.fillAmount = _fillAmountTest + addToBlack;
    }
}