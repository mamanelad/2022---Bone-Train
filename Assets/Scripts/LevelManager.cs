using System;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelData data;
    [SerializeField] private GameObject eventPrefab;
    [SerializeField] private GameObject uiPanel;

    public static LevelManager Shared { get; set; }

    public bool InEvent { get; private set; }
    
    private float timer;
    private int interactionIndex = 0;
    private GameObject curEvent;
    
    private void Awake()
    {
        Shared = this;
    }

    public void StartLevel(LevelData levelData)
    {
        data = levelData;
        timer = data.delayBetweenInteractions;
    }

    public void EndLevel()
    {
        data = null;
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
        curEvent = Instantiate(eventPrefab);
        curEvent.transform.parent = uiPanel.transform;
        var eventManager = curEvent.GetComponent<EventManager>();
        eventManager.data = data.interactions[interactionIndex];
        eventManager.ConfigureEvent();
        timer = data.delayBetweenInteractions;
        interactionIndex += 1;
    }

    public void FinishEvent()
    {
        InEvent = false;
        Destroy(curEvent);
        Time.timeScale = 1;
    }
}
