using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("GameObjects")]
    private GameObject _train;

    private EnemyManager _enemyManager;

    [Space(10)] [Header("Speed")] [SerializeField]
    private float _speedPercentageFromTrain;
    [SerializeField] private float factorSpeed = 0.1f;
    private float _speed;

    [Space(10)] [Header("Distance")] [SerializeField]
    private float distanceForAttack;
    [SerializeField]
    private float distanceForDestroy;
    private void Awake()
    {
        SetSpeed();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_train)
        {
            print("Needs to give the enemy the train game object");
            InitEnemy();
        }
        else
        {
            TowardsTrain();
            if (CheckIfCanAttack())
            {
                _enemyManager.AttackTrain();
            }

            if (CheckIfCanDestroy())
            {
                Destroy(gameObject);
            }
        }
    }

    public void InitEnemy(EnemyManager enemyManager = null, GameObject train = null)
    {
        _train = !train ? FindObjectOfType<SplineWalker>().gameObject : train;
        _enemyManager = !enemyManager ? FindObjectOfType<EnemyManager>() : enemyManager;
    }

    private void SetSpeed()
    {
        var maxTrainSpeed = GameManager.Shared.GetMaxSpeed();
        _speed = maxTrainSpeed * (_speedPercentageFromTrain / 100);
    }

    private void TowardsTrain()
    {
        if (GameManager.Shared.GetSpeedState() != GameManager.SpeedState.Stop)
        {
            Vector3 myPosition = transform.position;
            Vector3 trainPosition = _train.transform.position;
            Vector3 newPosition = Vector3.MoveTowards(myPosition, trainPosition, _speed * factorSpeed);
            transform.position = newPosition;
        }
    }

    private bool CheckIfCanAttack()
    {
        Vector3 myPosition = transform.position;
        Vector3 trainPosition = _train.transform.position;
        var distance = Vector3.Distance(myPosition, trainPosition);
        return distance <= distanceForAttack;
    }
    
    private bool CheckIfCanDestroy()
    {
        Vector3 myPosition = transform.position;
        Vector3 trainPosition = _train.transform.position;
        var distance = Vector3.Distance(myPosition, trainPosition);
        return distance >= distanceForDestroy;
    }
}
