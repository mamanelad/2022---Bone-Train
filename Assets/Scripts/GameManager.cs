using System;
using UnityEngine;

[DefaultExecutionOrder(-999)]
public class GameManager : MonoBehaviour
{
    public static GameManager Shared { get; set; }

    [SerializeField] private LevelManager levelManager;

    public LevelData ld;

    [HideInInspector] public int SoulStones { get; set; }
    [HideInInspector] public int GoodSouls { get; set; }
    [HideInInspector] public int BadSouls { get; set; }

    [HideInInspector] public Morale morale = Morale.Neutral;


    [SerializeField] private Map _map;
    [SerializeField] private LevelData[] _levelsData;
    [SerializeField] private int levelIndex;
    public bool goToNextLevel;
    public LevelData nextLevel;
    
    public enum Road
    {
        Up,
        Down
    }

    public Road curRoad;
    private void Awake()
    {
        Shared = this;
        // if (Shared == null)
        // {
        //     Shared = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else
        //     Destroy(gameObject);
    }

    private void Update()
    {
        if (goToNextLevel)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        goToNextLevel = false;

        // foreach (var node in _map.GetComponentsInChildren<Node>())
        // {
        //     if (node.InThisNode())
        //     {
        //         nextLevel = node.GetLevelData();
        //     }
        // }
        
        levelManager.StartLevel(nextLevel);
        
    }

    public void OpenMap()
    {
        _map.changeScale = true;
    }
    
    private void EndGame()////////////////////
    {
        Application.Quit();
        print("end game");
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
