using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpeedHandle : MonoBehaviour
{
    [SerializeField] private GameObject handle;
    
    [Space(20)]
    [Header("Slider")]
    [SerializeField] private Slider slider;
    [Range(0,1)]
    private float sliderAmount;
    
    [Space(20)]
    [Header("Speed")]
    private float maxSpeed;
    private float minSpeed;
    private float curSpeed;
    
    [Space(20)]
    [Header("Rotation")]
    [SerializeField] private float maxRotation;
    
    private void Start()
    {
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck();});
        handle.transform.eulerAngles = new Vector3(0, 0, maxRotation);
        maxSpeed = GameManager.Shared.maxSpeed;
        minSpeed = GameManager.Shared.minSpeed;
        curSpeed = GameManager.Shared.GetSpeed();
        slider.value = Mathf.InverseLerp(maxSpeed, minSpeed, curSpeed);
        // ValueChangeCheck();
    }
    
    

    public void ValueChangeCheck()
    {
        sliderAmount = slider.value;
        curSpeed = Mathf.Lerp(minSpeed, maxSpeed, sliderAmount);
        GameManager.Shared.SetSpeed(curSpeed);
        RotateHandle();
    }

    private void RotateHandle()
    {
        var speedDelta = curSpeed/maxSpeed;
        var curRotation = Mathf.Lerp(-maxRotation, maxRotation, speedDelta);
        handle.transform.eulerAngles = new Vector3(0, 0, -curRotation);
    }
   
}