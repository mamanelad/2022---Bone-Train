using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-999)]
public class GameManager : MonoBehaviour
{
    public static GameManager Shared { get; set; }

    private void Awake()
    {
        if (Shared == null)
        {
            Shared = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    
    
}
