using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScreen : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LVL 5", LoadSceneMode.Single);
    }
    
    
    public void RetuenToStartScreen()
    {
        SceneManager.LoadScene("startscreen final");
    }
}
