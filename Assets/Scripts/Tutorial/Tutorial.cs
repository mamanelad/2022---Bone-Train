using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TutorialObject.TutorialKind[] orderOfTutorial;
    [SerializeField] private TutorialObject[] tutorialObjects;

    private int _tutorialIndex = 0;
    private int amountOfTutorialObjects;
    private TutorialObject _currTutorialObject;
    private int _objectIndex;
    private bool _finished;


    [Header("Test")] public bool test;
    private bool _objectIsOn = false;

    void Start()
    {
        _currTutorialObject = tutorialObjects[0];
        amountOfTutorialObjects = tutorialObjects.Length;
    }


    // Update is called once per frame
    void Update()
    {

        if (test)
        {
            test = false;
            TestFunction();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            print("got to where it needed to delete the game object --- before");
            test = false;
            NextButton();
            _currTutorialObject.gameObject.SetActive(false);
        }
    }

    private void TestFunction()
    {
        test = false;
        OpenNextTutorialObject();
    }


    public void OpenNextTutorialObject()
    {
        if (IsTutorialFinished())
        {
            GameManager.Shared.SetTutorialIsOn(false);
            _finished = true;
            print("tutorial finished");
            gameObject.SetActive(false);
            return;
        }
        
        Time.timeScale = 0;
        TutorialObject.TutorialKind tutorialKindToOpen = orderOfTutorial[_tutorialIndex];

        foreach (var tutorialObject in tutorialObjects)
        {
            if (tutorialObject.myKind == tutorialKindToOpen)
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
        test = false;
        _currTutorialObject.gameObject.SetActive(false);
        print("got to where it needed to delete the game object");
        Time.timeScale = 1;
    }

    public void NextButtonFromEventObject()
    {
        NextButton();
    }

    public bool IsTutorialFinished()
    {
        return !(_tutorialIndex < amountOfTutorialObjects);
    }
}