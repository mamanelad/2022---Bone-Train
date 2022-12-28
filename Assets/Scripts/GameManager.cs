using System;
using UnityEditor;
using UnityEngine;

[DefaultExecutionOrder(-999)]
public class GameManager : MonoBehaviour
{
    [Header("Map")] [SerializeField] private JunctionChange curJunction;
    private JunctionChange nextJunction;

    [Space(20)] [Header("Event")] private EventObject _currEventData;
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

    [Space(20)] [Header("Train")] [SerializeField]
    private GameObject train;

    public enum Road
    {
        Up,
        Down
    }

    [Space(20)] [Header("Speed")] [SerializeField]
    private GameObject speedHandle;

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
        if (nextJunction == null)
        {
            if (curJunction == null)
            {
                print("Drag the first Junction in the game to the game manager");
            }
            else
            {
                nextJunction = curJunction.GetNextJunction();
            }
        }

        if (_eventManager == null)
        {
            print("Drag the event manager of the scene to the game manager");
            _eventManager = FindObjectOfType<EventManager>();
            // _eventManager.gameObject.SetActive(false);
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


    // public void OpenMap()
    // {
    //     map.changeScale = true;
    // }

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

    public JunctionChange GetCurrJunction()
    {
        return curJunction;
    }

    public JunctionChange GetNextJunction()
    {
        return nextJunction;
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
}

public enum Morale : int
{
    VeryBad,
    Bad,
    Neutral,
    Good,
    VeryGood,
}