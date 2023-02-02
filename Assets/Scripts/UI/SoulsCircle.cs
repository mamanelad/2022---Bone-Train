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

    [SerializeField] [Range(0, 1)] private float addFillBy = 0.1f;

    private float _changeBarTimer;

    [Space(20)] [Header("Bad Souls")] [SerializeField] [Range(0, 1)]
    private float eatGoodSoulsPercentage;

    [SerializeField] private float eatGoodSoulsTime = 1f;
    private float _eatGoodSoulsTimer;

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
        _changeBarTimer = changeBarTime;
        _eatGoodSoulsTimer = eatGoodSoulsTime;
    }


    private void Update()
    {
        _changeBarTimer -= Time.deltaTime;
        if (_changeBarTimer <= 0)
        {
            _changeBarTimer = changeBarTime;
            UpdateSoulsBar();
        }
    }

    private void FixedUpdate()
    {
        _eatGoodSoulsTimer -= Time.deltaTime;
        if (_eatGoodSoulsTimer <= 0)
        {
            _eatGoodSoulsTimer = eatGoodSoulsTime;
            GoodSoulsEater();
        }
    }

    private void GoodSoulsEater()
    {
        var goodSoulsAmount = GameManager.Shared.GetGoodSouls();
        if (goodSoulsAmount > 0)
        {
            var percentage = eatGoodSoulsPercentage;
            if (GameManager.Shared.GetSpeedState() == GameManager.SpeedState.Stop)
                percentage /= 2;

            var badSoulsAmount = GameManager.Shared.GetBadSouls();
            int badSoulsToAdd = (int) Mathf.Floor(badSoulsAmount * percentage);
            GameManager.Shared.ChangeByBadSouls(badSoulsToAdd);
            GameManager.Shared.ChangeByGoodSouls(-badSoulsToAdd);
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

        _fillAmountNew = Mathf.Lerp(minFillAmount, maxFillAmount, badSoulsPercentageFromTotal);
    }


    private void UpdateSoulsBar(WhenTheFunctionIsCalled when = WhenTheFunctionIsCalled.OnPlay)
    {
        if (when == WhenTheFunctionIsCalled.OnPlay)
            fillAmountOld = Mathf.Lerp(fillAmountOld, _fillAmountNew, addFillBy);

        badSoulsImage.fillAmount = fillAmountOld;
        blackMarkImage.fillAmount = fillAmountOld + addToBlack;
    }

    private void TestFunction()
    {
        float totalSoulsAmount = goodSoulsTest + badSoulsTest;
        float badSoulsPercentageFromTotal = badSoulsTest / totalSoulsAmount;
        _fillAmountTest = Mathf.Lerp(minFillAmount, maxFillAmount, badSoulsPercentageFromTotal);


        badSoulsImage.fillAmount = _fillAmountTest;
        blackMarkImage.fillAmount = _fillAmountTest + addToBlack;
    }
}