using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TutorialObject.TutorialKind[] orderOfTutorial;
    [SerializeField] private TutorialObject[] tutorialObjects;

    private int _tutorialIndex = 0;
    [SerializeField] private int amountOfTutorialObjects;
    private TutorialObject _currTutorialObject;

    void Start()
    {
        if (amountOfTutorialObjects == 0)
            amountOfTutorialObjects = tutorialObjects.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    public void OpenNextTutorialObject()
    {
        Time.timeScale = 0;
        TutorialObject.TutorialKind tutorialKindToOpen = orderOfTutorial[_tutorialIndex];

        foreach (var tutorialObject in tutorialObjects)
        {
            if (tutorialObject.GetTutorialKind() == tutorialKindToOpen)
            {
                _currTutorialObject = tutorialObject;
                break;
            }       
        }
        
        _currTutorialObject.gameObject.SetActive(true);
        _tutorialIndex += 1;
    }


    public void NextButton()
    {
        
        _currTutorialObject.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    public bool IsTutorialFinished()
    {
        return _tutorialIndex < amountOfTutorialObjects;
    }
}