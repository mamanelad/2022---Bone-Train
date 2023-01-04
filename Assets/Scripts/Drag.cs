using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private float dampingSpeed = .05f;
    Canvas canvas;
    private RectTransform _draggingObjectRectTransform;
    private Vector3 _velocity = Vector3.zero;


    [SerializeField] private GameObject fuelIconPrefab;

    private void Awake()
    {
        _draggingObjectRectTransform = transform as RectTransform;
    }

    private void Update()
    {
        if (!canvas)
            canvas = FindObjectOfType<UIManager>().gameObject.GetComponent<Canvas>();
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
        newFuelIcon.transform.SetParent(canvas.transform);
        newFuelIcon.transform.localScale = transform.localScale;
        newFuelIcon.GetComponent<Drag>().SetCanvas(canvas);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(gameObject);
    }

    public void SetCanvas(Canvas newCanvas)
    {
        canvas = newCanvas;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("kaka");
    }
}