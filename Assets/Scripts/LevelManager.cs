using System;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelData data;
    [SerializeField] private GameObject uiEvent;

    public static LevelManager Shared { get; set; }

    public bool InEvent { get; private set; }
    public bool InLevel { get; private set; }
    
    private float timer;
    private int interactionIndex;

    private void Awake()
    {
        Shared = this;
    }

    private void Start()
    {
        uiEvent.SetActive(false);
        StartLevel(data);
    }
    
    private void Update()
    {
        if (InEvent || data == null)
            return;
        
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            RunEvent();
    }

    private void RunEvent()
    {
        if (interactionIndex == data.interactions.Count)
        {
            EndLevel();
            return;
        }

        InEvent = true;
        Time.timeScale = 0;
        var eventManager = uiEvent.GetComponent<EventManager>();
        eventManager.data = data.interactions[interactionIndex];
        uiEvent.SetActive(true);
        eventManager.ConfigureEvent();
        timer = data.delayBetweenInteractions;
        interactionIndex += 1;
    }

    public void FinishEvent()
    {
        InEvent = false;
        uiEvent.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void StartLevel(LevelData levelData)
    {
        if (InLevel)
            return;

        InLevel = true;
        data = levelData;
        timer = data.delayBetweenInteractions;
    }

    public void EndLevel()
    {
        InLevel = false;
        data = null;
    }
}
