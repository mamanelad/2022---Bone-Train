using System;
using UnityEditor;
using UnityEngine;
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
    private bool _arrowsAreOn;
    private Arrow.ArrowSide _arrowSide = Arrow.ArrowSide.None;

    [Space(20)] [Header("Extra")] public static GameManager Shared;
    [HideInInspector] public Morale morale = Morale.Neutral;

    [Space(20)] [Header("Road")] public Road curRoad;

    [Space(20)] [Header("Speed")] [SerializeField]
    public float maxSpeed = 1100;

    public float minSpeed = 100;
    private float curSpeed = 500;

    private GameObject speedHandle;

    [SerializeField] private SpeedState speedState = SpeedState.Low;

    [Space(20)] [Header("Train")] [SerializeField]
    private GameObject train;

    [Space(20)] [Header("Fuel")] private FuelManager _fuelManager;

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


    private void Awake()
    {
        Shared = this;
        curSpeed = Mathf.Lerp(minSpeed, maxSpeed, 0.5f);

        //
        // if (shared == null)
        // {
        //     shared = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else
        //     Destroy(gameObject);
        if (_arrows.Length != 2)
        {
            print("Drag Arrows to the arrow array");
        }

        if (train == null)
        {
            print("Drag the train first to the game manager");
        }
    }


    private void Update()
    {
        return;
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
        speedState = SpeedState.Stop;
    }

    public void ContinueTrain()
    {
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
        if (GoodSouls + addNum < 0) {};
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
        curSpeed = newSpeed;
    }

    public void ArrowSpriteHandler(Arrow.ArrowSide sideToMark)
    {
        foreach (var arrow in _arrows)
        {
            arrow.ArrowHandler(sideToMark);
        }
    }

    public void ArrowsTurnOnAndOff(bool on)
    {
        _arrowsAreOn = on;
        foreach (var arrow in _arrows)
        {
            arrow.gameObject.SetActive(on);
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
}

public enum Morale : int
{
    VeryBad,
    Bad,
    Neutral,
    Good,
    VeryGood,
}