using System;
using UnityEngine;

[DefaultExecutionOrder(-999)]
public class GameManager : MonoBehaviour
{
    public static GameManager shared;

    [SerializeField] private LevelManager levelManager;

    public LevelData ld;

    [HideInInspector] public Morale morale = Morale.Neutral;


    [SerializeField] private Map _map;
    [SerializeField] private LevelData[] _levelsData;
    [SerializeField] private int levelIndex;
    public bool goToNextLevel;
    public LevelData nextLevel;
    [Space(20)] [Header("UI")] private UIManager _uiManager;
    [SerializeField] public int SoulStones;
    [SerializeField] public int GoodSouls;
    [SerializeField] public int BadSouls;


    public enum Road
    {
        Up,
        Down
    }

    public Road curRoad;

    private void Awake()
    {

        _uiManager = FindObjectOfType<UIManager>();
        shared = this;
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
        UIController();
        if (goToNextLevel)
        {
            NextLevel();
        }
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

    private void UIController()
    {
        _uiManager.ChangeBadSouls();

        _uiManager.ChangeSoulStones();
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
}

public enum Morale : int
{
    VeryBad,
    Bad,
    Neutral,
    Good,
    VeryGood,
}