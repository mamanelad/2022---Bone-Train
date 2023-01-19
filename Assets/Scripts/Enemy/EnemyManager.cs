using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager : MonoBehaviour
{
    private EventManager _eventManager;
    [SerializeField] private EventObject enemyEvent;

    [Space(10)] [Header("Amounts")] private int _enemiesAmount;
    [SerializeField] private int enemiesAmountMax = 0;


    [Space(10)] [Header("Enemy")] [SerializeField]
    private GameObject enemyPrefab;

    [Space(10)] [Header("Distance")] [SerializeField]
    private float maxDistanceToCreateNewEnemy = 20f;
    [SerializeField] private float minDistanceToCreateNewEnemy = 10f;

    [Space(10)] [Header("Angle")] [SerializeField]
    private float rotationAngle = 30f;

    [Space(10)] [Header("Tests")] [SerializeField]
    private bool creatEnemy;

    [SerializeField] private bool creatCloseEnemy;
    // [Space(10)] [Header("Speed")] [SerializeField]
    // private float speedToCreatEnemy;
    
    // [Space(10)] [Header("Times")]
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!_eventManager)
            _eventManager = GameManager.Shared.GetEventManager();
        
        if (CanCreateNewEnemy() || creatEnemy)
            CreateNewEnemy();


    }

    public void AttackTrain(Enemy curEnemy = null)
    {
        if (enemyEvent)
        {    _eventManager = GameManager.Shared.GetEventManager();
            _eventManager.StartEvent(enemyEvent);
        }

        else
        {
            print("No enemy event Data");
        }

        if (curEnemy)
        {
            Destroy(curEnemy.gameObject);
            DecreesEnemiesAmount();
        }
        
    }

    private void DecreesEnemiesAmount()
    {
        _enemiesAmount = Mathf.Max(0, _enemiesAmount-1);

    }

    /**
     * Return true if the train speed state is LOW,
     * and the amount of enemies are lower than the max amount.
     */
    private bool CanCreateNewEnemy()
    {
        if (_enemiesAmount >= enemiesAmountMax) return false;
        var curTrainState = GameManager.Shared.GetSpeedState();
        return curTrainState == GameManager.SpeedState.Low;
    }

    private void CreateNewEnemy()
    {
        creatEnemy = false;
        _enemiesAmount += 1;
        var trainMovingDirection = GameManager.Shared.GetTrainDirection();
        var oppositeDirection = trainMovingDirection * (-1);
        var distanceToCreat = Random.Range(minDistanceToCreateNewEnemy, maxDistanceToCreateNewEnemy);

        var distant = distanceToCreat;
        if (creatCloseEnemy)
        {
            distant = 100f;
        }
        
        var directionAndLength = distant * oppositeDirection;

        var randomRotationAngle = Random.Range(-rotationAngle , rotationAngle );
        var newDirectionVector = Quaternion.AngleAxis(randomRotationAngle, Vector3.up) * directionAndLength;

        var trainPos = GameManager.Shared.GetTrainPosition();

        var positionToCreatNewEnemy = trainPos + newDirectionVector;

        var newEnemy = Instantiate(enemyPrefab, positionToCreatNewEnemy, Quaternion.identity, transform);
        var enemyComponent = newEnemy.GetComponent<Enemy>();
        var train = GameManager.Shared.GetTrain();
        enemyComponent.InitEnemy(this, train);
    }
}
