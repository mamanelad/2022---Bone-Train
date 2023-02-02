using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGameGate : MonoBehaviour
{
    [SerializeField] private Animator fadeOutAnimator;
    [SerializeField] private GameObject fadeOutGameObject;
    [SerializeField] private KeyCode exitKey = KeyCode.Escape;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            StartCoroutine(FadeOut());
            FindObjectOfType<EnemyManager>().lockEnemies = true;
        }
            
    }

    public IEnumerator FadeOut(bool lostGame = false)
    {
        // stop eating boolean
        fadeOutGameObject.SetActive(true);
        fadeOutAnimator.SetTrigger("Fade Out");
        yield return new WaitForSeconds(5f);
        if (lostGame)
            SceneManager.LoadScene("Game lose", LoadSceneMode.Single);
        else
            SceneManager.LoadScene("Game win", LoadSceneMode.Single);
    }
}