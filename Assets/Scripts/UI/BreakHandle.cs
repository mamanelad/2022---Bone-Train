using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class BreakHandle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private bool _goodAngle = true;
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

    private float baseRotationZ;
    [SerializeField] private bool takeDown;
    [SerializeField] private float takeDownSpeed;

    private float firstY;
    [SerializeField] private float needToAddY;
    private enum DirectionHandle
    {
        Horizon,
        Vertical
    }

    [SerializeField] private DirectionHandle _directionHandle = DirectionHandle.Vertical;
    private void Awake()
    {
        
        _draggingObjectRectTransform = transform as RectTransform;
        firstY = _draggingObjectRectTransform.position.y;
        _handleRectTransform = handle.GetComponent<Transform>() as RectTransform;
        baseRotationZ = _handleRectTransform.eulerAngles.z;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (takeDown)
        {
            var temp = _handleRectTransform.eulerAngles;
            temp.z -= takeDownSpeed*Time.deltaTime;
            
            if (betweenRange(temp.z, one, two) && betweenRange(temp.z, tree, four))
            {
                _handleRectTransform.eulerAngles = temp; 
                
            }
            else
            {
                  takeDown = false;
            }
            
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggingObjectRectTransform,
            eventData.position,
            eventData.pressEventCamera, out var globalMousePosition))
        {
            Vector2 oldV = _globalMousePositionOld - centerPos.position;
            Vector2 newV = globalMousePosition - centerPos.position;
            curAngle = Vector2.SignedAngle(oldV, newV);

            Vector3 newRotation = _handleRectTransform.eulerAngles;

            // float newZ = Mathf.SmoothDamp(newRotation.z, newRotation.z + curAngle, ref _currentVelocity, Mathf.Sqrt(smoothFactor * Time.deltaTime));
            if (curAngle <=0 )
            {
                return;
            }

            // newRotation.z = newZ;
            newRotation.z += curAngle;

            float z = newRotation.z;
            if (z > 360)
            {
                z -= 360;
            }
            
            
            if (betweenRange(z, one, two) && betweenRange(z, tree, four))
            {
                // print(z);
                _handleRectTransform.eulerAngles = Vector3.SmoothDamp(_handleRectTransform.eulerAngles, newRotation,
                    ref _velocity,
                    dampingSpeed);
                // print(_handleRectTransform.rotation.z);    
                _globalMousePositionOld = globalMousePosition;
                // _handleRectTransform.eulerAngles = newRotation;
            }
            else
            {
                
            }
        }
    }


    private bool betweenRange(float toCheck, float min, float max)
    {
        // return true;
        return (!(min <= toCheck && toCheck <= max));
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        takeDown = false;
        _globalMousePositionOld = transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_handleRectTransform.transform.position.y > firstY + needToAddY)
        {
            print("kaka");
            GameManager.Shared.StopTrain();
        }
        takeDown = true;
        // print("return to startRotation");
    }
}