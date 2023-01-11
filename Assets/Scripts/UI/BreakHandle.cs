using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BreakHandle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform _draggingObjectRectTransform;
    private Vector3 _velocity = Vector3.zero;
    [SerializeField] private float dampingSpeed = .05f;

    [SerializeField] private Transform centerPos;
    [SerializeField] private Transform handlePos;

    [SerializeField] private GameObject handle;
    private RectTransform _handleRectTransform;

    private Vector3 _globalMousePositionOld;
    private Vector3 _globalMousePositionCur;
    private float curAngle;

    [SerializeField] private float one;
    [SerializeField] private float two;
    [SerializeField] private float tree;
    [SerializeField] private float four;
    
    private void Awake()
    {
        _draggingObjectRectTransform = transform as RectTransform;
        _handleRectTransform = handle.GetComponent<Transform>() as RectTransform; 
      
        
    }

    

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggingObjectRectTransform, eventData.position,
            eventData.pressEventCamera, out var globalMousePosition))
        {
            Vector2 oldV = _globalMousePositionOld - centerPos.position;
            Vector2 newV = globalMousePosition - centerPos.position;
            curAngle = Vector2.SignedAngle(oldV, newV);
            
            Vector3 newRotation = _handleRectTransform.eulerAngles;
            
            // float newZ = Mathf.SmoothDamp(newRotation.z, newRotation.z + curAngle, ref _currentVelocity, Mathf.Sqrt(smoothFactor * Time.deltaTime));
            
           
            // newRotation.z = newZ;
            newRotation.z += curAngle;
            
            float z = newRotation.z %360;
            if (betweenRange(z, one, two) || betweenRange(z,  tree, four))
            {
                _handleRectTransform.eulerAngles = Vector3.SmoothDamp(_handleRectTransform.eulerAngles , newRotation, ref _velocity,
                    dampingSpeed);
                _globalMousePositionOld = globalMousePosition;
                // _handleRectTransform.eulerAngles = newRotation;
            }
        }
    }

    
    private bool betweenRange(float toCheck, float min, float max)
    {
        return min <= toCheck && toCheck <= max;
    }
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        _globalMousePositionOld = transform.position;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        // print("return to startRotation");
    }
}
