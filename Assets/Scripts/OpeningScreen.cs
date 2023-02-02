using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScreen : MonoBehaviour
{
    [SerializeField] private KeyCode exitKey = KeyCode.Escape;
    public void StartGame()
    {
        SceneManager.LoadScene("LVL 5", LoadSceneMode.Single);
    }
    
    
    public void RetuenToStartScreen()
    {
        SceneManager.LoadScene("startscreen final");
    }

    private void Update()
    {
        if(Input.GetKey(exitKey))
        {
            Application.Quit();
        }
    }

    
}
