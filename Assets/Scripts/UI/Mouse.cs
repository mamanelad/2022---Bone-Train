using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Mouse : MonoBehaviour
{
    [SerializeField] private Texture2D idleMouseTextureSmall;
    [SerializeField] private Texture2D idleMouseTextureBig;
    [SerializeField] private Texture2D dragMouseTexture;
    [SerializeField] private bool isMouseCloseEveryWhere;

    void Start()
    {
        Cursor.SetCursor(idleMouseTextureSmall, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void Update()
    {
        
        if (isMouseCloseEveryWhere)
            MouseCloseEveryWhere();
        
    }

    private void MouseCloseEveryWhere()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeToDragMouse();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ChangeToIdleMouse();
        }
    }

    public void ChangeToDragMouse()
    {
        Cursor.SetCursor(dragMouseTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ChangeToIdleMouse()
    {
        Cursor.SetCursor(idleMouseTextureSmall, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ChangeSizeBigger()
    {
        if (UIAudioManager.Instance)
            UIAudioManager.Instance.PlayUIHoverEvent();
        Cursor.SetCursor(idleMouseTextureBig, Vector2.zero, CursorMode.ForceSoftware);

    }
    
    public void ChangeSizeSmaller()
    {
        ChangeToIdleMouse();
    }
}