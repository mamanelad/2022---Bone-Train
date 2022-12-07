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

    private void Awake()
    {
        if (Shared == null)
        {
            Shared = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            levelManager.StartLevel(ld);
        
        print(SoulStones);
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
