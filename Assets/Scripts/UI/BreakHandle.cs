using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BreakHandle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private bool _goodAngle = true;
    private RectTransform _draggingObjectRectTransform;
    private Vector3 _velocity = Vector3.zero;
    [SerializeField] private float dampingSpeed = .05f;

    [SerializeField] private Transform centerPos;
    [SerializeField] private Transform handlePos;
    [SerializeField] private Transform up;

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

    private Image _image;
    private float firstY;
    [SerializeField] private float needToAddY;
    private Vector3 startPosition;

    [SerializeField] private float difX = 0.5f;

    private float mySecretFloat = 0f;

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
        _image = GetComponent<Image>();
        startPosition = transform.position;
    }

    private void Start()
    {
    }

    private void Update()
    {
        print(_handleRectTransform.transform.position.y);
        if (takeDown)
        {
            TackDown();
        }
        
    }

    private void TackDown()
    {
        
            var temp = _handleRectTransform.eulerAngles;
            temp.z -= takeDownSpeed * Time.deltaTime;
            mySecretFloat -= takeDownSpeed * Time.deltaTime;
            
            
            if (mySecretFloat - 0.1f >= 0) 
            {
                _handleRectTransform.eulerAngles = temp;
            }
            else
            {
                mySecretFloat = 0;
                var newCol = _image.color;
                newCol.a = 255;
                _image.color = newCol;
                takeDown = false;
                transform.position = handlePos.transform.position;
            }
        
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        var newCol = _image.color;
        newCol.a = 0;
        _image.color = newCol;


        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggingObjectRectTransform,
            eventData.position,
            eventData.pressEventCamera, out var globalMousePosition))
        {
            Vector3 oldV = _globalMousePositionOld - centerPos.position;
            Vector3 newV = globalMousePosition - centerPos.position;


            curAngle = Vector2.SignedAngle(oldV, newV);
            Vector3 newRotation = _handleRectTransform.eulerAngles;

            newRotation.z += curAngle;
            mySecretFloat += curAngle;
            
            if (!(CheckIfGotUp()))
            {
                _handleRectTransform.eulerAngles = newRotation;
                _globalMousePositionOld = globalMousePosition;
                _draggingObjectRectTransform.position = globalMousePosition;
                    // Vector3.SmoothDamp(_draggingObjectRectTransform.position, globalMousePosition, ref _velocity,
                    //     dampingSpeed);
            }
            else
            {
                GameManager.Shared.StopTrain();
                
            }
            
        }
    }


    private bool CheckIfGotUp()
    {
        return (up.position.y <= transform.position.y);
    }

    private bool CheckIfGotDown()
    {
        if (handlePos.transform.position.y + needToAddY > transform.position.y)
        {
            if (handlePos.transform.position.x  < transform.position.x)
            {
                return true;
            }
        }

        
        return false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        takeDown = false;
        _globalMousePositionOld = transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        takeDown = true;
    }
}