using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class BreakChain : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float score;
    [SerializeField] private float smoothFactor;
    [SerializeField] private float percentageToStartTrain = 0.95f;

    private float _maxSliderAmount;
    private float _minSliderAmount;
    private bool _startTrain;
    private float _currentVelocity = 0;
    private float _sliderValue;
    private bool wait;
    
    private enum StopOrStart
    {
        Stop,
        Start
    }

    private StopOrStart _stopOrStart = StopOrStart.Start;

    private void Start()
    {
        _slider.onValueChanged.AddListener((newVal => { _sliderValue = newVal; }));
        _maxSliderAmount = _slider.maxValue;
        _minSliderAmount = _slider.minValue;
        GameManager.Shared.StopTrain();
    }


    private void Update()
    {
        float curScore = Mathf.SmoothDamp(_sliderValue, score, ref _currentVelocity, smoothFactor * Time.deltaTime);
        _slider.value = curScore;
        CalculatePercentage();
    }

    private void CalculatePercentage()
    {
        float total = _maxSliderAmount - _minSliderAmount;
        if (_sliderValue >= percentageToStartTrain * total)
        {
            CallToStopOrStartTrain();
        }
    }

    private void CallToStopOrStartTrain()
    {
        switch (_stopOrStart)
        {
            case StopOrStart.Stop:
                GameManager.Shared.StopTrain();
                _stopOrStart = StopOrStart.Start;
                wait = true;
                break;
                
            case StopOrStart.Start:
                GameManager.Shared.ContinueTrain();
                _stopOrStart = StopOrStart.Stop;
                wait = true;
                break;
        }
    }
}