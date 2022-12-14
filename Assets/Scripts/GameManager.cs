using System;
using UnityEngine;

[DefaultExecutionOrder(-999)]
public class GameManager : MonoBehaviour
{
    [Space(20)] [Header("Map")] [SerializeField]
    private Map _map;

    [Space(20)] [Header("Levels")] [SerializeField]
    private LevelData[] _levelsData;

    [SerializeField] private int levelIndex;
    public bool goToNextLevel;
    public LevelData nextLevel;
    [SerializeField] private LevelManager levelManager;

    [Space(20)] [Header("Event")] private EventData currEventData;
    private bool inEvent;
    private bool gotToNewEvent;
    private EventManager _eventManager;


    [Space(20)] [Header("UI")] private UIManager _uiManager;
    [SerializeField] public int SoulStonesInitializeValue;
    [SerializeField] public int GoodSoulsInitializeValue;
    [SerializeField] public int BadSoulsInitializeValue;

    [NonSerialized] public int SoulStones;
    [NonSerialized] public int GoodSouls;
    [NonSerialized] public int BadSouls;

    [Space(20)] [Header("Extra")] public static GameManager shared;
    public LevelData ld;
    [HideInInspector] public Morale morale = Morale.Neutral;

    [Space(20)] [Header("Road")] public Road curRoad;

    public enum Road
    {
        Up,
        Down
    }


    private void Awake()
    {
        shared = this;
        SoulStones = SoulStonesInitializeValue;
        GoodSouls = GoodSoulsInitializeValue;
        BadSouls = BadSoulsInitializeValue;
        
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
            _eventManager = FindObjectOfType<EventManager>();
        
        if (_uiManager == null)
            _uiManager = FindObjectOfType<UIManager>();
        
        if (gotToNewEvent)
            ActivateNewEvent();
        
    }

    private void ActivateNewEvent()
    {
        gotToNewEvent = false;
        _eventManager.data = currEventData;
        _eventManager.ConfigureEvent();
    }

    public void NextLevel()
    {
        goToNextLevel = false;
        levelManager.StartLevel(nextLevel);
    }

    public void OpenMap()
    {
        _map.changeScale = true;
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
        gotToNewEvent = true;
        currEventData = newEventData;
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