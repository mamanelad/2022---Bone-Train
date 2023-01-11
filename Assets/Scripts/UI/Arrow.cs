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
    private Color baseColor;
    
    private bool isMouseOn;

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
        baseColor = _image.color;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        ChangeColorToMouseOver(isMouseOn ? mouseOverColor : baseColor);
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

    public void ChangeColorToMouseOver(Color newColor)
    {
        _image.color = newColor;
    }
    private void ChangeSpriteToMark()
    {
        _image.sprite = markSprite;
    }
    
    private void ChangeSpriteToRegular()
    {
        _image.sprite = regularSprite;
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
