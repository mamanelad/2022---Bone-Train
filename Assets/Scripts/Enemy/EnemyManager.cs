using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private InteractionManager _eventManager;
    [SerializeField] private InteractionData enemyEvent;

    [Space(10)] [Header("Amounts")] private int _enemiesAmount;
    [SerializeField] private int enemiesAmountMax = 0;


    [Space(10)] [Header("Enemy")] [SerializeField]
    private GameObject enemyPrefab;

    [Space(10)] [Header("Distance")] [SerializeField]
    private float maxDistanceToCreateNewEnemy = 20f;

    [SerializeField] private float minDistanceToCreateNewEnemy = 10f;

    [Space(10)] [Header("Angle")] [SerializeField]
    private float rotationAngle = 30f;


    [Space(10)] [Header("Times")] [SerializeField]
    private float addEnemiesTime = 3f;

    private bool _wait;

    private float _addEnemiesTimer;

    [Space(10)] [Header("Close Enemies")] public bool lockEnemies;
    public bool closeEnemies;

    [Space(10)] [Header("Tests")] [SerializeField]
    private bool creatEnemy;

    [SerializeField] private bool creatCloseEnemy;


    private void Awake()
    {
        _addEnemiesTimer = addEnemiesTime;
    }


    public void CloseAndLockEnemies()
    {
        if (closeEnemies) return;
        closeEnemies = true;
        var objects = FindObjectsOfType<Enemy>();
        foreach (var obj in objects)
            obj.gameObject.SetActive(false);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (lockEnemies)
            CloseAndLockEnemies();
        else
            closeEnemies = false;

        if (!_eventManager)
            _eventManager = GameManager.Shared.GetEventManager();

        if (_wait)
        {
            _addEnemiesTimer -= Time.deltaTime;
            if (_addEnemiesTimer <= 0)
            {
                _wait = false;
                _addEnemiesTimer = addEnemiesTime;
            }
        }

        if (!_wait && (CanCreateNewEnemy() || creatEnemy))
            CreateNewEnemy();
    }

    public void AttackTrain(Enemy curEnemy = null)
    {
        if (lockEnemies) return;
        if (enemyEvent)
        {
            _eventManager = GameManager.Shared.GetEventManager();
            _eventManager.StartInteraction(enemyEvent);
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
        _enemiesAmount = Mathf.Max(0, _enemiesAmount - 1);
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
        if (lockEnemies) return;
        _wait = true;
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

        var randomRotationAngle = Random.Range(-rotationAngle, rotationAngle);
        var newDirectionVector = Quaternion.AngleAxis(randomRotationAngle, Vector3.up) * directionAndLength;

        var trainPos = GameManager.Shared.GetTrainPosition();

        var positionToCreatNewEnemy = trainPos + newDirectionVector;

        var newEnemy = Instantiate(enemyPrefab, positionToCreatNewEnemy, Quaternion.identity, transform);
        var enemyComponent = newEnemy.GetComponent<Enemy>();
        var train = GameManager.Shared.GetTrain();
        enemyComponent.InitEnemy(this, train);
    }
}