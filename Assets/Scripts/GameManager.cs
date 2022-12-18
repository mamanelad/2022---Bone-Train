using System;
using UnityEditor;
using UnityEngine;

[DefaultExecutionOrder(-999)]
public class GameManager : MonoBehaviour
{
    [Header("Map")] [SerializeField] private Map map;

    [Space(20)] [Header("Event")] private EventData _currEventData;
    private bool _inEvent;
    private bool _gotToNewEvent;
    [SerializeField] private EventManager _eventManager;


    [Space(20)] [Header("UI")] private UIManager _uiManager;
    [SerializeField] public int soulStonesInitializeValue;
    [SerializeField] public int goodSoulsInitializeValue;
    [SerializeField] public int badSoulsInitializeValue;

    [NonSerialized] public int SoulStones;
    [NonSerialized] public int GoodSouls;
    [NonSerialized] public int BadSouls;

    [Space(20)] [Header("Extra")] public static GameManager Shared;
    public LevelData ld;
    [HideInInspector] public Morale morale = Morale.Neutral;

    [Space(20)] [Header("Road")] public Road curRoad;
    

    public enum Road
    {
        Up,
        Down
    }

    [Space(20)] [Header("Speed")] [SerializeField]
    private GameObject speedHandle;

    private float _trainSpeed;
    [SerializeField] private SpeedState speedState = SpeedState.Stop; 
    public enum SpeedState
    {
        Max,
        Mid,
        Low, 
        Stop
    }


    private void Awake()
    {
        Shared = this;
        SoulStones = soulStonesInitializeValue;
        GoodSouls = goodSoulsInitializeValue;
        BadSouls = badSoulsInitializeValue;

        //
        // if (shared == null)
        // {
        //     shared = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else
        //     Destroy(gameObject);
    }


    private void Update()
    {
        if (_eventManager == null)
        {
            print("Drag the event manager of the scene to the game manager");
            _eventManager = FindObjectOfType<EventManager>();
            _eventManager.gameObject.SetActive(false);
        }


        if (_uiManager == null)
            _uiManager = FindObjectOfType<UIManager>();

        if (_gotToNewEvent)
            ActivateNewEvent();
    }

    private void ActivateNewEvent()
    {
        if (_inEvent) return;
        _inEvent = true;
        Time.timeScale = 0;
        _gotToNewEvent = false;
        _eventManager.gameObject.SetActive(true);
        _eventManager.data = _currEventData;
        _eventManager.ConfigureEvent();
    }

    public void ReturnFromEvent()
    {
        Time.timeScale = 1;
        _inEvent = false;
    }


    public void OpenMap()
    {
        map.changeScale = true;
    }

    private void EndGame()
    {
        Application.Quit();
        print("end game");
    }

    public void AddToGoodSouls(int addNum)
    {
        if (GoodSouls + addNum < 0) return;
        GoodSouls += addNum;
        _uiManager.ChangeGoodSouls();
    }

    public int GetGoodSouls()
    {
        return GoodSouls;
    }


    public void AddToBadSouls(int addNum)
    {
        if (BadSouls + addNum < 0) return;
        BadSouls += addNum;
        _uiManager.ChangeBadSouls();
    }

    public int GetBadSouls()
    {
        return BadSouls;
    }

    public void AddToSoulStones(int addNum)
    {
        if (SoulStones + addNum < 0) return;
        SoulStones += addNum;
        _uiManager.ChangeSoulStones();
    }

    public int GetSoulStones()
    {
        return SoulStones;
    }

    public void GotToEvent(EventData newEventData)
    {
        _gotToNewEvent = true;
        _currEventData = newEventData;
    }

    public float GetSpeed()
    {
        return _trainSpeed;
    }
    
    public void SetSpeed(float newSpeed)
    {
        _trainSpeed = newSpeed;
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