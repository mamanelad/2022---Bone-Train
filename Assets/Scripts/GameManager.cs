using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-999)]
public class GameManager : MonoBehaviour
{
    [Space(20)] [Header("Event")] private EventObject _currEventData;
    private bool _inEvent;
    private bool _gotToNewEvent;
    private EventManager _eventManager;
    [SerializeField] private EventObject devilEvent;


    [Space(20)] [Header("UI")] private UIManager _uiManager;
    [SerializeField] public int soulStonesInitializeValue;
    [SerializeField] public int goodSoulsInitializeValue;
    [SerializeField] public int badSoulsInitializeValue;

    [NonSerialized] public int SoulStones;
    [NonSerialized] public int GoodSouls;
    [NonSerialized] public int BadSouls;

    [SerializeField] private Arrow[] _arrows;
    private Arrow curMouseOnArrow;
    private bool _arrowsAreOn;
    private Arrow.ArrowSide _arrowSide = Arrow.ArrowSide.None;

    [Space(20)] [Header("Extra")] public static GameManager Shared;
    [HideInInspector] public Morale morale = Morale.Neutral;

    [Space(20)] [Header("Road")] public Road curRoad;

    [Space(20)] [Header("Speed")] [SerializeField]
    public float maxSpeed = 1100;

    public float minSpeed = 100;
    [SerializeField]private float curSpeed = 500;

    public int addToSpeedFuel;
    public int addToSpeedGoodSoul;
    public int addToSpeedBadSoul;

    private GameObject speedHandle;

    [SerializeField] private SpeedState speedState = SpeedState.Low;

    [Space(20)] [Header("Train")] [SerializeField]
    private GameObject train;

    [Space(20)] [Header("Fuel")] private FuelManager _fuelManager;
    
    [Space(20)] [Header("Take Down Amounts")]
    [SerializeField] private int takeDownFuelContinueTrain;
    [SerializeField] private int takeDownDragFuel;
    [SerializeField] private int takeDownDragGoodSouls;
    [SerializeField] private int takeDownDragBadSouls;
    

    [Header("Test")]
    [SerializeField] private bool testArrows0;
    [SerializeField] private bool testArrows;
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

    private void Awake()
    {
        
        Shared = this;
        speedState = SpeedState.Run;

        InitSouls();
        InitiateCheckPointData();

        if (_arrows.Length != 2)
        {
            print("Drag Arrows to the arrow array");
        }

        if (train == null)
        {
            print("Drag the train first to the game manager");
        }
    }


    private void Start()
    {
        ArrowsTurnOnAndOff(false);
    }

    private void Update()
    {
        if (testArrows0)
        {
            testArrows0 = false;
            TestArrows();
        }
        if (speedState == SpeedState.Stop) curSpeed = 0;
        if (speedState == SpeedState.Run) InitSpeed();

        ArrowOverHandler();
        if ((Input.GetKeyDown(KeyCode.Escape)))
        {
            SceneManager.LoadScene("MainMenue");
        }

        if (_eventManager == null)
        {
            _eventManager = FindObjectOfType<EventManager>();
        }

        if (_fuelManager == null)
        {
            _fuelManager = FindObjectOfType<FuelManager>();
        }

        InitUiNumbers();

        SpeedStateHandler();
        bool drivingMode = speedState != SpeedState.Stop;
        _fuelManager.SetDriving(drivingMode);

        if (_uiManager == null)
            _uiManager = FindObjectOfType<UIManager>();

        if (_gotToNewEvent)
            ActivateNewEvent();
    }

    private void InitSpeed()
    {
        curSpeed = Mathf.Lerp(minSpeed, maxSpeed, 0.5f);
    }

    private void InitSouls()
    {
        SoulStones = soulStonesInitializeValue;
        GoodSouls = goodSoulsInitializeValue;
        BadSouls = badSoulsInitializeValue;
    }

    private void InitUiNumbers()
    {
        if (_uiManager == null)
        {
            _uiManager = FindObjectOfType<UIManager>();
            if (_uiManager != null)
            {
                ChangeByBadSouls(badSoulsInitializeValue);
                ChangeByGoodSouls(goodSoulsInitializeValue);
                ChangeBySoulStones(soulStonesInitializeValue);
            }
        }
    }

    private void SpeedStateHandler()
    {
        if (speedState == SpeedState.Stop) return;
        if (curSpeed <= maxSpeed / 3)
        {
            speedState = SpeedState.Low;
        }
        else if (curSpeed <= maxSpeed / 2)
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
            print("stop train");
            SetSpeed(0);
            speedState = SpeedState.Stop;
        }
    }

    public void ContinueTrain()
    {
        ChangeBySoulStones(takeDownFuelContinueTrain);
        speedState = SpeedState.Run;
    }

    private void ActivateNewEvent()
    {
        if (_inEvent) return;
        _inEvent = true;
        Time.timeScale = 0;
        _gotToNewEvent = false;
        _eventManager.gameObject.SetActive(true);
        _eventManager.StartEvent(_currEventData);
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

    public void ChangeByGoodSouls(int addNum)
    {
        GoodSouls += addNum;
        _uiManager.SetGoodSouls();
    }

    public int GetGoodSouls()
    {
        return GoodSouls;
    }


    public void ChangeByBadSouls(int addNum)
    {
        if (BadSouls + addNum < 0) return;
        BadSouls += addNum;
        _uiManager.SetBadSouls();
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
                _eventManager.StartEvent(devilEvent);
            }
        }

        SoulStones += addNum;
        _uiManager.SetSoulStones();
    }

    public int GetSoulStones()
    {
        return SoulStones;
    }


    public float GetSpeed()
    {
        return curSpeed;
    }

    public void SetSpeed(float newSpeed)
    {
        UIAudioManager.Instance.SetTrainLoopSpeed(GetCurrSpeedPerspectiveToStartSpeed());
        if (speedState == SpeedState.Stop)
        {
            return;
        }

        if (speedState != SpeedState.Stop)
        {
            if (newSpeed >= minSpeed && newSpeed <= maxSpeed)
            {
                curSpeed = newSpeed;
            }
        }
    }

    public void ArrowSpriteHandler(Arrow.ArrowSide sideToMark)
    {
        foreach (var arrow in _arrows)
        {
            arrow.ArrowHandler(sideToMark);
        }
    }

    public void ArrowsTurnOnAndOff(bool mood)
    {
        _arrowsAreOn = mood;
        foreach (var arrow in _arrows)
        {
            if (mood)
            {
                arrow.gameObject.SetActive(mood);
                arrow.ChangeColorToOnColor();
            }
            else
            {
                arrow.SetIsPressedOf();
                arrow.ChangeColorToOnColor();
                arrow.gameObject.SetActive(mood);
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
        print(_CheckPointData.SoulStones);
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

            case Furnace.BurnObject.GoodSoul:
            ChangeByGoodSouls(takeDownDragGoodSouls);
            break;

            case Furnace.BurnObject.BadSoul:
            ChangeByBadSouls(takeDownDragBadSouls);
            break;
        }
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