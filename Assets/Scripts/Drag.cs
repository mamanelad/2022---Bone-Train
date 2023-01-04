using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject fuelIconPrefab;
    [SerializeField] private float dampingSpeed = .05f;
    
    private Canvas _canvas;
    private RectTransform _draggingObjectRectTransform;
    private Vector3 _velocity = Vector3.zero;
    private Furnace _furnace;

    private int i = 0;
    private void Awake()
    {
        _draggingObjectRectTransform = transform as RectTransform;
    }

    private void Update()
    {
        if (!_canvas)
            _canvas = FindObjectOfType<UIManager>().gameObject.GetComponent<Canvas>();

        if (!_furnace)
            _furnace = FindObjectOfType<Furnace>();

        if (RectOverlap(transform.GetComponent<RectTransform>(), _furnace.GetComponent<RectTransform>()))
            TouchFurnace();
        
        
    }

    private void TouchFurnace()
    {
        
    }

    private bool RectOverlap(RectTransform firstRect, RectTransform secondRect)
    {
        if (firstRect.position.x + firstRect.rect.width * 0.5f < secondRect.position.x - secondRect.rect.width * 0.5f)
        {
            return false;
        }

        if (secondRect.position.x + secondRect.rect.width * 0.5f < firstRect.position.x - firstRect.rect.width * 0.5f)
        {
            return false;
        }

        if (firstRect.position.y + firstRect.rect.height * 0.5f < secondRect.position.y - secondRect.rect.height * 0.5f)
        {
            return false;
        }

        if (secondRect.position.y + secondRect.rect.height * 0.5f < firstRect.position.y - firstRect.rect.height * 0.5f)
        {
            return false;
        }

        return true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggingObjectRectTransform, eventData.position,
            eventData.pressEventCamera, out var globalMousePosition))
        {
            _draggingObjectRectTransform.position =
                Vector3.SmoothDamp(_draggingObjectRectTransform.position, globalMousePosition, ref _velocity,
                    dampingSpeed);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var newFuelIcon = Instantiate(fuelIconPrefab, transform.position, Quaternion.identity);
        newFuelIcon.transform.SetParent(_canvas.transform);
        newFuelIcon.transform.localScale = transform.localScale;
        newFuelIcon.GetComponent<Drag>().SetCanvas(_canvas);
        newFuelIcon.GetComponent<Drag>().SetFurnace(_furnace);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(gameObject);
    }

    public void SetCanvas(Canvas newCanvas)
    {
        _canvas = newCanvas;
    }
    
    public void SetFurnace(Furnace newFurnace)
    {
        _furnace = newFurnace;
    }
}