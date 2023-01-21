using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulsCircle : MonoBehaviour
{
    [SerializeField] private Image badSoulsImage;
    [SerializeField] private Image blackMarkImage;
    [SerializeField] [Range(0f, 0.25f)] private float _fillAmount;

    [SerializeField] private float minFillAmount = 0f;
    [SerializeField] private float maxFillAmount = 0.25f;

    [SerializeField] [Range(0.01f, 0.1f)] private float addToBlack;

    private float _goodSouls;
    private float _badSouls;


    [Space(30)] [Header("Test")] [SerializeField]
    private bool test;

    [SerializeField] private float goodSoulsTest;
    [SerializeField] private float badSoulsTest;
    private float fillAmountTest;

    void Start()
    {
        ChangeSoulsAmount();
        
    }

    private void Update()
    {
        if (test)
        {
            TestFunction();
        }
    }


    public void ChangeSoulsAmount()
    {
        _goodSouls = GameManager.Shared.GetGoodSouls();
        _badSouls = GameManager.Shared.GetBadSouls();
        CalculateFillAmount();
        UpdateSoulsBar();
    }

    private void CalculateFillAmount()
    {
        float totalSoulsAmount = _goodSouls + _badSouls;
        float badSoulsPercentageFromTotal = _badSouls / totalSoulsAmount;
        _fillAmount = Mathf.Lerp(minFillAmount, maxFillAmount, badSoulsPercentageFromTotal);
    }


    private void UpdateSoulsBar()
    {
        if (test) return;
        badSoulsImage.fillAmount = _fillAmount;
        blackMarkImage.fillAmount = _fillAmount + addToBlack;
    }

    private void TestFunction()
    {
        print("kaka");
        float totalSoulsAmount = goodSoulsTest + badSoulsTest;
        float badSoulsPercentageFromTotal = badSoulsTest / totalSoulsAmount;
        fillAmountTest = Mathf.Lerp(minFillAmount, maxFillAmount, badSoulsPercentageFromTotal);
        print(fillAmountTest);
        badSoulsImage.fillAmount = fillAmountTest;
        blackMarkImage.fillAmount = fillAmountTest + addToBlack;
    }
}