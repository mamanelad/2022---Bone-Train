using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private bool _notInitialize = true;
    [Header("GameObjects")]
    private GameObject _train;

    private EnemyManager _enemyManager;

    [Space(10)] [Header("Speed")] [SerializeField]
    private float _speedPercentageFromTrainMax;
    [SerializeField] private float _speedPercentageFromTrainMin;
    private float _speedPercentageFromTrain;
    [SerializeField] private float factorSpeed = 0.1f;
    private float _speed;

    [Space(10)] [Header("Distance")] [SerializeField]
    private float distanceForAttack;
    [FormerlySerializedAs("distanceForDestroy")] [SerializeField]
    private float distanceForSelfDestroy;


    [SerializeField] private float raiderSpeed = 2000;
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (_notInitialize) return;
        
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
                _enemyManager.AttackTrain(this);
            }
            
            if (CheckIfCanDestroy())
            {
                Destroy(gameObject);
            }
        }
    }

    public void InitEnemy(EnemyManager enemyManager = null, GameObject train = null)
    {
        _notInitialize = false;
        _train = !train ? FindObjectOfType<SplineWalker>().gameObject : train;
        _enemyManager = !enemyManager ? FindObjectOfType<EnemyManager>() : enemyManager;
        SetSpeed();
    }

    private void SetSpeed()
    {
        _speedPercentageFromTrain = Random.Range(_speedPercentageFromTrainMin, _speedPercentageFromTrainMax);
        var maxTrainSpeed = GameManager.Shared.GetMaxSpeed();
        _speed = maxTrainSpeed * (_speedPercentageFromTrain / 100);
    }

    private void TowardsTrain()
    {
        if (Time.timeScale == 0)
            return;
        var curSpeed = _speed;
        if (GameManager.Shared.GetSpeedState() == GameManager.SpeedState.Stop)
            curSpeed = _speed / 2;

        Vector3 myPosition = transform.position;
        Vector3 trainPosition = _train.transform.position;
        Vector3 newPosition = Vector3.MoveTowards(myPosition, trainPosition, raiderSpeed);
        transform.position = newPosition;
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
        return distance >= distanceForSelfDestroy;
    }
}
