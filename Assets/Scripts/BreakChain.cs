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
    [SerializeField] private float percentageToStopTrain = 0.95f;

    private float _maxSliderAmount;
    private float _minSliderAmount;
    private bool _stopTrain;
    private float _currentVelocity = 0;
    private float _sliderValue;
    private void Start()
    {
        _slider.onValueChanged.AddListener((newVal => { _sliderValue = newVal;} ));
        _maxSliderAmount = _slider.maxValue;
        _minSliderAmount = _slider.minValue;
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
        // print("slider value is: " + _sliderValue);
        // print("auther thing is: " + percentageToStopTrain*total);
        if (_sliderValue >= percentageToStopTrain*total)
        {
            CallToStopTrain();    
        }
    }

    private void CallToStopTrain()
    {
        if (!_stopTrain)
        {
            _stopTrain = true;
            print("need to stop the train");
            GameManager.Shared.StopTrain();
        }   
    }
}
