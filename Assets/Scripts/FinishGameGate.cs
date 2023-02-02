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

    private IEnumerator FadeOut()
    {
        // stop eating boolean
        fadeOutGameObject.SetActive(true);
        fadeOutAnimator.SetTrigger("Fade Out");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Game win", LoadSceneMode.Single);
    }
}