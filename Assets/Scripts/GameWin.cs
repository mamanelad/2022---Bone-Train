using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private KeyCode exitKey = KeyCode.Escape;

    [SerializeField] public TextMeshProUGUI goodSoulsNumberText;

    // Start is called before the first frame update
    void Start()
    {
        goodSoulsNumberText.text =
            $"You left the gates of Hell with {gameData.goodSoulsAmount.ToString()} innocent souls, WELL DONE !";
    }

    private void Update()
    {
        if (Input.GetKey(exitKey))
        {
            Application.Quit();
        }
    }

    public void ReturnToStartScreenButton()
    {
        SceneManager.LoadScene("startscreen final");
    }
}