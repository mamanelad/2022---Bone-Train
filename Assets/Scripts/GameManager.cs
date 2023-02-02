using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[DefaultExecutionOrder(-999)]
public class GameManager : MonoBehaviour
{
    [Header("Event")] private InteractionData _currEventData;
    private bool _inEvent;
    private bool _gotToNewEvent;
    private InteractionManager _interactionManager;
    [SerializeField] private InteractionData devilEvent;


    [Space(10)] [Header("UI")] private UIManager _uiManager;
    [SerializeField] public int soulStonesInitializeValue;
    [SerializeField] public int goodSoulsInitializeValue;
    [SerializeField] public int badSoulsInitializeValue;
    [SerializeField] public int swordsInitializeValue;
    [SerializeField] public int shieldsInitializeValue;

    [SerializeField] private int maxSoulStones;
    [NonSerialized] public int SoulStones;
    [NonSerialized] public int GoodSouls;
    [NonSerialized] public int BadSouls;
    [NonSerialized] public int Swords;
    [NonSerialized] public int Shields;

    [SerializeField] private Arrow[] _arrows;
    private Arrow curMouseOnArrow;
    private bool _arrowsAreOn;
    private Arrow.ArrowSide _arrowSide = Arrow.ArrowSide.None;
    private SoulsCircle _soulsCircle;
    public bool badSoulsCanEatGoodSouls = true;
    
    [Space(20)] [Header("Extra")] public static GameManager Shared;
    [HideInInspector] public Morale morale = Morale.Neutral;
    private Mouse _mouse;
    private bool _initFinish;

    [Space(20)] [Header("Road")] public Road curRoad;

    [Space(20)] [Header("Speed")] [SerializeField]
    private bool The_Speed_Values_To_Decrees_And_Add_Are_In_The_Furnace_Script_You_Idiot;

    [SerializeField] private bool The_How_Much_Fuel_To_Decrees_Are_In_The_Fuel_Manager_Script_You_Ass;

    public float maxSpeed = 2000;

    [SerializeField] private float speedToStartBreaking = 1000;
    public float minSpeed = 0;
    [SerializeField] private float curSpeed = 500;

    // public int addToSpeedFuel;
    // public int addToSpeedGoodSoul;
    // public int addToSpeedBadSoul;

    private GameObject speedHandle;

    [SerializeField] private SpeedState speedState = SpeedState.Low;

    [Space(20)] [Header("Train")] [SerializeField]
    private GameObject train;

    [Space(20)] [Header("Fuel")] private FuelManager _fuelManager;

    [Space(20)] [Header("Take Down Amounts")] [SerializeField]
    private int takeDownFuelContinueTrain;

    [SerializeField] private int takeDownDragFuel;
    [SerializeField] private int takeDownDragGoodSouls;
    [SerializeField] private int takeDownDragBadSouls;

    [Space(20)] [Header("Slow Motion")] [Range(0, 1)] [SerializeField]
    private float arrowsAreOnSlowMotionTimeScale = 0.7f;

    [SerializeField] private float regularTimeScale = 1f;

    [Space(20)] [Header("Extra")] private Vector3 _moveDirection;
    private Vector3 _trainPosition;


    [Space(20)] [Header("Tutorial")] [SerializeField]
    private bool _tutorialIsOn;

    private Tutorial _tutorial;
    private bool _firstTimeUsingStartHandle = true;
    private bool _firstTimeAddingGoodSouls = true;
    private bool _firstTimeAddingBadSouls = true;
    private bool _firstTimeArrows = true;
    private bool _firstTimeSpecialItem = true;
    private bool _firstTimeJunction = true;
    private bool _firstTimeArrowsTutorials = true;

    [Header("Test")] [SerializeField] private bool testArrows0;
    [SerializeField] private bool testArrows;
    [SerializeField] private bool openArrows;
    [SerializeField] private bool closeArrows;

    public enum Road
    {
        Up,
        Down
    }


    public enum SpeedState
    {
        Max,
        Mid,
        Low,
        Stop,
        Run
    }


    [Space(20)] [Header("Check PointS")] private CheckPointData _CheckPointData;
    
    [Space(10)][Header("Keyboard")] [SerializeField] private KeyCode closeTutorialsKey = KeyCode.T;
    [SerializeField] private KeyCode restartKey = KeyCode.R;
    [SerializeField] private KeyCode exitKey = KeyCode.Escape;

    [Space(10)] [Header("Game Data")] [SerializeField]
    private GameData gameData;

    private void Awake()
    {
        Shared = this;
        speedState = SpeedState.Run;
        gameData.goodSoulsAmount = 0;

        // InitUiNumbers();
        InitiateCheckPointData();

        if (_arrows.Length != 2)
        {
            print("Drag Arrows to the arrow array");
        }

        if (train == null)
        {
            print("Drag the train first to the game manager");
        }

        _mouse = GetComponent<Mouse>();
        if (!_mouse)
        {
            print("Add the mouse script to the game manager object");
        }
    }


    private void Start()
    {
        _trainPosition = train.transform.position;
        _uiManager = FindObjectOfType<UIManager>();
        InitArrows();
        _soulsCircle = FindObjectOfType<SoulsCircle>();

        if (_tutorialIsOn)
        {
            if (!_tutorial)
            {
                _tutorial = FindObjectOfType<Tutorial>();
            }

            if (_tutorial)
            {
                _interactionManager.SetTutorialObject(_tutorial);
            }
            // _tutorial.OpenNextTutorialObject();
        }

        else
        {
            CloseTutorial();
        }
    }

    private void Update()
    {
        if (Input.GetKey(exitKey))
        {
            Application.Quit();
        }
        
        if (Input.GetKey(closeTutorialsKey))
        {
            CloseTutorial();
        }
        if (!_initFinish && _uiManager)
        {
            _initFinish = true;
            InitSouls();
        }

        if (Input.GetKeyDown(restartKey))
        {
            ReturnToStartScreen();
        }

        if (openArrows)
        {
            ArrowsTurnOnAndOff(openArrows);
            openArrows = false;
        }

        if (closeArrows)
        {
            ArrowsTurnOnAndOff(false);
            closeArrows = false;
        }


        if (speedState == SpeedState.Stop) curSpeed = 0;
        if (speedState == SpeedState.Run) InitSpeed();

        ArrowOverHandler();
        if ((Input.GetKeyDown(KeyCode.Escape)))
        {
            SceneManager.LoadScene("startscreen final" );
        }

        if (_interactionManager == null)
        {
            print("The event manager needs to start turn on");
            _interactionManager = FindObjectOfType<InteractionManager>();
            if (_interactionManager)
            {
                _interactionManager.gameObject.SetActive(false);
            }
        }

        if (_fuelManager == null)
        {
            _fuelManager = FindObjectOfType<FuelManager>();
        }


        SpeedStateHandler();
        bool drivingMode = speedState != SpeedState.Stop;
        _fuelManager.SetDriving(drivingMode);

        if (_uiManager == null)
            _uiManager = FindObjectOfType<UIManager>();

        if (_gotToNewEvent)
            ActivateNewEvent();

        CalculateMovingDirection();
    }

    public void ReturnToStartScreen()
    {
        SceneManager.LoadScene("startscreen final");
    }
    private void InitSpeed()
    {
        curSpeed = Mathf.Lerp(speedToStartBreaking, maxSpeed, 0.5f);
    }

    private void InitSouls()
    {
        SoulStones = soulStonesInitializeValue;
        GoodSouls = goodSoulsInitializeValue;
        BadSouls = badSoulsInitializeValue;
        Swords = swordsInitializeValue;
        Shields = shieldsInitializeValue;

        _uiManager.SetBadSouls();
        _uiManager.SetGoodSouls();
        _uiManager.SetSoulStones();
        _uiManager.SetSwords();
        _uiManager.SetShields();

        _soulsCircle.ChangeSoulsAmount(SoulsCircle.WhenTheFunctionIsCalled.OnInit);
    }

    // private void InitUiNumbers()
    // {
    //     if (_uiManager == null)
    //     {
    //         _uiManager = FindObjectOfType<UIManager>();
    //     }
    //
    //     if (_uiManager)
    //     {
    //         print("kakakkakak");
    //         ChangeByBadSouls(badSoulsInitializeValue);
    //         ChangeByGoodSouls(goodSoulsInitializeValue);
    //         ChangeBySoulStones(soulStonesInitializeValue);
    //         ChangeBySwords(swordsInitializeValue);
    //         ChangeByShields(shieldsInitializeValue);
    //     }
    // }

    private void SpeedStateHandler()
    {
        if (speedState == SpeedState.Stop) return;

        if (curSpeed <= speedToStartBreaking)
        {
            speedState = SpeedState.Low;
        }

        else if (curSpeed <= maxSpeed * 0.75f)
        {
            speedState = SpeedState.Mid;
        }
        else
        {
            speedState = SpeedState.Max;
        }
    }

    public void StopTrain()
    {
        if (speedState != SpeedState.Stop)
        {
            // print("stop train");
            SetSpeed(0);
            speedState = SpeedState.Stop;
        }
    }

    public void ContinueTrain()
    {
        if (_firstTimeUsingStartHandle)
        {
            _firstTimeUsingStartHandle = false;
            if (_tutorialIsOn)
            {
                _tutorial.FirstTimeHandle();
            }
        }

        ChangeBySoulStones(takeDownFuelContinueTrain);
        speedState = SpeedState.Run;
    }

    private void ActivateNewEvent()
    {
        if (_inEvent) return;
        _inEvent = true;
        Time.timeScale = 0;
        _gotToNewEvent = false;
        _interactionManager.gameObject.SetActive(true);
        _interactionManager.StartInteraction(_currEventData);
    }

    public void ReturnFromEvent()
    {
        Time.timeScale = 1;
        _inEvent = false;
    }


    private void EndGame()
    {
        Application.Quit();
        print("end game");
    }

    public void OpenGoodSoulTuturial()
    {
        if (_tutorial && _tutorialIsOn && _firstTimeAddingGoodSouls)
        {
            _firstTimeAddingGoodSouls = false;
            _tutorial.OpenTutorialObject(TutorialObject.TutorialKind.GoodSouls);
        }
    }

    public void OpenBadsoulTuturial()
    {
        if (_tutorial && _tutorialIsOn && _firstTimeAddingBadSouls)
        {
            _firstTimeAddingBadSouls = false;
            _tutorial.OpenTutorialObject(TutorialObject.TutorialKind.BadSouls);
        }
    }

    public void ChangeByGoodSouls(int addNum)
    {
        if (GoodSouls + addNum < 0)
        {
            GoodSouls = 0;
            return;
        }

        GoodSouls += addNum;
        _uiManager.SetGoodSouls();
        _soulsCircle.ChangeSoulsAmount();
    }

    public int GetGoodSouls()
    {
        return GoodSouls;
    }


    public void ChangeByBadSouls(int addNum)
    {
        if (_firstTimeAddingBadSouls)
        {
            _firstTimeAddingBadSouls = false;
            if (_tutorialIsOn)
                _tutorial.OpenTutorialObject(TutorialObject.TutorialKind.BadSouls);
        }

        if (BadSouls + addNum < 0)
        {
            BadSouls = 0;
            return;
        }

        BadSouls += addNum;

        _uiManager.SetBadSouls();
        _soulsCircle.ChangeSoulsAmount();
    }

    public int GetBadSouls()
    {
        return BadSouls;
    }

    public void ChangeBySoulStones(int addNum)
    {
        if (SoulStones + addNum < 0)
        {
            SoulStones = 0;
            if (devilEvent == null)
            {
                print("No devil event");
            }
            else
            {
                _interactionManager.StartInteraction(devilEvent);
            }

            return;
        }

        if (SoulStones + addNum > maxSoulStones)
            addNum = maxSoulStones - SoulStones;

        SoulStones += addNum;
        _uiManager.SetSoulStones();
        _soulsCircle.ChangeSoulsAmount();
    }

    public int GetSoulStones()
    {
        return SoulStones;
    }

    public void ChangeBySwords(int addNum)
    {
        if (!_firstTimeSpecialItem)
        {
            _firstTimeSpecialItem = true;
            if (_tutorialIsOn)
                _tutorial.OpenTutorialObject(TutorialObject.TutorialKind.SpecialItem);
        }

        if (Swords + addNum < 0)
            Swords = 0;

        Swords += addNum;
        _uiManager.SetSwords();
    }

    public void ChangeByShields(int addNum)
    {
        if (!_firstTimeSpecialItem)
        {
            _firstTimeSpecialItem = true;
            if (_tutorialIsOn)
                _tutorial.OpenTutorialObject(TutorialObject.TutorialKind.SpecialItem);
        }

        if (Shields + addNum < 0)
            Shields = 0;

        Shields += addNum;
        _uiManager.SetShields();
    }


    public float GetSpeed()
    {
        return curSpeed;
    }


    public void SetSpeed(float newSpeed, bool fromFurnace = false)
    {
        if (fromFurnace)
        {
            if (_tutorialIsOn)
                _tutorial.FirstTimeFurnace();
        }

        UIAudioManager.Instance.SetTrainLoopSpeed(GetCurrSpeedPerspectiveToStartSpeed());
        if (speedState == SpeedState.Stop)
        {
            return;
        }

        if (newSpeed <= minSpeed)
        {
            curSpeed = minSpeed;
        }

        else if (newSpeed >= maxSpeed)
        {
            curSpeed = maxSpeed;
        }

        else
        {
            curSpeed = newSpeed;
        }
    }

    public void ArrowSpriteHandler(Arrow.ArrowSide sideToMark)
    {
        foreach (var arrow in _arrows)
        {
            arrow.ArrowHandler(sideToMark, "ArrowSpriteHandler");
        }
    }

    private void InitArrows()
    {
        _arrows = FindObjectsOfType<Arrow>();
        ArrowsTurnOnAndOff(false);
    }

    public void ArrowsTurnOnAndOff(bool mood, LoadIcon.IconIndex[][] itemIndexes = null)
    {
        if (_firstTimeArrows)
        {
            if (mood)
            {
                _firstTimeArrows = false;
            }
        }


        foreach (var arrow in _arrows)
        {
            switch (mood)
            {
                case true:
                    arrow.SetIcons(itemIndexes);
                    _arrowsAreOn = true;
                    SlowTheGame("ArrowsTurnOnAndOff in game manager");
                    arrow.gameObject.SetActive(true);
                    arrow.OpenObject();
                    break;

                case false:
                    _arrowSide = Arrow.ArrowSide.None;
                    _arrowsAreOn = false;
                    Time.timeScale = 1f;
                    arrow.CloseObject();
                    break;
            }
        }
    }

    private void TestArrows()
    {
        if (testArrows)
        {
            ArrowsTurnOnAndOff(true);
        }

        else
        {
            ArrowsTurnOnAndOff(false);
            SetArrowSide(Arrow.ArrowSide.None);
        }
    }

    private void ArrowOverHandler()
    {
        if (_arrowsAreOn)
        {
            if (IsMouseOverUI())
            {
                ArrowsMouseOver();
            }
            else
            {
                if (curMouseOnArrow)
                {
                    curMouseOnArrow.SetIsMouseIsOn(false);
                }
            }
        }
    }

    public GameObject GetTrain()
    {
        return train;
    }

    public bool GetIsArrowsAreOn()
    {
        return _arrowsAreOn;
    }

    public Arrow.ArrowSide GetArrowSide()
    {
        return _arrowSide;
    }


    public void SetArrowSide(Arrow.ArrowSide sideChosen)
    {
        _arrowSide = sideChosen;
    }


    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

    public SpeedState GetSpeedState()
    {
        return speedState;
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void ArrowsMouseOver()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResultsList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);

        for (int i = 0; i < raycastResultsList.Count; i++)
        {
            var arrow = raycastResultsList[i].gameObject.GetComponent<Arrow>();
            if (arrow)
            {
                arrow.SetIsMouseIsOn(true);
                curMouseOnArrow = arrow;
            }
        }
    }

    private void InitiateCheckPointData()
    {
        _CheckPointData = (CheckPointData) ScriptableObject.CreateInstance(typeof(CheckPointData));
        SaveCheckPointData();
        // print(_CheckPointData.SoulStones);
    }

    public void SaveCheckPointData()
    {
        //Inventory
        _CheckPointData.SoulStones = soulStonesInitializeValue;
        _CheckPointData.GoodSouls = GoodSouls;
        _CheckPointData.BadSouls = BadSouls;

        //TrainState
    }

    public void ReturnToLastCheckPoint()
    {
        //Inventory
        soulStonesInitializeValue = _CheckPointData.SoulStones;
        GoodSouls = _CheckPointData.GoodSouls;
        BadSouls = _CheckPointData.BadSouls;

        //TrainState
    }

    public float GetCurrSpeedPerspectiveToStartSpeed()
    {
        var startSpeed = Mathf.Lerp(minSpeed, maxSpeed, 0.5f);
        var pitch = curSpeed / startSpeed;
        pitch = pitch <= 0.5f ? 0 : pitch;
        pitch = pitch >= 1.7f ? 2 : pitch;
        return pitch;
    }

    public void ChangeInventoryFromDrag(Furnace.BurnObject objectToBurn)
    {
        switch (objectToBurn)
        {
            case Furnace.BurnObject.Fuel:
                ChangeBySoulStones(takeDownDragFuel);
                break;

            // case Furnace.BurnObject.GoodSoul:
            //     ChangeByGoodSouls(takeDownDragGoodSouls);
            //     break;

            case Furnace.BurnObject.BadSoul:
                ChangeByBadSouls(takeDownDragBadSouls);
                break;
        }
    }

    private void CalculateMovingDirection()
    {
        var newTrainPosition = train.transform.position;
        _moveDirection = newTrainPosition - _trainPosition;
        _trainPosition = newTrainPosition;
    }

    public Vector3 GetTrainDirection()
    {
        return _moveDirection.normalized;
    }

    public Vector3 GetTrainPosition()
    {
        return train.transform.position;
    }

    public GameObject GetTrainGameObject()
    {
        return train;
    }

    public InteractionManager GetEventManager()
    {
        return _interactionManager;
    }

    public Mouse GetMouse()
    {
        return _mouse;
    }

    public UIManager GetUIManager()
    {
        return _uiManager;
    }

    public void SlowTheGame(string whatFunctionCalledTheFunction)
    {
        Time.timeScale = arrowsAreOnSlowMotionTimeScale;
    }

    public void ReturnToRegularTime(string whatFunctionCalledTheFunction)
    {
        print("return to regular time");
        Time.timeScale = regularTimeScale;
    }

    public void SetTutorialIsOn(bool mood)
    {
        _tutorialIsOn = mood;
        if (!mood)
            CloseTutorial();
    }

    public bool GetTutorialIsOn()
    {
        return _tutorialIsOn;
    }

    public void ShakeHandle(bool mood)
    {
        BreakChain breakChain = FindObjectOfType<BreakChain>();
        if (breakChain)
            breakChain.Shake(mood);
    }

    public void TutorialsJunction()
    {
        if (_tutorialIsOn)
        {
            if (_firstTimeJunction)
            {
                _firstTimeJunction = false;
                _tutorial.StartJunction();
            }
        }
    }

    public void ArrowsTutorial()
    {
        if (_firstTimeArrowsTutorials)
        {
            _firstTimeArrowsTutorials = false;
            _tutorial.FirstTimeArrows();
        }
    }

    private void CloseTutorial()
    {
        _tutorialIsOn = false;
        if (!_tutorial)
            _tutorial = FindObjectOfType<Tutorial>();

        if (_tutorial)
        {
            _tutorial.CloseTutorial();
            _tutorial.gameObject.SetActive(false);
        }
    }

    public void EndFuelEvent()
    {
        
    }
    
    public void GameOver()
    {
        SceneManager.LoadScene("Game lose");
    }
    
    public void GameWon()
    {
        gameData.goodSoulsAmount = GoodSouls;
        SceneManager.LoadScene("Game win");
    }
}

public enum Morale : int
{
    VeryBad,
    Bad,
    Neutral,
    Good,
    VeryGood,
}