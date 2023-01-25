using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite regularSprite;
    [SerializeField] private Sprite markSprite;
    [SerializeField] private Arrow otherArrow;

    [SerializeField] private Color mouseOverColor = Color.red;
    [SerializeField] private Color arrowSelectedColor = Color.red;
    [SerializeField] private Color regularColor = Color.white;
    private Color _lastColor;

    private bool isMouseOn;
    private bool isPressed;

    public enum ArrowSide
    {
        None,
        Left,
        Right
    }

    [SerializeField] private ArrowSide _arrowSide;


    public void ArrowHandler(ArrowSide sideToMark, string funcName = "dont know")
    {
        if (sideToMark == _arrowSide)
        {
            // print("got to arrow handler from " + funcName);
            ChangeSpriteToMark(true);
        }
        else
        {
            ChangeSpriteToRegular();
        }
    }


    public void ChangeColor(Color color, bool mood = false)
    {
        if (false) return;
        _image.color = color;
    }

    public Color GetRegularColor()
    {
        return regularColor;
    }

    public void CloseObject()
    {
        _lastColor = regularColor;
        // print("got to close arrows");
        _image.color = regularColor;
        _image.sprite = regularSprite;
        isPressed = false;
        gameObject.SetActive(false);
    }

    public void OpenObject()
    {
        _lastColor = regularColor;
        _image.color = regularColor;
        _image.sprite = regularSprite;
        isPressed = false;
    }


    private void ChangeSpriteToMark(bool mood = false)
    {
        if (false)
            return;

        // print("got to sprite to mark");
        GameManager.Shared.SetArrowSide(_arrowSide);
        // isPressed = true;
        _image.sprite = markSprite;
        otherArrow.ChangeSpriteToRegular();
        otherArrow.ChangeColor(regularColor);
        ChangeColor(arrowSelectedColor);
    }

    public void ChangeSpriteToRegular(bool mood = false)
    {
        // isPressed = false;
        if (!_image)
        {
            _image = GetComponent<Image>();
        }


        _image.sprite = regularSprite;
    }


    public void ClickButton()
    {
        GameManager.Shared.ReturnToRegularTime("ClickButton in arrows");
        GameManager.Shared.SetArrowSide(_arrowSide);
        ArrowHandler(_arrowSide, "ClickButton");
        otherArrow.ArrowHandler(_arrowSide, "ClickButton");
    }


    public void SetIsMouseIsOn(bool mood)
    {
        isMouseOn = mood;
    }


    public void PointerDown()
    {
        isPressed = true;
        otherArrow.isPressed = false;
        GameManager.Shared.GetMouse().ChangeToDragMouse();
        _image.color = arrowSelectedColor;
        otherArrow._image.color = regularColor;
        GameManager.Shared.SetArrowSide(_arrowSide);
    }

    public void PointerUp()
    {
        GameManager.Shared.GetMouse().ChangeToIdleMouse();
    }

    public void PointerEnter()
    {
        if (isPressed) return;
        GameManager.Shared.GetMouse().ChangeSizeBigger();
        _lastColor = _image.color;
        ChangeColor(mouseOverColor);
    }

    public void PointerExit()
    {
        if (isPressed) return;
        GameManager.Shared.GetMouse().ChangeSizeSmaller();
        ChangeColor(_lastColor);
    }
}