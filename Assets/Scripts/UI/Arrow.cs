using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Arrow : MonoBehaviour
{
    private Image _image;
    [SerializeField] private Sprite regularSprite;
    [SerializeField] private Sprite markSprite;
    [SerializeField] private Arrow otherArrow;
    [SerializeField] private Color mouseOverColor = Color.red;
    private Color regularColor;

    private bool isMouseOn;
    private bool isPressed;

    public enum ArrowSide
    {
        None,
        Left,
        Right
    }

    [SerializeField] private ArrowSide _arrowSide;

    void Start()
    {
        _image = GetComponent<Image>();
        regularColor = _image.color;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        ChangeColorToOnColor(!isMouseOn);
    }

    public void ArrowHandler(ArrowSide sideToMark)
    {
        if (sideToMark == _arrowSide)
        {
            ChangeSpriteToMark();
        }
        else
        {
            ChangeSpriteToRegular();
        }
    }

    public void SetIsPressedOf()
    {
        isPressed = false;
    }

    public void ChangeColorToOnColor(bool mood = true)
    {
        if (isPressed) return;
        if (mood)
            ChangeSpriteToRegular();
        Color newColor = mood ? regularColor : mouseOverColor;
        _image.color = newColor;
    }


    private void ChangeSpriteToMark()
    {
        isPressed = true;
        _image.sprite = markSprite;
        ChangeColorToOnColor(true);
    }

    public void ChangeSpriteToRegular()
    {
        isPressed = false;
        _image.sprite = regularSprite;
        ChangeColorToOnColor(false);
    }

    public void ClickButton()
    {
        GameManager.Shared.SetArrowSide(_arrowSide);
        ArrowHandler(_arrowSide);
        otherArrow.ArrowHandler(_arrowSide);
    }


    public void SetIsMouseIsOn(bool mood)
    {
        isMouseOn = mood;
    }
}