using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EventManager _eventManager;
    [SerializeField] private EventObject enemyEvent;
    // Start is called before the first frame update
    void Start()
    {
        _eventManager = FindObjectOfType<EventManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackTrain()
    {
        _eventManager.StartEvent(enemyEvent);
    }
}
