using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EventManager _eventManager;
    [SerializeField] private EventObject enemyEvent;

    [Space(10)] [Header("Amounts")] private int _enemiesAmount;
    [SerializeField] private int _enemiesAmountMax;
    
    // [Space(10)] [Header("Times")]
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

    public void DecreesEnemiesAmount()
    {
        _enemiesAmount = Mathf.Max(0, _enemiesAmount-1);

    }
}
