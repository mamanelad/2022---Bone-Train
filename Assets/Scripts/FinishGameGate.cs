using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGameGate : MonoBehaviour
{
    [SerializeField] private Animator fadeOutAnimator;
    [SerializeField] private GameObject fadeOutGameObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
            StartCoroutine(FadeOut());
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