using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempEventsTest : MonoBehaviour
{
    [Serializable]
    public struct Branch
    {
        public int branchIndex;
        public List<EventObject> events;
    }
    private enum Direction
    {
        RUN,
        LEFT,
        RIGHT,
        WAIT,
        DONE
    }
    
    [SerializeField] private ModiefiedEventManager eventManager;
    [SerializeField] private float timeBetweenEvents = 1.5f;
    [SerializeField] private List<Branch> branches;
    [SerializeField] private GameObject turnButton;

    private Branch currentBranch;
    private int currentBranchIndex;
    private List<EventObject> currentBranchEvents;
    
    private float currentTimer;
    private int eventIndex;
    
    private Direction direction = Direction.RUN;
    
    private void Start()
    {
        currentTimer = timeBetweenEvents;
        turnButton.SetActive(false);
        SetUpBranchData(1);
    }

    private void SetUpBranchData(int index)
    {

        if (index-1 > branches.Count || direction == Direction.DONE)
        {
            direction = Direction.DONE;
            return;
        }
        
        index -= 1;
        if (branches.Count < index)
        {
            print("Branch doesn't exists");
            return;
        }
        currentBranch = branches[index];
        currentBranchIndex = currentBranch.branchIndex;
        currentBranchEvents = currentBranch.events;
    }

    private void Update()
    {
        if (direction == Direction.DONE)
            return;
        
        if (direction == Direction.RUN)
            RunEvents();
        if (direction == Direction.WAIT)
            Turn();
        if (direction == Direction.LEFT)
            DoTurnLeft();
        if (direction == Direction.RIGHT)
            DoTurnRight();
    }

    private void DoTurnLeft()
    {
        SetUpBranchData(currentBranchIndex * 2);
        direction = Direction.RUN;
        eventIndex = 0;
    }
    
    private void DoTurnRight()
    {
        SetUpBranchData(currentBranchIndex * 2 + 1);
        direction = Direction.RUN;
        eventIndex = 0;
    }

    private void RunEvents()
    {
        
        if (currentTimer > 0 && Time.timeScale != 0)
        {
            currentTimer -= Time.deltaTime;
            return;
        }
        else if (Time.timeScale == 0)
        {
            currentTimer = timeBetweenEvents;
            return;
        }

        if (eventIndex >= currentBranchEvents.Count && Time.timeScale != 0)
        {
            direction = Direction.WAIT;
            return;
        }
        
        eventManager.StartEvent(currentBranchEvents[eventIndex]);
        TempGameManager.Shared.ChangeBySoulStones(-10);
        eventIndex += 1;
        
    }

    private void Turn()
    {
        if (turnButton.activeSelf)
            return;
        
        turnButton.SetActive(true);
    }

    public void TurnLeft()
    {
        if (direction != Direction.DONE)
            direction = Direction.LEFT;
        turnButton.SetActive(false);
    }

    public void TurnRight()
    {
        if(direction != Direction.DONE)
            direction = Direction.RIGHT;
        turnButton.SetActive(false);
    }
}
