using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScreen : MonoBehaviour
{
    public void StartGame()
    {
        print("AAAAA");
        SceneManager.LoadScene("LVL 3", LoadSceneMode.Single);
    }
}
