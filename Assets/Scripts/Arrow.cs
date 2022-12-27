using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    private Image _image;
    [SerializeField] private Sprite regularSprite;
    [SerializeField] private Sprite markSprite;

    public enum ArrowSide
    {
        Left,
        Right
    }

    [SerializeField] private ArrowSide _arrowSide = ArrowSide.Right;
    
    void Start()
    {
        _image = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //     ArrowSpriteHandler(ArrowSide.Left);
        // }
        //
        // if (Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     ArrowSpriteHandler(ArrowSide.Right);
        // }
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
    
    private void ChangeSpriteToMark()
    {
        _image.sprite = markSprite;
    }
    
    private void ChangeSpriteToRegular()
    {
        _image.sprite = regularSprite;
    }
}
