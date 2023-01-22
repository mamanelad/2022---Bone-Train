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
    [SerializeField] [Range(0,1.3f)] private float percentageToStartTrain = 0.95f;

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
        _minSliderAmount = _slider.value;
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
        if (wait)
        {
            if (_slider.value <= _minSliderAmount + 5)
            {
                wait = false;
            }
        }
        else
        {
            float total = _maxSliderAmount - _minSliderAmount;
            if (_sliderValue >= percentageToStartTrain * total)
            {
            
                CallToStopOrStartTrain();
            }    
        }
        
    }

    private void CallToStopOrStartTrain()
    {
        
        switch (_stopOrStart)
        {
            case StopOrStart.Stop:
                UIAudioManager.Instance.PlayTrainHalt();
                GameManager.Shared.StopTrain();
                _stopOrStart = StopOrStart.Start;
                wait = true;
                break;
                
            case StopOrStart.Start:
                UIAudioManager.Instance.PlayTrainHorn();
                GameManager.Shared.ContinueTrain();
                _stopOrStart = StopOrStart.Stop;
                wait = true;
                break;
        }
    }

    public void StartDrag()
    {
        GameManager.Shared.GetMouse().ChangeToDragMouse();
    }
    
    public void EndDrag()
    {
        GameManager.Shared.GetMouse().ChangeToIdleMouse();
    }
    
    public void PointerEnter()
    {
        GameManager.Shared.GetMouse().ChangeSizeBigger();
    }

    public void PointerExit()
    {
        GameManager.Shared.GetMouse().ChangeSizeSmaller();
    }
}