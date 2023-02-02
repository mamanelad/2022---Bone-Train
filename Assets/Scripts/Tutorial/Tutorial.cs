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

    [Space(20)] [Header("Timing")] [Header("StartHandle")] [SerializeField]
    private float handleShowTime = 1.5f;

    private bool _handleWasOn;
    private bool _handleWasUse;

    [Header("Fuel Furnace & Speed Bar")] [SerializeField]
    private float fuelShowTime = 3f;

    private bool _fueSequencesIsOver;
    private bool _fuelIsOn;


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
        //Handle
        if (!_handleWasOn)
        {
            handleShowTime -= Time.deltaTime;
            if (handleShowTime <= 0)
            {
                _handleWasOn = true;
                OpenTutorialObject(TutorialObject.TutorialKind.StartHandle);
            }

            return;
        }

        if (!_handleWasUse) return;
        //FUEL FURNACE AND SPEED BAR
        if (!_fueSequencesIsOver)
        {
            fuelShowTime -= Time.deltaTime;
            if (fuelShowTime <= 0)
            {
                if (!_fuelIsOn)
                {
                    _fuelIsOn = true;
                    OpenTutorialObject(TutorialObject.TutorialKind.Fuel);
                }
            }

            return;
        }


        //
        // if (test)
        // {
        //     test = false;
        //     TestFunction();
        // }
    }

    // private void TestFunction()
    // {
    //     test = false;
    //     OpenNextTutorialObject();
    // }


    public void OpenTutorialObject(TutorialObject.TutorialKind tutorialKindToOpen)
    {
        if (!_handleWasOn) return;

        foreach (var tutorialObject in tutorialObjects)
        {
            if (tutorialObject.myKind == tutorialKindToOpen)
            {
                _currTutorialObject = tutorialObject;
                break;
            }
        }

        if (_currTutorialObject.stopTime)
            Time.timeScale = _currTutorialObject.slowMotionTime;

        if (_currTutorialObject.closeTheObjectWithTimer)
            _currTutorialObject.StartCloseRoutine();

        _currTutorialObject.gameObject.SetActive(true);
        
    }

    public void CloseTutorialObject(TutorialObject.TutorialKind tutorialKindToClose)
    {
        var objectToClose = _currTutorialObject;
        foreach (var tutorialObject in tutorialObjects)
        {
            if (tutorialObject.myKind == tutorialKindToClose)
            {
                objectToClose = tutorialObject;
                break;
            }
        }

        test = false;
        objectToClose.gameObject.SetActive(false);
        // print("got to where it needed to delete the game object");
        if (_currTutorialObject.openWithTime)
            Time.timeScale = 1;
        
        _tutorialIndex += 1;
        if (_tutorialIndex == tutorialObjects.Length)
        {
           GameManager.Shared.SetTutorialIsOn(false); 
        }
    }


    public bool IsTutorialFinished()
    {
        return !(_tutorialIndex < amountOfTutorialObjects);
    }

    public void FirstTimeHandle()
    {
        CloseTutorialObject(TutorialObject.TutorialKind.StartHandle);
        _handleWasUse = true;
        GameManager.Shared.ShakeHandle(false);
    }

    public void FirstTimeFurnace()
    {
        if (_fueSequencesIsOver) return;
        _fueSequencesIsOver = true;
        CloseTutorialObject(TutorialObject.TutorialKind.Fuel);
        OpenTutorialObject(TutorialObject.TutorialKind.SpeedMeter);
    }
    
    public void FirstTimeArrows()
    {
        CloseTutorialObject(TutorialObject.TutorialKind.Arrows);
    }

    public void NextButton()
    {
        CloseTutorialObject(_currTutorialObject.myKind);
        if (_currTutorialObject.nextObject != TutorialObject.TutorialKind.None)
            OpenTutorialObject(_currTutorialObject.nextObject);
    }
    
    public void StartJunction()
    {
        OpenTutorialObject(TutorialObject.TutorialKind.MiniMap);
    }
    // public void NextButton()
    // {
    //     var curKind = _currTutorialObject.myKind;
    //     _currTutorialObject.gameObject.SetActive(false);
    //     bool returnToRegularTime = true;
    //
    //     switch (curKind)
    //     {
    //         case TutorialObject.TutorialKind.StartHandle:
    //             break;
    //
    //         case TutorialObject.TutorialKind.Fuel:
    //             OpenTutorialObject(TutorialObject.TutorialKind.Furnace);
    //             returnToRegularTime = false;
    //             break;
    //
    //         case TutorialObject.TutorialKind.Furnace:
    //             OpenTutorialObject(TutorialObject.TutorialKind.SpeedMeter);
    //             returnToRegularTime = false;
    //             break;
    //
    //         case TutorialObject.TutorialKind.SpeedMeter:
    //             _fueSequencesIsOver = true;
    //             break;
    //
    //         case TutorialObject.TutorialKind.GoodSouls:
    //             break;
    //
    //         case TutorialObject.TutorialKind.BadSouls:
    //             break;
    //
    //         case TutorialObject.TutorialKind.Deals:
    //             break;
    //
    //         case TutorialObject.TutorialKind.MiniMap:
    //             returnToRegularTime = false;
    //             OpenTutorialObject(TutorialObject.TutorialKind.Junction);
    //             break;
    //
    //         case TutorialObject.TutorialKind.Junction:
    //             returnToRegularTime = false;
    //             OpenTutorialObject(TutorialObject.TutorialKind.Arrows);
    //             break;
    //
    //         case TutorialObject.TutorialKind.Arrows:
    //             break;
    //
    //
    //         case TutorialObject.TutorialKind.SpecialItem:
    //             break;
    //     }
    //
    //
    //     test = false;
    //
    //     print("got to where it needed to delete the game object");
    //
    //     if (returnToRegularTime)
    //     {
    //         Time.timeScale = 1;
    //     }
    // }

    // public void NextButtonFromEventObject()
    // {
    //     NextButton();
    // }

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

    public void CloseTutorial()
    {
        foreach (var tutorialObject in tutorialObjects)
        {
            tutorialObject.gameObject.SetActive(false);
        } 
    }
}